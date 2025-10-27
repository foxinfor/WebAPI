using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    internal class AuthorRepository : IAuthorRepository
    {
        public Task<Author> CreateAsync(Author entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            InMemoryDatabase.Authors.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult<IEnumerable<Author>>(InMemoryDatabase.Authors);
        }

        public Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();


            var author = InMemoryDatabase.Authors.Find(a => a.Id == id);
            return Task.FromResult(author);
        }

        public Task DeleteAsync(Author entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var author = InMemoryDatabase.Authors.Find(a => a.Id == entity.Id);
            if (author != null)
            {
                InMemoryDatabase.Authors.Remove(author);
            }

            return Task.CompletedTask;
        }

        public Task<Author> UpdateAsync(Author entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var index = InMemoryDatabase.Authors.FindIndex(a => a.Id == entity.Id);
            if (index >= 0)
            {
                InMemoryDatabase.Authors[index] = entity;
            }
            return Task.FromResult(entity);
        }
    }
}
