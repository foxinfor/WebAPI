using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly List<Author> _authors = new();

        public Task<IEnumerable<Author>> GetAllAsync()
        {
            return Task.FromResult(_authors.AsEnumerable());
        }

        public Task<Author?> GetByIdAsync(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(author);
        }

        public Task AddAsync(Author entity)
        {
            _authors.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Author entity)
        {
            var index = _authors.FindIndex(a => a.Id == entity.Id);
            if (index >= 0)
                _authors[index] = entity;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author != null)
                _authors.Remove(author);

            return Task.CompletedTask;
        }
    }
}
