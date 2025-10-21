using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Mappers
{
    public class AuthorConfigMapper : Profile
    {
        public AuthorConfigMapper()
        {
            CreateMap<AuthorDTO, Author>();
            CreateMap<Author, AuthorDTO>();
        }
    }
}
