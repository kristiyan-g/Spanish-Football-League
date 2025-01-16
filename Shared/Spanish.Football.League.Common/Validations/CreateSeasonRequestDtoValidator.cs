namespace Spanish.Football.League.Common.Validations
{
    using FluentValidation;
    using Spanish.Football.League.Common.Models;

    /// <summary>
    /// Validates user input, when creating season.
    /// </summary>
    public class CreateSeasonRequestDtoValidator : AbstractValidator<CreateSeasonRequestDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSeasonRequestDtoValidator"/> class.
        /// Defines the validation rules for the CreateSeasonRequestDto model.
        /// </summary>
        public CreateSeasonRequestDtoValidator()
        {
            // Rule for SeasonYear to be a 4-digit number
            RuleFor(x => x.SeasonYear)
                .InclusiveBetween(1000, 9999)
                .WithMessage("Season year must be a valid 4-digit year.");

            // Rule for NumberOfTeams to be even and between 2 and 20
            RuleFor(x => x.NumberOfTeams)
                .InclusiveBetween(2, 20)
                .Must(x => x % 2 == 0)
                .WithMessage("Number of teams must be an even number between 2 and 20.");

            // Rule for the total number of teams (strong + weak) should not exceed total teams
            RuleFor(x => x.NumberOfStrongTeams + x.NumberOfWeakTeams)
                .LessThanOrEqualTo(x => x.NumberOfTeams)
                .WithMessage("The total number of strong and weak teams cannot exceed the total number of teams.");

            // Rule for NumberOfStrongTeams to be less than or equal to NumberOfTeams
            RuleFor(x => x.NumberOfStrongTeams)
                .LessThanOrEqualTo(x => x.NumberOfTeams)
                .WithMessage("Number of strong teams cannot exceed the total number of teams.");

            // Rule for NumberOfWeakTeams to be less than or equal to NumberOfTeams
            RuleFor(x => x.NumberOfWeakTeams)
                .LessThanOrEqualTo(x => x.NumberOfTeams)
                .WithMessage("Number of weak teams cannot exceed the total number of teams.");

            // Rule for NumberOfStrongTeams and NumberOfWeakTeams to be non-negative
            RuleFor(x => x.NumberOfStrongTeams)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Number of strong teams cannot be negative.");

            RuleFor(x => x.NumberOfWeakTeams)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Number of weak teams cannot be negative.");
        }
    }
}
