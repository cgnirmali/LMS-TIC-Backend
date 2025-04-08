using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services.Implementation
{
    public class ScheduleService  : IScheduleService
    {

        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task CreateSchedulesForMonth(int year, int month)
        {
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var schedules = new List<Schedule>();

            for (int day = 1; day <= daysInMonth; day++)
            {
                schedules.Add(new Schedule
                {
                    ScheduleId = Guid.NewGuid(),
                    Date = new DateTime(year, month, day),
                    ClassSchedule = ClassSchedule.NoAdd
                });
            }

            // Sort schedules by date (though they should already be in order)
            var orderedSchedules = schedules.OrderBy(s => s.Date).ToList();

            await _scheduleRepository.AddSchedulesAsync(orderedSchedules);
        }



        public async Task<List<ScheduleResponseDto>> GetSchedulesByMonthAndYearAsync(int year, int month)
        {
            return await _scheduleRepository.GetSchedulesByMonthAndYearAsync(year, month);
        }


    }
}
