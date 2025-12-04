using MovieCatalog.Entities;

namespace MovieCatalog.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync(string? title, int? year, int? genreId,
                                             string? orderBy, int page, int pageSize);
        Task<Movie?> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(Movie movie);
        Task<bool> ExistsAsync(int id);
    }
}
