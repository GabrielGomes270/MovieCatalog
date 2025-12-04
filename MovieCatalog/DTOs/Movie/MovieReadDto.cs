namespace MovieCatalog.DTOs.Movie
{
    public class MovieReadDto
    {
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string GenreId { get; set; } = string.Empty;

        public string GenreName { get; set; } = string.Empty;
    }
}
