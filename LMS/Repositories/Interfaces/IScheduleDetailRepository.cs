using LMS.DB.Entities;

namespace LMS.Repositories.Interfaces
{
    public interface IScheduleDetailRepository
    {

        Task AddScheduleAsync(ScheduleDetail scheduleDetail);
        Task<Schedule> getSheduleById(Guid id);

        Task UpdateScheduleAsync(Schedule schedule);

        Task UpdateScheduleDetailAsync(ScheduleDetail scheduledetail);

        Task<ScheduleDetail> getSheduleDetailByIdAsync(Guid ScheduleDetailsId);
    }
}
