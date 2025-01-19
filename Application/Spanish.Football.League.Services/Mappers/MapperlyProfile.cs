namespace Spanish.Football.League.Services.Mappers
{
    using Riok.Mapperly.Abstractions;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// The MapperlyProfile class configures mapping rules between the Dto and
    /// Entity objects using the Mapperly library.
    /// </summary>
    [Mapper]
    public partial class MapperlyProfile
    {
        public partial IEnumerable<TeamDetailsResponseDto> MapToTeamDetailsResponse(IEnumerable<TeamDetails> teamDetails);

        public partial IEnumerable<SeasonStatsResponseDto> MapToSeasonStatsResponse(IEnumerable<SeasonStats> seasonStats);

        public partial IEnumerable<TeamDetails> MapToTeamDetailsEntity(IEnumerable<CreateTeamDetailsDto> teamDetailsDtos);

        public partial Winner MapToWinnerEntity(CreateWinnerDto winnerDto);

        public partial IEnumerable<SeasonStats> MapToSeasonStatsEntity(IEnumerable<CreateSeasonStatsDto> seasonStatsDtos);

        public partial IEnumerable<Result> MapToResultEntity(IEnumerable<CreateResultDto> resultDtos);

        public partial IEnumerable<Match> MapToMatchEntity(IEnumerable<CreateMatchDto> matchDtos);

        public partial IEnumerable<TeamDto> MapToTeamDto(IEnumerable<Team> teams);

        public partial SeasonResultsResponseDto MapToSeasonResultsResponse(Season season, IEnumerable<Result> results, Winner winner);
    }
}
