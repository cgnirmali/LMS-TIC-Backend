using LMS.DB.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
