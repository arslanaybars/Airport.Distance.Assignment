using Ada.Api.Models;
using FluentValidation;

namespace Ada.Api.Validation
{
    public class AirportCodesValidator : AbstractValidator<AirportCodes>
    {
        public AirportCodesValidator()
        {
            RuleFor(x => x.From).NotNull()
                .WithMessage("From area could not be null!")
                .Length(3, 3)
                .WithMessage("From area must be 3 character")
                .Matches(@"^[A-Z]+$")
                .WithMessage("From area accepts capital letters");

            RuleFor(x => x.To).NotNull()
                .WithMessage("To area could not be null!")
                .Length(3, 3)
                .WithMessage("To area must be 3 character")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("To area accepts capital letters");
        }
    }
}
