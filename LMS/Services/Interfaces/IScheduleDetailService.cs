using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IScheduleDetailService
    {
        Task<ScheduleDetail> CreateScheduleAsync(ScheduleDetailRequestDto request);
        Task<ScheduleDetail> getscheduledetailbyId(Guid ScheduleDetailsId);
        Task<ScheduleDetail> UpdateScheduleDetailAsync(Guid ScheduleDetailsId, UpdateScheduleDetailRequestDto request);

        Task<bool> DeleteScheduleDetail(Guid ScheduleDetailsId);


    }
}
