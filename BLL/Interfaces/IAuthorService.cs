using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IAuthorService : IService<AuthorDTO>
    {
        Task<IEnumerable<AuthorWithCountBooks?>> GetAuthorsWithCountBookAsync(CancellationToken cancellationToken);
        Task<AuthorDTO?> GetAuthorByNameAsync(string name, CancellationToken cancellationToken);
    }
}   