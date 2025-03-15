using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IBatchRepository
    {
        Task<Batch> GetByIdAsync(Guid id);
        Task<IEnumerable<Batch>> GetAllAsync();
        Task AddAsync(Batch batch);
        Task UpdateAsync(Batch batch);
        Task DeleteAsync(Guid id);
    }
}
