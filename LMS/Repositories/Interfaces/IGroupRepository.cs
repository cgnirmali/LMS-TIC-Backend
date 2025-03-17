using LMS.DB.Entities;



namespace LMS.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetByIdAsync(Guid id);
        Task<IEnumerable<Group>> GetAllAsync();
        Task AddAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(Guid id);
    }
}
