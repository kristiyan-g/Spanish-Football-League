namespace Spanish.Football.League.Services
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
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
        ILogger<FootballLeagueService> logger,
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
        private readonly Random random = new ();

        /// <inheritdoc/>
        public async Task<int?> CreateSeasonAsync(CreateSeasonRequestDto request)
        {
            var isSeasonExist = await seasonRepository.GetAll().FirstOrDefaultAsync(x => x.SeasonYear == request.SeasonYear);
            if (isSeasonExist != null)
            {
                return null;
            }

            var season = new Season()
            {
                SeasonYear = request.SeasonYear,
            };

            await seasonRepository.AddAsync(season);
            await seasonRepository.SaveChangesAsync();

            var teamsDto = GenerateTeams(request.NumberOfTeams, request.NumberOfStrongTeams, request.NumberOfWeakTeams);
            var matchDtos = await GenerateMatchesAsync(teamsDto, season.Id);

            await GenerateResultsAsync(teamsDto, matchDtos);
            await GenerateTeamDetailsAsync(teamsDto, season.Id);

            return season.Id;
        }

        /// <inheritdoc/>
        public async Task<SeasonResultsResponseDto?> GetSeasonResultsAsync(int seasonId)
        {
            var season = await seasonRepository.GetByIdAsync(seasonId);
            if (season == null)
            {
                return null;
            }

            var result = await resultRepository.GetAll().Where(r => r.SeasonId == seasonId).ToListAsync();
            var winner = await winnerRepository.GetAll().FirstOrDefaultAsync(w => w.SeasonId == seasonId);

            var resultResponse = mapperly.MapToSeasonResultsResponse(season, result, winner);

            return resultResponse;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SeasonStatsResponseDto>?> GetSeasonStatsAsync(int seasonId)
        {
            var seasonStats = await seasonStatsRepository.GetAll().Where(s => s.SeasonId == seasonId).OrderByDescending(s => s.Points).ToListAsync();
            if (seasonStats.Count == 0)
            {
                return null;
            }

            var seasonStatsResponse = mapperly.MapToSeasonStatsResponse(seasonStats);

            return seasonStatsResponse;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TeamDetailsResponseDto>?> GetTeamDetailsAsync(int seasonId)
        {
            var teamDetails = await teamDetailsRepository.GetAll().Where(t => t.SeasonId == seasonId).OrderByDescending(t => t.ExpectedWinPercentage).ToListAsync();
            if (teamDetails.Count == 0)
            {
                return null;
            }

            var teamDetailsResponse = mapperly.MapToTeamDetailsResponse(teamDetails);

            return teamDetailsResponse;
        }

        /// <summary>
        /// Generates teams for the season based on the number of teams and their strength.
        /// </summary>
        /// <param name="numberOfTeams">The total number of teams to generate.</param>
        /// <param name="numberOfStrongTeams">The number of strong teams.</param>
        /// <param name="numberOfWeakTeams">The number of weak teams.</param>
        /// <returns>A collection of generated teams.</returns>
        private IEnumerable<TeamDto> GenerateTeams(int numberOfTeams, int numberOfStrongTeams, int numberOfWeakTeams)
        {
            var randomTeams = teamRepository.GetAll().AsEnumerable().OrderBy(t => random.Next()).Take(numberOfTeams);
            var randomTeamsDto = mapperly.MapToTeamDto(randomTeams).ToList();

            for (int i = 0; i < numberOfWeakTeams; i++)
            {
                randomTeamsDto[i].Weight = RandomWeightGenerator.GenerateRandomWeight(GameConstants.WeakTeamsMinWeight, GameConstants.WeakTeamsMaxWeight);
            }

            for (int i = numberOfWeakTeams; i < numberOfWeakTeams + numberOfStrongTeams; i++)
            {
                randomTeamsDto[i].Weight = RandomWeightGenerator.GenerateRandomWeight(GameConstants.StrongTeamsMinWeight, GameConstants.StrongTeamsMaxWeight);
            }

            for (int i = numberOfWeakTeams + numberOfStrongTeams; i < numberOfTeams; i++)
            {
                randomTeamsDto[i].Weight = RandomWeightGenerator.GenerateRandomWeight(GameConstants.WeakTeamsMaxWeight + 0.1m, GameConstants.StrongTeamsMinWeight - 0.1m);
            }

            logger.LogDebug($"Completed generating teams. Returning {randomTeamsDto.Count} teams!");
            return randomTeamsDto;
        }

        /// <summary>
        /// Generates matches for the given teams in a specific season.
        /// </summary>
        /// <param name="teamsDto">The teams participating in the season.</param>
        /// <param name="seasonId">The ID of the season.</param>
        /// <returns>A collection of generated matches.</returns>
        private async Task<IEnumerable<CreateMatchDto>> GenerateMatchesAsync(IEnumerable<TeamDto> teamsDto, int seasonId)
        {
            var matchDtos = new List<CreateMatchDto>();
            var teamList = teamsDto.ToList();
            int totalTeams = teamList.Count;

            // Round-Robin logic
            for (int i = 0; i < totalTeams - 1; i++)
            {
                for (int j = 0; j < totalTeams / 2; j++)
                {
                    var homeTeam = teamList[j];
                    var awayTeam = teamList[totalTeams - 1 - j];

                    var odds = OddsCalculator.CalculateOdds(homeTeam.Weight, awayTeam.Weight);

                    matchDtos.Add(new CreateMatchDto
                    {
                        SeasonId = seasonId,
                        SeasonHalf = SeasonHalvesEnum.Spring,
                        HomeTeamName = homeTeam.Name,
                        AwayTeamName = awayTeam.Name,
                        HomeTeamOdd = odds.HomeTeamOdd,
                        AwayTeamOdd = odds.AwayTeamOdd,
                    });
                }

                // Rotating the teams (without the first team)
                var lastTeam = teamList[totalTeams - 1];
                teamList.RemoveAt(totalTeams - 1);
                teamList.Insert(1, lastTeam);
            }

            // Generating the matches for the secong half
            var secondHalfMatches = matchDtos.Select(match =>
            {
                // Rotate the home and away team.
                var homeTeam = teamsDto.First(t => t.Name == match.AwayTeamName);
                var awayTeam = teamsDto.First(t => t.Name == match.HomeTeamName);

                var odds = OddsCalculator.CalculateOdds(homeTeam.Weight, awayTeam.Weight);

                return new CreateMatchDto
                {
                    SeasonId = seasonId,
                    SeasonHalf = SeasonHalvesEnum.Fall,
                    HomeTeamName = homeTeam.Name,
                    AwayTeamName = awayTeam.Name,
                    HomeTeamOdd = odds.HomeTeamOdd,
                    AwayTeamOdd = odds.AwayTeamOdd,
                };
            }).ToList();

            matchDtos.AddRange(secondHalfMatches);

            var matches = mapperly.MapToMatchEntity(matchDtos);

            await matchRepository.AddRangeAsync(matches);
            await matchRepository.SaveChangesAsync();

            logger.LogDebug($"Sucessfully created {matchDtos.Count} matches!");
            return matchDtos;
        }

        /// <summary>
        /// Generates results for the matches in the season.
        /// </summary>
        /// <param name="teamsDto">The teams participating in the matches.</param>
        /// <param name="matchDtos">The matches for which results will be generated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateResultsAsync(IEnumerable<TeamDto> teamsDto, IEnumerable<CreateMatchDto> matchDtos)
        {
            var resultDtos = new List<CreateResultDto>();

            foreach (var match in matchDtos)
            {
                var homeTeam = teamsDto.FirstOrDefault(t => t.Name == match.HomeTeamName);
                var awayTeam = teamsDto.FirstOrDefault(t => t.Name == match.AwayTeamName);

                // Use the GameEngineService to generate match scores
                var matchScore = gameEngineService.GenerateMatchScore(homeTeam.Weight, awayTeam.Weight);

                bool isExpected = CalculateIsExpectedFromOdds(match.HomeTeamOdd, match.AwayTeamOdd, matchScore.HomeTeamScore, matchScore.AwayTeamScore);

                var result = new CreateResultDto
                {
                    SeasonId = match.SeasonId,
                    SeasonHalf = match.SeasonHalf,
                    HomeTeamName = match.HomeTeamName,
                    HomeTeamOdd = match.HomeTeamOdd,
                    AwayTeamName = match.AwayTeamName,
                    AwayTeamOdd = match.AwayTeamOdd,
                    HomeTeamScore = matchScore.HomeTeamScore,
                    AwayTeamScore = matchScore.AwayTeamScore,
                    IsExpected = isExpected,
                };

                resultDtos.Add(result);
            }

            await GenerateSeasonStatsAsync(resultDtos);

            var results = mapperly.MapToResultEntity(resultDtos);

            logger.LogDebug("Sucessfully created results!");

            await resultRepository.AddRangeAsync(results);
            await resultRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Generates season statistics based on the results of the matches.
        /// </summary>
        /// <param name="resultDtos">The results of the matches.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateSeasonStatsAsync(IEnumerable<CreateResultDto> resultDtos)
        {
            var seasonStatsDtos = new Dictionary<string, CreateSeasonStatsDto>();

            int seasonId = resultDtos.Select(s => s.SeasonId).FirstOrDefault();

            foreach (var result in resultDtos.Where(r => r.SeasonId == seasonId))
            {
                if (!seasonStatsDtos.ContainsKey(result.HomeTeamName))
                {
                    seasonStatsDtos[result.HomeTeamName] = new CreateSeasonStatsDto
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

                if (!seasonStatsDtos.ContainsKey(result.AwayTeamName))
                {
                    seasonStatsDtos[result.AwayTeamName] = new CreateSeasonStatsDto
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

                var homeStats = seasonStatsDtos[result.HomeTeamName];
                homeStats.ScoredGoals += result.HomeTeamScore;
                homeStats.ConcededGoals += result.AwayTeamScore;
                homeStats.GoalDifference = homeStats.ScoredGoals - homeStats.ConcededGoals;

                var awayStats = seasonStatsDtos[result.AwayTeamName];
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
                    homeStats.Points++;
                    awayStats.Points++;

                    homeStats.Draws++;
                    awayStats.Draws++;
                }
            }

            var seasonStats = mapperly.MapToSeasonStatsEntity(seasonStatsDtos.Values);

            logger.LogDebug("Sucessfully created season stats!");

            await seasonStatsRepository.AddRangeAsync(seasonStats);
            await seasonStatsRepository.SaveChangesAsync();

            await GenerateSeasonWinnerAsync(seasonStatsDtos.Values);
        }

        /// <summary>
        /// Generates a season winner based on the season statistics.
        /// </summary>
        /// <param name="seasonStatsDtos">The statistics for the teams in the season.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateSeasonWinnerAsync(IEnumerable<CreateSeasonStatsDto> seasonStatsDtos)
        {
            var winners = seasonStatsDtos.OrderByDescending(t => t.Points).ThenByDescending(t => t.GoalDifference).ThenByDescending(t => t.ScoredGoals).FirstOrDefault();

            var winnerDto = new CreateWinnerDto()
            {
                SeasonId = winners.SeasonId,
                WinnerTeamName = winners.TeamName,
                Points = winners.Points,
            };

            var winner = mapperly.MapToWinnerEntity(winnerDto);

            logger.LogDebug("Successfully created winner!");

            await winnerRepository.AddAsync(winner);
            await winnerRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Generates team details including weight and expected win percentage for a season.
        /// </summary>
        /// <param name="teams">The teams participating in the season.</param>
        /// <param name="seasonId">The ID of the season.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateTeamDetailsAsync(IEnumerable<TeamDto> teams, int seasonId)
        {
            var teamDetailsDto = new List<CreateTeamDetailsDto>();

            decimal totalWeight = teams.Sum(t => t.Weight);

            foreach (var team in teams)
            {
                var detail = new CreateTeamDetailsDto
                {
                    SeasonId = seasonId,
                    TeamName = team.Name,
                    TeamColor = team.Color,
                    Weight = team.Weight,
                    ExpectedWinPercentage = Math.Round(team.Weight / totalWeight * 100),
                };

                teamDetailsDto.Add(detail);
            }

            var teamDetails = mapperly.MapToTeamDetailsEntity(teamDetailsDto);

            logger.LogDebug("Successfully created team details!");

            await teamDetailsRepository.AddRangeAsync(teamDetails);
            await teamDetailsRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Calculates whether the match result was expected based on the odds and scores.
        /// </summary>
        /// <param name="homeTeamOdd">The odd for the home team.</param>
        /// <param name="awayTeamOdd">The odd for the away team.</param>
        /// <param name="homeTeamScore">The score of the home team.</param>
        /// <param name="awayTeamScore">The score of the away team.</param>
        /// <returns>
        /// A boolean indicating whether the result was expected.
        /// </returns>
        private bool CalculateIsExpectedFromOdds(decimal homeTeamOdd, decimal awayTeamOdd, int homeTeamScore, int awayTeamScore)
        {
            if (homeTeamScore == awayTeamScore && Math.Abs(homeTeamOdd - awayTeamOdd) < 1.5m)
            {
                return true;
            }

            var isHomeTeamExpectedToWin = homeTeamOdd < awayTeamOdd;

            if (homeTeamScore > awayTeamScore)
            {
                // Home team won the match
                return isHomeTeamExpectedToWin;
            }
            else if (awayTeamScore > homeTeamScore)
            {
                // Away team won the match
                return !isHomeTeamExpectedToWin;
            }
            else
            {
                return false;
            }
        }
    }
}