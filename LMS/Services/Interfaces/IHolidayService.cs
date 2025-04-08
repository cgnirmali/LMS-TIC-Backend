using LMS.DB.Entities;
using LMS.DTOs.RequestModel;

namespace LMS.Services.Interfaces
{
    public interface IHolidayService
    {

        Task<Holiday> CreateHolidayScheduleAsync(HolidayRequestDto request);
        Task<Holiday> UpdateHolidayAsync(Guid Id, UpdateHolidayRequestDto request);
    }
}
