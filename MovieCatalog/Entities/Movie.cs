namespace MovieCatalog.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        public string? CoverImagePath { get; set; }
    }
}
