using FluentValidation;

namespace MovieCatalog.DTOs.Movie
{
    public class MovieUpdateDtoValidator : AbstractValidator<MovieUpdateDto>
    {
        public MovieUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O campo título é obrigatório.");

            RuleFor(x => x.Synopsis)
                .NotEmpty().WithMessage("O campo 'sinopse' é obrigatório.");

            RuleFor(x => x.Director)
                .NotEmpty().WithMessage("O campo 'diretor' é obrigatório.");

            RuleFor(x => x.ReleaseYear)
                .InclusiveBetween(1900, 2100).WithMessage("Ano de lançamento inválido.");

            RuleFor(x => x.GenreId)
                .NotEmpty().WithMessage("O campo 'gênero' é obrigatório.");

            RuleFor(x => x.CoverImagePath)
                .NotEmpty().WithMessage("O caminho da imagem de capa é obrigatório.");
        }
    }
}
