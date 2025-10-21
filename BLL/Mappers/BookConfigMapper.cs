using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Mappers
{
    public class BookConfigMapper : Profile
    {
        public BookConfigMapper()
        {
            CreateMap<BookDTO, Book>();
            CreateMap<Book, BookDTO>();
        }
    }
}
