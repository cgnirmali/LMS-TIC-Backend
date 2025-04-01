using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS.Services.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<GroupResponseDTO> GetGroupByIdAsync(Guid id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            return new GroupResponseDTO
            {
                Id = group.Id,
                CreatedDate = group.CreatedDate,
                Name = group.Name
            };
        }

        public async Task<IEnumerable<GroupResponseDTO>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAllAsync();
            return groups.Select(group => new GroupResponseDTO
            {
                Id = group.Id,
                CreatedDate = group.CreatedDate,
                Name = group.Name
            });
        }

        public async Task<GroupResponseDTO> CreateGroupAsync(GroupRequestDTO request)
        {
            var group = new Group
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Name = request.Name
            };

            await _groupRepository.AddAsync(group);

            return new GroupResponseDTO
            {
                Id = group.Id,
                CreatedDate = group.CreatedDate,
                Name = group.Name
            };
        }

        public async Task UpdateGroupAsync(Guid id, GroupRequestDTO request)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group != null)
            {
                group.Name = request.Name;
                await _groupRepository.UpdateAsync(group);
            }
        }

        public async Task DeleteGroupAsync(Guid id)
        {
            await _groupRepository.DeleteAsync(id);
        }
    }
}
