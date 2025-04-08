using LMS.DB.Entities;
using LMS.DB;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories.Implementation
{
    public class HolidayRepository : IHolidayRepository
    {


        private readonly AppDbContext _dbContext;

        public HolidayRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddHolidayAsync(Holiday holiday)
        {
            await _dbContext.Holiday.AddAsync(holiday);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<Holiday> getHolidayById(Guid Id)

        {
            var data = await _dbContext.Holiday.FindAsync(Id);
            return data;

        }

        public async Task UpdateHolidayAsync(Holiday holiday)
        {

            _dbContext.Holiday.Update(holiday);
            await _dbContext.SaveChangesAsync();
        }
    }
}
