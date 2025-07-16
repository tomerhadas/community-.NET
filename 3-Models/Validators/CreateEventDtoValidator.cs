using CommunityEventHub.Models.Dto;
using FluentValidation;

namespace CommunityEventHub.Validators
{
    public class CreateEventDtoValidator : AbstractValidator<CreateEventDto>
    {
        public CreateEventDtoValidator()
        {
            RuleFor(e => e.Title).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
            RuleFor(e => e.Date).NotEmpty();
            RuleFor(e => e.Location).NotEmpty();
            RuleFor(e => e.EventType).NotEmpty();
        }
    }
}
