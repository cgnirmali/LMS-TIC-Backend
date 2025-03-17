using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

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
            if (group == null)
            {
                // Handle the case where the group is not found
                return null;
            }

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
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                // You can throw an exception or handle the invalid input as per the business logic
                throw new ArgumentException("Group name cannot be empty.");
            }

            var group = new Group  // Use the correct Group class here
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                CourseId = request.CourseId,
                Name = request.Name
            };

            await _groupRepository.AddAsync(group);

            return new GroupResponseDTO
            {
                Id = group.Id,
                CreatedDate = group.CreatedDate,
                CourseId = request.CourseId,
                Name = group.Name
            };
        }

        public async Task UpdateGroupAsync(Guid id, GroupRequestDTO request)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group != null)
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    throw new ArgumentException("Group name cannot be empty.");
                }

                group.Name = request.Name;
                await _groupRepository.UpdateAsync(group);
            }
            else
            {
                // Handle case when group is not found
                throw new KeyNotFoundException("Group not found.");
            }
        }

        public async Task DeleteGroupAsync(Guid id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group != null)
            {
                await _groupRepository.DeleteAsync(id);
            }
            else
            {
                // Handle case when group is not found
                throw new KeyNotFoundException("Group not found.");
            }
        }
    }
}
