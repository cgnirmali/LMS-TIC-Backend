using LMS.DB.Entities;
using LMS.DB;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories.Implementation
{
    public class ScheduleDetailRepository : IScheduleDetailRepository
    {


        private readonly AppDbContext _Context;

        public ScheduleDetailRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task AddScheduleAsync(ScheduleDetail scheduleDetail)
        {
            await _Context.ScheduleDetails.AddAsync(scheduleDetail);
            await _Context.SaveChangesAsync();

        }


        public async Task<Schedule> getSheduleById(Guid id)

        {
            var data = await _Context.Schedules.FindAsync(id);
            return data;

        }

        public async Task UpdateScheduleAsync(Schedule schedule)
        {

            _Context.Schedules.Update(schedule);
            await _Context.SaveChangesAsync();
        }


        public async Task<ScheduleDetail> getSheduleDetailByIdAsync(Guid ScheduleDetailsId)

        {
            var data = await _Context.ScheduleDetails.FindAsync(ScheduleDetailsId);
            return data;

        }
        public async Task UpdateScheduleDetailAsync(ScheduleDetail scheduledetail)
        {

            _Context.ScheduleDetails.Update(scheduledetail);
            await _Context.SaveChangesAsync();
        }



        public async Task<bool> DeleteScheduleAsync(Guid ScheduleDetailsId)
        {
            var scheduledetail = await _Context.ScheduleDetails.FindAsync(ScheduleDetailsId);
            if (scheduledetail == null)
            {
                return false;
            }

            _Context.ScheduleDetails.Remove(scheduledetail);
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
