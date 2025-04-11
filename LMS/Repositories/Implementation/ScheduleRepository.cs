using LMS.DB.Entities;
using LMS.DB;
using LMS.DTOs.ResponseModel;
using Microsoft.EntityFrameworkCore;
using LMS.Repositories.Interfaces;

namespace LMS.Repositories.Implementation
{
    public class ScheduleRepository  : IScheduleRepository
    {


        private readonly AppDbContext _Context;

        public ScheduleRepository(AppDbContext dbContext)
        {
            _Context = dbContext;
        }


        public async Task AddSchedulesAsync(List<Schedule> schedules)
        {
            _Context.Schedules.AddRange(schedules);
            await _Context.SaveChangesAsync();
        }



        public async Task<List<ScheduleResponseDto>> GetSchedulesByMonthAndYearAsync(int year, int month)
        {
            var schedules = await (
                from schedule in _Context.Schedules
                where schedule.Date.Year == year && schedule.Date.Month == month
                orderby schedule.Date // 👉 இதுதான் OrderBy
                select new ScheduleResponseDto
                {
                    ScheduleId = schedule.ScheduleId,
                    Date = schedule.Date,
                    ClassSchedule = (int)schedule.ClassSchedule,
                    ScheduleDetail = (
                        from detail in schedule.ScheduleDetail
                        join groups in _Context.Groups
                            on detail.GroupId equals groups.Id
                        orderby detail.cellNumber
                        select new ScheduleDetailResponseDto
                        {
                            ScheduleDetailsId = detail.ScheduleDetailsId,
                            StartTime = detail.StartTime.Value,
                            EndTime = detail.EndTime.Value,
                            Description = detail.Description,
                            cellNumber = detail.cellNumber,
                            TypeOfClass = (int)detail.TypeOfClass,
                            GroupId = detail.GroupId.ToString(),
                            Name = groups.Name
                        }).ToList(),

                    HolidayDetail = schedule.Holiday != null ? new HolidayDetailResponseDto
                    {
                        HolidayId = schedule.Holiday.Id,
                        HolidayName = schedule.Holiday.holiday
                    } : null
                }).ToListAsync();

            return schedules;
        }

    }
}
