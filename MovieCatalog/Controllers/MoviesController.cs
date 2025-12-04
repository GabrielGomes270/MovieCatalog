using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog.Data;
using MovieCatalog.DTOs.Movie;
using MovieCatalog.Entities;
using MovieCatalog.Repositories;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies(
            [FromQuery] string? title, 
            [FromQuery] int? year, 
            [FromQuery] int? genreId, 
            [FromQuery] string? orderBy, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
            var movies = await _movieRepository.GetAllMoviesAsync(title, year, genreId, orderBy, page, pageSize);
            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MovieReadDto>(movie));
        }

        [HttpPost]
        public async Task<ActionResult<MovieReadDto>> CreateMovie(MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Movie>(movieCreateDto);
            await _movieRepository.AddMovieAsync(movie);

            var readDto = _mapper.Map<MovieReadDto>(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _mapper.Map(movieUpdateDto, movie);
            await _movieRepository.UpdateMovieAsync(movie);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _movieRepository.DeleteMovieAsync(movie);
            return NoContent();
        }
    }
}
