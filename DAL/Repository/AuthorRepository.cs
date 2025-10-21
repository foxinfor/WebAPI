using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    internal class AuthorRepository : IAuthorRepository
    {
        private readonly List<Author> _authors = new();

        public Task<Author> CreateAsync(Author entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _authors.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult<IEnumerable<Author>>(_authors);
        }

        public Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var author = _authors.Find(a => a.Id == id);
            return Task.FromResult(author);
        }

        public Task DeleteAsync(Author entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var author = _authors.Find(a => a.Id == entity.Id);
            if (author != null)
            {
                _authors.Remove(author);
            }

            return Task.CompletedTask;
        }

        public Task<Author> UpdateAsync(Author entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var index = _authors.FindIndex(a => a.Id == entity.Id);

            _authors[index] = entity;
            return Task.FromResult(entity);
        }
    }
}
