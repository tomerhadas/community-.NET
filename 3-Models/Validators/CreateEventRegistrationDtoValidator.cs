using FluentValidation;
using CommunityEventHub.Models.Dto;

namespace CommunityEventHub.Validators
{
    public class CreateEventRegistrationDtoValidator : AbstractValidator<CreateEventRegistrationDto>
    {
        public CreateEventRegistrationDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.EventId).GreaterThan(0);
            RuleFor(x => x.RegistrationDate).NotEmpty();
        }
    }
}
