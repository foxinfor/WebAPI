namespace BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T createDto);
        Task UpdateAsync(T dto);
        Task DeleteAsync(Guid id);   
    }
}
