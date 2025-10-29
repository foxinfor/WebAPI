using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByYearAsync(int year, CancellationToken cancellationToken);

    }
}