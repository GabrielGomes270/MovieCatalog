using MovieCatalog.Entities;

namespace MovieCatalog.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdentifierAsync(string identifier);

        Task<User?> GetByRefreshTokenAsync(string refreshToken);

        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task SoftDeleteAsync(User user);
    }
}
