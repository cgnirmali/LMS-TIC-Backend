using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Services.Implementation;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly INotifictionService _notificationService;

        public NotificationController(INotifictionService notificationService)
        {
            _notificationService = notificationService;
        }

        // POST api/notification/send
        [HttpPost("send")]
        public async Task<IActionResult> SendNotificationAsync([FromBody] NotificationRequest dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Message))
            {
                return BadRequest("Invalid notification data.");
            }

            var result = await _notificationService.SendNotificationAsync(dto.ReceiverId, dto.Message, dto.NotificationType);

            if (!result)
            {
                return NotFound("User not found.");
            }

            return Ok("Notification sent successfully.");
        }

        // GET api/notification/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotificationsByUserIdAsync(Guid userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            if (notifications == null || notifications.Count == 0)
            {
                return NotFound("No notifications found.");
            }

            var notificationDtos = notifications.Select(n => new NotificationResponse
            {
                Id = n.Id,
                CreatedDate = n.CreatedDate,
                ReceiverId = n.ReceiverId,
                Message = n.Message,
                NotificationType = n.NotificationType
            }).ToList();

            return Ok(notificationDtos);
        }

        // GET api/notification/{notificationId}
        [HttpGet("notification/{notificationId}")]
        public async Task<IActionResult> GetNotificationByIdAsync(Guid notificationId)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(notificationId);
            if (notification == null)
            {
                return NotFound("Notification not found.");
            }

            var notificationDto = new NotificationResponse
            {
                Id = notification.Id,
                CreatedDate = notification.CreatedDate,
                ReceiverId = notification.ReceiverId,
                Message = notification.Message,
                NotificationType = notification.NotificationType
            };

            return Ok(notificationDto);
        }

        // DELETE api/notification/{notificationId}
        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotificationAsync(Guid notificationId)
        {
            var result = await _notificationService.DeleteNotificationAsync(notificationId);
            if (!result)
            {
                return NotFound("Notification not found.");
            }

            return Ok("Notification deleted successfully.");
        }

        // PUT api/notification/{notificationId}
        [HttpPut("{notificationId}")]
        public async Task<IActionResult> UpdateNotificationAsync(Guid notificationId, [FromBody] UpdateNotification dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Message))
            {
                return BadRequest("Invalid notification data.");
            }

            var result = await _notificationService.UpdateNotificationAsync(notificationId, dto.Message, dto.NotificationType);

            if (!result)
            {
                return NotFound("Notification not found.");
            }

            return Ok("Notification updated successfully.");
        }
    }
}