using AutoMapper;
using BooksWebAPI.Models;

namespace BooksWebAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
