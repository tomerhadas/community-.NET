using CommunityEventHub.Models.Dto;
using FluentValidation;

namespace CommunityEventHub.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(u => u.FullName).NotEmpty().MaximumLength(100);
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
        }
    }
}
