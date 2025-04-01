using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services.Interfaces
{
    public interface IGroupService
    {
        Task<GroupResponseDTO> GetGroupByIdAsync(Guid id);
        Task<IEnumerable<GroupResponseDTO>> GetAllGroupsAsync();
        Task<GroupResponseDTO> CreateGroupAsync(GroupRequestDTO request);
        Task UpdateGroupAsync(Guid id, GroupRequestDTO request);
        Task DeleteGroupAsync(Guid id);
    }
}
