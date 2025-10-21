using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    internal class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public Task<Book> CreateAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _books.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult<IEnumerable<Book>>(_books);
        }

        public Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = _books.Find(b => b.Id == id);
            return Task.FromResult(book);
        }

        public Task DeleteAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var book = _books.Find(b => b.Id == entity.Id);
            if (book != null)
            {
                _books.Remove(book);
            }

            return Task.CompletedTask;
        }

        public Task<Book> UpdateAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var index = _books.FindIndex(a => a.Id == entity.Id);

            _books[index] = entity;
            return Task.FromResult(entity);
        }
    }
}
