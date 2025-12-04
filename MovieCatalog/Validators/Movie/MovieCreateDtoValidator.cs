using FluentValidation;

namespace MovieCatalog.DTOs.Movie
{
    public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
    {
        public MovieCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O campo 'título' é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Synopsis)
                .NotEmpty().WithMessage("O campo 'sinopse' é obrigatório.");

            RuleFor(x => x.Director)
                .NotEmpty().WithMessage("O campo 'diretor' é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.ReleaseYear)
                .InclusiveBetween(1900, DateTime.Now.Year);

            RuleFor(x => x.GenreId)
                .NotEmpty().WithMessage("O campo 'gênero' é obrigatório.");
        }
    }
}
