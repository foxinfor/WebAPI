using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            return books.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                PublishedYear = b.PublishedYear,
                AuthorId = b.AuthorId
            });
        }

        public async Task<BookDTO?> GetByIdAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return null;

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId
            };
        }

        public async Task AddAsync(BookDTO dto)
        {
            var book = new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };
            await _repository.AddAsync(book);
        }

        public async Task UpdateAsync(BookDTO dto)
        {
            var book = new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };
            await _repository.UpdateAsync(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
