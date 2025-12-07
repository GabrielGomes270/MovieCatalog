using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.DTOs.Genre;
using MovieCatalog.Entities;
using MovieCatalog.Repositories;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenresController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres(
            [FromQuery] string? name,
            [FromQuery] string? orderBy = "id")
        {
            var genres = await _genreRepository.GetAllGenresAsync(name, orderBy);
            return Ok(_mapper.Map<IEnumerable<GenreReadDto>>(genres));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenreReadDto>(genre));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreateDto genreCreateDto)
        {
            var genre = _mapper.Map<Genre>(genreCreateDto);
            await _genreRepository.AddGenreAsync(genre);

            var genreReadDto = _mapper.Map<GenreReadDto>(genre);
            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genreReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDto genreUpdateDto)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            _mapper.Map(genreUpdateDto, genre);
            await _genreRepository.UpdateGenreAsync(genre);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            await _genreRepository.DeleteGenreAsync(genre);

            return NoContent();
        }
    }
}
