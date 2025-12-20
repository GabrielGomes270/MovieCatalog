using FluentValidation;
using MovieCatalog.DTOs.Authentication;

namespace MovieCatalog.Validators.Authentication
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        }
    }
}
