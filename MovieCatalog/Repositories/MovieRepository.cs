using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Entities;

namespace MovieCatalog.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync(string? title, int? year, int? genreId, string? orderBy, int page, int pageSize)
        {
            var query = _context.Movies.Include(x => x.Genre).AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            if (year.HasValue)
            {
                query = query.Where(x => x.ReleaseYear == year.Value);
            }

            if (genreId.HasValue)
            {
                query = query.Where(x => x.GenreId == genreId.Value);
            }

            query = orderBy?.ToLower() switch
            {
                "title" => query.OrderBy(x => x.Title),
                "title_desc" => query.OrderByDescending(x => x.Title),
                "year" => query.OrderBy(x => x.ReleaseYear),
                "year_desc" => query.OrderByDescending(x => x.ReleaseYear),
                _ => query.OrderBy(x => x.Id)
            };

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public Task<Movie?> GetMovieByIdAsync(int id)
        {
            return _context.Movies.Include(x => x.Genre).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Movies.AnyAsync(x => x.Id == id);
        }

    }
}
