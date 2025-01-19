using Spanish.Football.League.Common.Models;
using System.Net;
using System.Net.Http.Json;
using Spanish.Football.League.IntegrationTests.Utils;
using Spanish.Football.League.Common.Enums;
using Spanish.Football.League.Common;

namespace Spanish.Football.League.IntegrationTests
{
    public class SeasonControllerTests(WebAppFactory factory) : BaseTest(factory)
    {
        private readonly HttpClient _client = factory.CreateDefaultClient();

        private static int seasonYear = 2000;

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenSeasonYearIsInvalid()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = TestConstants.InvalidSeasonYear,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.InvalidSeasonYearMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenNumberOfTeamsIsOutOfRange()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.InvalidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.DefaultValue,
                NumberOfWeakTeams = TestConstants.DefaultValue,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.InvalidNumberOfTeamsMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenNumberOfTeamsIsOdd()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.OddNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.InvalidNumberOfTeamsMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenNumberOfStrongTeamsIsInvalid()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.NegativeValue,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.InvalidStrongTeamsMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenNumberOfWeakTeamsIsInvalid()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.NegativeValue,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.InvalidWeakTeamsMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenNumberOfStrongTeamsExceedsNumberOfTeams()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ExceedingValue,
                NumberOfWeakTeams = TestConstants.DefaultValue,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.StrongTeamsExceedTeamsMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenNumberOfWeakTeamsExceedsNumberOfTeams()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.DefaultValue,
                NumberOfWeakTeams = TestConstants.ExceedingValue,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.WeakTeamsExceedTeamsMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_ReturnBadRequest_WhenSumOfStrongAndWeakTeamsExceedsNumberOfTeams()
        {
            var invalidRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ExceedingValue,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, invalidRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(TestConstants.SumExceedsTeamsMessage, responseContent);
        }

        [Fact]
        public async Task CreateSeason_FillsMatchesTables_WithValidInput()
        {
            var validRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, validRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var seasonResponse = await response.Content.ReadAsStringAsync();
            var seasonId = int.Parse(seasonResponse);

            var matches = DbContext.Matches.Where(m => m.SeasonId == seasonId).ToList();
            Assert.NotEmpty(matches);
            Assert.Equal(TestConstants.TotalMatches, matches.Count);

            var firstHalfMatches = matches.Where(m => m.SeasonHalf == SeasonHalvesEnum.Spring).ToList();
            var secondHalfMatches = matches.Where(m => m.SeasonHalf == SeasonHalvesEnum.Fall).ToList();

            Assert.Equal(TestConstants.MatchesPerHalf, firstHalfMatches.Count);
            Assert.Equal(TestConstants.MatchesPerHalf, secondHalfMatches.Count);

            var teamMatchCounts = new Dictionary<string, (int Home, int Away)>();
            foreach (var match in matches)
            {
                if (!teamMatchCounts.ContainsKey(match.HomeTeamName))
                {
                    teamMatchCounts[match.HomeTeamName] = (TestConstants.DefaultValue, TestConstants.DefaultValue);
                }

                if (!teamMatchCounts.ContainsKey(match.AwayTeamName))
                {
                    teamMatchCounts[match.AwayTeamName] = (TestConstants.DefaultValue, TestConstants.DefaultValue);
                }

                teamMatchCounts[match.HomeTeamName] = (
                    teamMatchCounts[match.HomeTeamName].Home + 1,
                    teamMatchCounts[match.HomeTeamName].Away
                );

                teamMatchCounts[match.AwayTeamName] = (
                    teamMatchCounts[match.AwayTeamName].Home,
                    teamMatchCounts[match.AwayTeamName].Away + 1
                );
            }

            foreach (var team in teamMatchCounts)
            {
                Assert.Equal(TestConstants.HomeMatchesPerTeam, team.Value.Home);
                Assert.Equal(TestConstants.AwayMatchesPerTeam, team.Value.Away);
                Assert.Equal(TestConstants.MatchesPerTeam, team.Value.Home + team.Value.Away);
            }
        }

        [Fact]
        public async Task CreateSeason_FillsResultsTable_WithValidInput()
        {
            var validRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, validRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var seasonResponse = await response.Content.ReadAsStringAsync();
            var seasonId = int.Parse(seasonResponse);

            var results = DbContext.Results.Where(r => r.SeasonId == seasonId).ToList();
            Assert.NotEmpty(results);

            Assert.Equal(TestConstants.TotalMatches, results.Count);

            Assert.All(results, result =>
            {
                Assert.True(result.HomeTeamScore >= TestConstants.DefaultValue);
                Assert.True(result.AwayTeamScore >= TestConstants.DefaultValue);
            });

            var firstHalfResults = results.Where(r => r.SeasonHalf == SeasonHalvesEnum.Spring).ToList();
            var secondHalfResults = results.Where(r => r.SeasonHalf == SeasonHalvesEnum.Fall).ToList();
            Assert.Equal(TestConstants.MatchesPerHalf, firstHalfResults.Count);
            Assert.Equal(TestConstants.MatchesPerHalf, secondHalfResults.Count);

            if (results.Any(r => r.IsExpected.HasValue))
            {
                Assert.All(results, result =>
                {
                    Assert.NotNull(result.IsExpected);
                });
            }
        }

        [Fact]
        public async Task CreateSeason_FillsSeasonStatsTable_WithValidInput()
        {
            var validRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, validRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var seasonResponse = await response.Content.ReadAsStringAsync();
            var seasonId = int.Parse(seasonResponse);

            var seasonStats = DbContext.SeasonStats.Where(s => s.SeasonId == seasonId).ToList();
            Assert.NotEmpty(seasonStats);

            Assert.Equal(TestConstants.ValidNumberOfTeams, seasonStats.Count);

            foreach (var stat in seasonStats)
            {
                Assert.NotNull(stat.TeamName);
                Assert.True(stat.ScoredGoals >= TestConstants.DefaultValue);
                Assert.True(stat.ConcededGoals >= TestConstants.DefaultValue);
                Assert.Equal(stat.ScoredGoals - stat.ConcededGoals, stat.GoalDifference);
                Assert.True(stat.Points >= TestConstants.DefaultValue);
            }
        }

        [Fact]
        public async Task CreateSeason_FillsTeamDetailsTable_WithValidInput()
        {
            var validRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, validRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var seasonResponse = await response.Content.ReadAsStringAsync();
            var seasonId = int.Parse(seasonResponse);

            var teamDetails = DbContext.TeamDetails.Where(t => t.SeasonId == seasonId).ToList();
            Assert.NotEmpty(teamDetails);
            Assert.Equal(TestConstants.ValidNumberOfTeams, teamDetails.Count);

            int strongTeamsCount = TestConstants.DefaultValue;
            int weakTeamsCount = TestConstants.DefaultValue;
            int remainingTeamsCount = TestConstants.DefaultValue;

            foreach (var team in teamDetails)
            {
                if (team.Weight >= GameConstants.StrongTeamsMinWeight && team.Weight <= GameConstants.StrongTeamsMaxWeight)
                {
                    strongTeamsCount++;
                }
                else if (team.Weight >= GameConstants.WeakTeamsMinWeight && team.Weight <= GameConstants.WeakTeamsMaxWeight)
                {
                    weakTeamsCount++;
                }
                else if (team.Weight >= GameConstants.WeakTeamsMaxWeight + 0.1m && team.Weight <= GameConstants.StrongTeamsMinWeight - 0.1m)
                {
                    remainingTeamsCount++;
                }
            }

            Assert.Equal(TestConstants.ValidNumberOfStrongTeams, strongTeamsCount);
            Assert.Equal(TestConstants.ValidNumbersOfWeakTeams, weakTeamsCount);
            Assert.Equal(TestConstants.RemainingTeams, remainingTeamsCount);
        }

        [Fact]
        public async Task CreateSeason_FillsWinnersTable_WithValidInput()
        {
            var validRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, validRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var seasonResponse = await response.Content.ReadAsStringAsync();
            var seasonId = int.Parse(seasonResponse);

            var winner = DbContext.Winners.Where(w => w.SeasonId == seasonId).ToList();
            Assert.NotEmpty(winner);
            Assert.Single(winner);
        }

        [Fact]
        public async Task CreateSeason_FillsSeasonTable_WithValidInput()
        {
            var validRequest = new CreateSeasonRequestDto()
            {
                SeasonYear = seasonYear++,
                NumberOfTeams = TestConstants.ValidNumberOfTeams,
                NumberOfStrongTeams = TestConstants.ValidNumberOfStrongTeams,
                NumberOfWeakTeams = TestConstants.ValidNumbersOfWeakTeams,
            };

            var response = await _client.PostAsJsonAsync(TestConstants.ApiUrl, validRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var seasonResponse = await response.Content.ReadAsStringAsync();
            var seasonId = int.Parse(seasonResponse);

            var season = DbContext.Winners.Where(w => w.SeasonId == seasonId).ToList();
            Assert.NotEmpty(season);
            Assert.Single(season);
        }

        [Fact]
        public async Task GetSeasonResultsAsync_ReturnsBadRequestWithIdEqualToZero()
        {
            var seasonResultResponseUrl = $"{TestConstants.ApiUrl}/results/{TestConstants.DefaultValue}";
            var response = await _client.GetAsync(seasonResultResponseUrl);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(TestConstants.InvalidSeasonIdMessage, responseAsString);
        }

        [Fact]
        public async Task GetSeasonResultsAsync_ReturnsBadRequestWithInvalidId()
        {
            var seasonResultResponseUrl = $"{TestConstants.ApiUrl}/{TestConstants.NegativeValue}/leaderboard";
            var response = await _client.GetAsync(seasonResultResponseUrl);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(TestConstants.InvalidSeasonIdMessage, responseAsString);
        }

        [Fact]
        public async Task GetSeasonStatsAsync_ReturnsBadRequestWithIdEqualToZero()
        {
            var seasonResultResponseUrl = $"{TestConstants.ApiUrl}/{TestConstants.DefaultValue}/leaderboard";
            var response = await _client.GetAsync(seasonResultResponseUrl);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(TestConstants.InvalidSeasonIdMessage, responseAsString);
        }

        [Fact]
        public async Task GetSeasonStatsAsync_ReturnsBadRequestWithInvalidId()
        {
            var seasonResultResponseUrl = $"{TestConstants.ApiUrl}/{TestConstants.NegativeValue}/leaderboard";
            var response = await _client.GetAsync(seasonResultResponseUrl);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(TestConstants.InvalidSeasonIdMessage, responseAsString);
        }

        [Fact]
        public async Task GetTeamDetailsAsync_ReturnsBadRequestWithIdEqualToZero()
        {
            var seasonResultResponseUrl = $"{TestConstants.ApiUrl}/{TestConstants.DefaultValue}/teamdetails";
            var response = await _client.GetAsync(seasonResultResponseUrl);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(TestConstants.InvalidSeasonIdMessage, responseAsString);
        }

        [Fact]
        public async Task GetTeamDetailsAsync_ReturnsBadRequestWithInvalidId()
        {
            var seasonResultResponseUrl = $"{TestConstants.ApiUrl}/{TestConstants.NegativeValue}/teamdetails";
            var response = await _client.GetAsync(seasonResultResponseUrl);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(TestConstants.InvalidSeasonIdMessage, responseAsString);
        }
    }
}
