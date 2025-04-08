using LMS.DB.Entities;
using System.Threading.Tasks;

namespace LMS.Repositories.Interfaces
{
    public interface IHolidayRepository
    {
        Task AddHolidayAsync(Holiday holiday);
        Task UpdateHolidayAsync(Holiday holiday);


        Task<Holiday> getHolidayById(Guid Id);
    }
}
