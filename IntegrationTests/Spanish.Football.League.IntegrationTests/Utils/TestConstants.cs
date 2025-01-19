namespace Spanish.Football.League.IntegrationTests.Utils
{
    using System.Runtime.CompilerServices;

    public class TestConstants
    {
        // Endpoint
        public const string ApiUrl = "api/season";

        // Error Messages
        public const string InvalidSeasonYearMessage = "Season year must be a valid 4-digit year.";
        public const string InvalidNumberOfTeamsMessage = "Number of teams must be an even number between 2 and 20.";
        public const string InvalidStrongTeamsMessage = "Number of strong teams cannot be negative.";
        public const string InvalidWeakTeamsMessage = "Number of weak teams cannot be negative.";
        public const string StrongTeamsExceedTeamsMessage = "Number of strong teams cannot exceed the total number of teams.";
        public const string WeakTeamsExceedTeamsMessage = "Number of weak teams cannot exceed the total number of teams.";
        public const string SumExceedsTeamsMessage = "The total number of strong and weak teams cannot exceed the total number of teams.";
        public const string InvalidSeasonIdMessage = "Season ID must be greater than 0!";

        // Test Input Values
        public const int DefaultValue = 0;
        public const int InvalidSeasonYear = 999;
        public const int NegativeValue = -1;
        public const int InvalidNumberOfTeams = 1;
        public const int OddNumberOfTeams = 5;
        public const int ExceedingValue = ValidNumberOfTeams + 1;

        public const int ValidNumberOfTeams = 20;
        public const int ValidNumberOfStrongTeams = 9;
        public const int ValidNumbersOfWeakTeams = 9;

        public static int RemainingTeams => ValidNumberOfTeams - (ValidNumberOfStrongTeams + ValidNumbersOfWeakTeams);

        public static int TotalMatches => ValidNumberOfTeams * (ValidNumberOfTeams - 1);

        public static int MatchesPerHalf => TotalMatches / 2;

        public static int MatchesPerTeam => (TotalMatches / ValidNumberOfTeams) * 2;

        public static int HomeMatchesPerTeam => MatchesPerTeam / 2;

        public static int AwayMatchesPerTeam => MatchesPerTeam / 2;
    }
}
