using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var authors = await _repository.GetAllAsync();
            return authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = a.Name,
                DateOfBirth = a.DateOfBirth
            });
        }

        public async Task<AuthorDTO?> GetByIdAsync(Guid id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;

            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth
            };
        }

        public async Task AddAsync(AuthorDTO dto)
        {
            var author = new Author
            {
                Id = dto.Id,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth
            };
            await _repository.AddAsync(author);
        }

        public async Task UpdateAsync(AuthorDTO dto)
        {
            var author = new Author
            {
                Id = dto.Id,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth
            };
            await _repository.UpdateAsync(author);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
