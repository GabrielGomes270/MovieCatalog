using Microsoft.EntityFrameworkCore;
using MovieCatalog.Entities;
namespace MovieCatalog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Movie> Movies { get; set; }
        public DbSet<Entities.Genre> Genres { get; set; }
        public DbSet<Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Genre>().HasData(
                new Entities.Genre { Id = 1, Name = "Action" },
                new Entities.Genre { Id = 2, Name = "Comedy" },
                new Entities.Genre { Id = 3, Name = "Drama" },
                new Entities.Genre { Id = 4, Name = "Horror" },
                new Entities.Genre { Id = 5, Name = "Science Fiction" }
            );
            modelBuilder.Entity<Entities.Movie>().HasData(
                new Entities.Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    Director = "Christopher Nolan",
                    ReleaseYear = 2010,
                    GenreId = 5,
                    CoverImagePath = null
                },
                new Entities.Movie
                {
                    Id = 2,
                    Title = "The Dark Knight",
                    Synopsis = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    Director = "Christopher Nolan",
                    ReleaseYear = 2008,
                    GenreId = 1,
                    CoverImagePath = null
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "temp",
                    Role = "Admin"
                }
            );

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
