using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            return Task.FromResult(_books.AsEnumerable());
        }

        public Task<Book?> GetByIdAsync(Guid id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }

        public Task AddAsync(Book entity)
        {
            _books.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Book entity)
        {
            var index = _books.FindIndex(b => b.Id == entity.Id);
            if (index >= 0)
                _books[index] = entity;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
                _books.Remove(book);

            return Task.CompletedTask;
        }
    }
}
