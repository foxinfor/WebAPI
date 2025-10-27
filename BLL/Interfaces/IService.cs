namespace BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<T> AddAsync(T createDto, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T dto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);   
    }
}
