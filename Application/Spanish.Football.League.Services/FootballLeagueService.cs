namespace Spanish.Football.League.Services
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Spanish.Football.League.Common;
    using Spanish.Football.League.Common.Enums;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.DomainModels;
    using Spanish.Football.League.Repository;
    using Spanish.Football.League.Services.Interfaces;
    using Spanish.Football.League.Services.Mappers;
    using Spanish.Football.League.Services.Utils;

    /// <inheritdoc/>
    public class FootballLeagueService(MapperlyProfile mapperly,
        IGenericRepository<Team, int> teamRepository,
        IGenericRepository<Match, int> matchRepository,
        IGenericRepository<Season, int> seasonRepository,
        IGenericRepository<Result, int> resultRepository,
        IGenericRepository<SeasonStats, int> seasonStatsRepository,
        IGenericRepository<Winner, int> winnerRepository,
        IGenericRepository<TeamDetails, int> teamDetailsRepository,
        IGameEngineService gameEngineService)
        : IFootballLeagueService
    {
        private readonly Random random = new();

        /// <inheritdoc/>
        public async Task<int> CreateSeasonAsync(CreateSeasonRequestDto request)
        {
            var season = new Season()
            {
                SeasonYear = request.SeasonYear,
            };

            await seasonRepository.AddAsync(season);
            await seasonRepository.SaveChangesAsync();

            var teams = GenerateTeams(request.NumberOfTeams, request.NumberOfStrongTeams, request.NumberOfWeakTeams);
            var matches = await GenerateMatchesAsync(teams, season.Id);

            await GenerateResultsAsync(teams, matches);
            await GenerateTeamDetailsAsync(teams, season.Id);

            return season.Id;
        }

        /// <summary>
        /// Generates teams for the season based on the number of teams and their strength.
        /// </summary>
        /// <param name="numberOfTeams">The total number of teams to generate.</param>
        /// <param name="numberOfStrongTeams">The number of strong teams.</param>
        /// <param name="numberOfWeakTeams">The number of weak teams.</param>
        /// <returns>A collection of generated teams.</returns>
        private IEnumerable<Team> GenerateTeams(int numberOfTeams, int numberOfStrongTeams, int numberOfWeakTeams)
        {
            var randomTeams = teamRepository.GetAll().AsEnumerable().OrderBy(t => random.Next()).Take(numberOfTeams).ToList();

            for (int i = 0; i < numberOfWeakTeams; i++)
            {
                randomTeams[i].Weight = RandomWeightGenerator.GenerateRandomWeight(GameConstants.WeakTeamsMinWeight, GameConstants.WeakTeamsMaxWeight);
            }

            for (int i = numberOfWeakTeams; i < numberOfWeakTeams + numberOfStrongTeams; i++)
            {
                randomTeams[i].Weight = RandomWeightGenerator.GenerateRandomWeight(GameConstants.StrongTeamsMinWeight, GameConstants.StrongTeamsMaxWeight);
            }

            for (int i = numberOfWeakTeams + numberOfStrongTeams; i < numberOfTeams; i++)
            {
                randomTeams[i].Weight = RandomWeightGenerator.GenerateRandomWeight(GameConstants.WeakTeamsMaxWeight + 0.1m, GameConstants.StrongTeamsMinWeight - 0.1m);
            }

            return randomTeams;
        }

        /// <summary>
        /// Generates matches for the given teams in a specific season.
        /// </summary>
        /// <param name="teams">The teams participating in the season.</param>
        /// <param name="seasonId">The ID of the season.</param>
        /// <returns>A collection of generated matches.</returns>
        private async Task<IEnumerable<Match>> GenerateMatchesAsync(IEnumerable<Team> teams, int seasonId)
        {
            var matches = new List<Match>();
            var teamList = teams.ToList();

            // First half of the season.
            for (int i = 0; i < teamList.Count; i++)
            {
                for (int j = i + 1; j < teamList.Count; j++)
                {
                    var team1 = teamList[i];
                    var team2 = teamList[j];

                    // Calculate the odds of the teams.
                    var teamsOdds = OddsCalculator.CalculateOdds(team1.Weight, team2.Weight);

                    matches.Add(new Match
                    {
                        SeasonId = seasonId,
                        SeasonHalf = SeasonHalvesEnum.Spring,
                        HomeTeamName = team1.Name,
                        HomeTeamOdd = teamsOdds.HomeTeamOdd,
                        AwayTeamName = team2.Name,
                        AwayTeamOdd = teamsOdds.AwayTeamOdd,
                    });
                }
            }

            // Second half of the season.
            var secondHalfMatches = new List<Match>();
            foreach (var match in matches.Where(m => m.SeasonHalf == SeasonHalvesEnum.Spring))
            {
                secondHalfMatches.Add(new Match
                {
                    SeasonId = seasonId,
                    SeasonHalf = SeasonHalvesEnum.Fall,
                    HomeTeamName = match.AwayTeamName,
                    HomeTeamOdd = match.AwayTeamOdd,
                    AwayTeamName = match.HomeTeamName,
                    AwayTeamOdd = match.HomeTeamOdd,
                });
            }

            matches.AddRange(secondHalfMatches);

            await matchRepository.AddRangeAsync(matches);
            await matchRepository.SaveChangesAsync();

            return matches;
        }

        /// <summary>
        /// Generates results for the matches in the season.
        /// </summary>
        /// <param name="teams">The teams participating in the matches.</param>
        /// <param name="matches">The matches for which results will be generated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateResultsAsync(IEnumerable<Team> teams, IEnumerable<Match> matches)
        {
            var results = new List<Result>();

            foreach (var match in matches)
            {
                var homeTeam = teams.FirstOrDefault(t => t.Name == match.HomeTeamName);
                var awayTeam = teams.FirstOrDefault(t => t.Name == match.AwayTeamName);

                if (homeTeam == null || awayTeam == null)
                {
                    throw new InvalidOperationException("Match contains invalid team names.");
                }

                // Use the GameEngineService to generate match scores
                var (homeScore, awayScore) = gameEngineService.GenerateMatchScore(homeTeam.Weight, awayTeam.Weight);

                var result = new Result
                {
                    SeasonId = match.SeasonId,
                    SeasonHalf = (int)match.SeasonHalf,
                    HomeTeamName = match.HomeTeamName,
                    AwayTeamName = match.AwayTeamName,
                    HomeTeamScore = homeScore,
                    AwayTeamScore = awayScore,
                    IsExpected = null,
                };

                results.Add(result);
            }

            await GenerateSeasonStatsAsync(results);

            await resultRepository.AddRangeAsync(results);
            await resultRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Generates season statistics based on the results of the matches.
        /// </summary>
        /// <param name="results">The results of the matches.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateSeasonStatsAsync(IEnumerable<Result> results)
        {
            var seasonStats = new Dictionary<string, SeasonStats>();

            int seasonId = results.Select(s => s.SeasonId).FirstOrDefault();

            foreach (var result in results.Where(r => r.SeasonId == seasonId))
            {
                if (!seasonStats.ContainsKey(result.HomeTeamName))
                {
                    seasonStats[result.HomeTeamName] = new SeasonStats
                    {
                        SeasonId = seasonId,
                        TeamName = result.HomeTeamName,
                        ScoredGoals = 0,
                        ConcededGoals = 0,
                        Points = 0,
                        Wins = 0,
                        Draws = 0,
                        Losses = 0,
                    };
                }

                if (!seasonStats.ContainsKey(result.AwayTeamName))
                {
                    seasonStats[result.AwayTeamName] = new SeasonStats
                    {
                        SeasonId = seasonId,
                        TeamName = result.AwayTeamName,
                        ScoredGoals = 0,
                        ConcededGoals = 0,
                        Points = 0,
                        Wins = 0,
                        Draws = 0,
                        Losses = 0,
                    };
                }

                var homeStats = seasonStats[result.HomeTeamName];
                homeStats.ScoredGoals += result.HomeTeamScore;
                homeStats.ConcededGoals += result.AwayTeamScore;
                homeStats.GoalDifference = homeStats.ScoredGoals - homeStats.ConcededGoals;

                var awayStats = seasonStats[result.AwayTeamName];
                awayStats.ScoredGoals += result.AwayTeamScore;
                awayStats.ConcededGoals += result.HomeTeamScore;
                awayStats.GoalDifference = awayStats.ScoredGoals - awayStats.ConcededGoals;

                if (result.HomeTeamScore > result.AwayTeamScore)
                {
                    homeStats.Points += 3;
                    homeStats.Wins++;

                    awayStats.Losses++;
                }
                else if (result.HomeTeamScore < result.AwayTeamScore)
                {
                    awayStats.Points += 3;
                    awayStats.Wins++;

                    homeStats.Losses++;
                }
                else
                {
                    homeStats.Points += 1;
                    awayStats.Points += 1;

                    homeStats.Draws++;
                    awayStats.Draws++;
                }
            }

            await seasonStatsRepository.AddRangeAsync(seasonStats.Values);
            await seasonStatsRepository.SaveChangesAsync();

            await GenerateSeasonWinnerAsync(seasonStats.Values);
        }

        /// <summary>
        /// Generates a season winner based on the season statistics.
        /// </summary>
        /// <param name="seasonStats">The statistics for the teams in the season.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateSeasonWinnerAsync(IEnumerable<SeasonStats> seasonStats)
        {
            var winner = new Winner();

            var winners = seasonStats.OrderByDescending(t => t.Points).ThenByDescending(t => t.GoalDifference).ThenByDescending(t => t.ScoredGoals).FirstOrDefault();

            winner.SeasonId = winners.SeasonId;
            winner.WinnerTeamName = winners.TeamName;
            winner.Points = winners.Points;

            await winnerRepository.AddAsync(winner);
        }

        /// <summary>
        /// Generates team details including weight and expected win percentage for a season.
        /// </summary>
        /// <param name="teams">The teams participating in the season.</param>
        /// <param name="seasonId">The ID of the season.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateTeamDetailsAsync(IEnumerable<Team> teams, int seasonId)
        {
            var teamDetails = new List<TeamDetails>();

            decimal totalWeight = teams.Sum(t => t.Weight);

            foreach (var team in teams)
            {
                var detail = new TeamDetails
                {
                    SeasonId = seasonId,
                    TeamName = team.Name,
                    Weight = team.Weight,
                    ExpectedWinPercentage = team.Weight / totalWeight * 100,
                };

                teamDetails.Add(detail);
            }

            await teamDetailsRepository.AddRangeAsync(teamDetails);
            await teamDetailsRepository.SaveChangesAsync();
        }
    }
}