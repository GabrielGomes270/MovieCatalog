using FluentValidation;
using MovieCatalog.DTOs.Genre;

namespace MovieCatalog.Validators.Genre
{
    public class GenreCreateDtoValidator : AbstractValidator<GenreCreateDto>
    {
        public GenreCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo 'nome' é obrigatório.")
                .MaximumLength(50).WithMessage("O campo 'nome' deve ter no máximo 50 caracteres.");
        }
    }
}
