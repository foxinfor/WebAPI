using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    internal class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Books.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task DeleteAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _context.Books.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Book> UpdateAsync(Book entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _context.Books.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
