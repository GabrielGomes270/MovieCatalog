using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Entities;

namespace MovieCatalog.Repositories
{
    public class GenreRepository : IGenreRepository
    {

        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync(string? name, string? orderBy)
        {
            var query = _context.Genres.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(g => g.Name.Contains(name));
            }

            query = orderBy?.ToLower() switch
            {
                "name" => query.OrderBy(g => g.Name),
                "name_desc" => query.OrderByDescending(g => g.Name),
                _ => query.OrderBy(g => g.Id)
            };

            return await query.ToListAsync();
        }

        public Task<Genre?> GetGenreByIdAsync(int id)
        {
            return _context.Genres.FindAsync(id).AsTask();
        }

        public async Task AddGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
