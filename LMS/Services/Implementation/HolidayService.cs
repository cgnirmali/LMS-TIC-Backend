using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Repositories.Implementation;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services.Implementation
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly IScheduleDetailRepository _scheduleDetailRepository;
        public HolidayService(IHolidayRepository holidayRepository, IScheduleDetailRepository scheduleDetailRepository)
        {
            _holidayRepository = holidayRepository;
            _scheduleDetailRepository = scheduleDetailRepository;
        }

        public async Task<Holiday> CreateHolidayScheduleAsync(HolidayRequestDto request)
        {

            var scheduledata = await _scheduleDetailRepository.getSheduleById(request.ScheduleId);

            scheduledata.ClassSchedule = ClassSchedule.Holiday;

            await _scheduleDetailRepository.UpdateScheduleAsync(scheduledata);

            var holidaydetail = new Holiday
            {
                Id = Guid.NewGuid(),
                holiday = request.holiday,
                ScheduleId = request.ScheduleId
            };


            await _holidayRepository.AddHolidayAsync(holidaydetail);
            return holidaydetail;
        }




        public async Task<Holiday> UpdateHolidayAsync(Guid Id, UpdateHolidayRequestDto request)
        {
            var response = await _holidayRepository.getHolidayById(Id);

            if (response == null)
                throw new Exception("Schedule detail not found");


            response.holiday = request.holiday;
            

            await _holidayRepository.UpdateHolidayAsync(response);

            return response;


        }
    }
}
