using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    internal class BookRepository : IBookRepository
    {
        public Task<Book> CreateAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            InMemoryDatabase.Books.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult<IEnumerable<Book>>(InMemoryDatabase.Books);
        }

        public Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = InMemoryDatabase.Books.Find(b => b.Id == id);
            return Task.FromResult(book);
        }

        public Task DeleteAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = InMemoryDatabase.Books.Find(b => b.Id == entity.Id);
            if (book != null)
            {
                InMemoryDatabase.Books.Remove(book);
            }

            return Task.CompletedTask;
        }

        public Task<Book> UpdateAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var index = InMemoryDatabase.Books.FindIndex(b => b.Id == entity.Id);
            if (index >= 0)
            {
                InMemoryDatabase.Books[index] = entity;
            }

            return Task.FromResult(entity);
        }
    }
}
