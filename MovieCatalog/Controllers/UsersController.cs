using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.DTOs.Users;
using MovieCatalog.Repositories;
using System.Security.Claims;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var username = User.Identity?.Name;
            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new
            {
                Id = userId,
                Username = username,
                Role = role
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<UserResponseDto>>(users);

            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (userId != id && role != "Admin")
            {
                return Forbid();
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(_mapper.Map<UserResponseDto>(user));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
            {
                return NotFound("Usuário não encontrado.");
            }
            await _userRepository.SoftDeleteAsync(user);
            return NoContent();
        }
    }
}
