using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services.Interfaces
{
    public interface IStaffService
    {
        Task<string> AddStaff(StaffRequest staffRequest, UserStaff_LectureRequest userStaff_LectureRequest);
        
        Task<List<StaffResponse>> GetAllStaff();
        Task<StaffResponse> GetStaffById(Guid id);
        Task UpdateStaff(Guid id, StaffRequest staffRequest);
        Task DeleteStaff(Guid id);
    }
}
