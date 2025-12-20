namespace MovieCatalog.DTOs.Authentication
{
    public class AuthResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }

        public TokenResponseDto Tokens { get; set; } = new();
    }
}
