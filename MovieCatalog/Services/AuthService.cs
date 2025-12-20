using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MovieCatalog.Configurations;
using MovieCatalog.DTOs.Authentication;
using MovieCatalog.Entities;
using MovieCatalog.Repositories;
using MovieCatalog.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        if (await _userRepository.ExistsByUsernameAsync(dto.Username))
        {
            return new AuthResponseDto
            {
                IsAuthenticated = false,
                Message = "Username já está em uso."
            };
        }

        if (await _userRepository.ExistsByEmailAsync(dto.Email))
        {
            return new AuthResponseDto
            {
                IsAuthenticated = false,
                Message = "Email já está em uso."
            };
        }

        var user = new User
        {
            Username = dto.Username,
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        await _userRepository.AddAsync(user);

        return await GenerateAuthSuccessResponse(user, "Usuário registrado com sucesso!");
    }


    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByIdentifierAsync(dto.Identifier);
        if (user == null)
        {
            return new AuthResponseDto
            {
                IsAuthenticated = false,
                Message = "Credenciais inválidas."
            };
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return new AuthResponseDto
            {
                IsAuthenticated = false,
                Message = "Credenciais inválidas."
            };
        }

        var (token,expiresAt) = JwtTokenGenerator.GenerateAccessToken(user, _jwtSettings);

        var refreshToken = JwtTokenGenerator.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        await _userRepository.UpdateAsync(user);

        return new AuthResponseDto
        {
            IsAuthenticated = true,
            Message = "Login realizado com sucesso!",
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Tokens = new TokenResponseDto
            {
                AccessToken = token,
                RefreshToken = JwtTokenGenerator.GenerateRefreshToken(),
                ExpiresAt = expiresAt
            }
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new AuthResponseDto
            {
                IsAuthenticated = false,
                Message = "Token de atualização inválido ou expirado."
            };
        }

        var (newToken, expiresAt) = JwtTokenGenerator.GenerateAccessToken(user, _jwtSettings);

        var newRefreshToken = JwtTokenGenerator.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        await _userRepository.UpdateAsync(user);

        return new AuthResponseDto {

            IsAuthenticated = true,
            Message = "Token atualizado com sucesso!",
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Tokens = new TokenResponseDto
            {
                AccessToken = newToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = expiresAt
            }
        };
    }


    private Task<AuthResponseDto> GenerateAuthSuccessResponse(User user, string message)
    {
        var (accessToken, expiresAt) = JwtTokenGenerator.GenerateAccessToken(user, _jwtSettings);
        var refresh = JwtTokenGenerator.GenerateRefreshToken();

        return Task.FromResult(new AuthResponseDto
        {
            IsAuthenticated = true,
            Message = message,
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Tokens = new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refresh,
                ExpiresAt = expiresAt
            }
        });
    }
}
