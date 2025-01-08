namespace Spanish.Football.League.Services.Mappers
{
    using Riok.Mapperly.Abstractions;
    using Spanish.Football.League.Common.Models;
    using Spanish.Football.League.DomainModels;

    [Mapper]
    public partial class MapperlyProfile
    {
        public partial TeamResponseDto MapToTeamResponseDto(Team team);

        public partial IEnumerable<TeamResponseDto> MapToResponseDtoList(IEnumerable<Team> teams);

        public partial IEnumerable<TeamDto> MapToTeamDtoList(IEnumerable<Team> teams);
    }
}
