using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupResponseDTO>> GetGroup(Guid id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupResponseDTO>>> GetGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpPost]
        public async Task<ActionResult<GroupResponseDTO>> CreateGroup(GroupRequestDTO request)
        {
            var group = await _groupService.CreateGroupAsync(request);
            return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, group);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, GroupRequestDTO request)
        {
            await _groupService.UpdateGroupAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            await _groupService.DeleteGroupAsync(id);
            return NoContent();
        }
    }
}
