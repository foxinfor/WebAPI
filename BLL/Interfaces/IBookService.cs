using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IBookService : IService<BookDTO>
    {
        Task<IEnumerable<BookDTO?>> GetBooksByYearAsync(int year, CancellationToken cancellationToken);
    }
}