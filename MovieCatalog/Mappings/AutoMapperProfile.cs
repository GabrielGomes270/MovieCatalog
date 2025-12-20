using AutoMapper;
using MovieCatalog.DTOs.Genre;
using MovieCatalog.DTOs.Movie;
using MovieCatalog.DTOs.Users;
using MovieCatalog.Entities;
namespace MovieCatalog.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, MovieReadDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();

            CreateMap<Genre, GenreReadDto>();
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();

            CreateMap<User, UserResponseDto>();
        }
    }
}
