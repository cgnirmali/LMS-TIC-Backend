using LMS.DTOs.RequestModel;

namespace LMS.Services.Interfaces
{
    public interface IStaffService
    {
        Task<string> AddStaff(StaffRequest staffRequest, UserStaff_LectureRequest userStaff_LectureRequest);
        


    }
}
