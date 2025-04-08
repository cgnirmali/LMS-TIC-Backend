using LMS.DB.Entities;
using LMS.DTOs.ResponseModel;

namespace LMS.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Task AddSchedulesAsync(List<Schedule> schedules);

        Task<List<ScheduleResponseDto>> GetSchedulesByMonthAndYearAsync(int year, int month);
    }
}
