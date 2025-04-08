using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IScheduleDetailService
    {
        Task<ScheduleDetail> CreateScheduleAsync(ScheduleDetailRequestDto request);

        Task<ScheduleDetail> UpdateScheduleDetailAsync(Guid ScheduleDetailsId, UpdateScheduleDetailRequestDto request);
    }
}
