namespace MovieCatalog.DTOs.Movie
{
    public class MovieCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string GenreId { get; set; } = string.Empty;
        public string CoverImagePath { get; set; } = string.Empty;
    }
}
