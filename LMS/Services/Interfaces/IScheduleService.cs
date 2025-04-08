using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IScheduleService
    {
        Task CreateSchedulesForMonth(int year, int month);
        Task<List<ScheduleResponseDto>> GetSchedulesByMonthAndYearAsync(int year, int month);
    }
}
