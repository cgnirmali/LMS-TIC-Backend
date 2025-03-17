using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

public interface ILecturerService
{
    Task<string> AddLecturer(LecturerRequest lecturerRequest, UserStaff_LectureRequest userStaff_LectureRequest);
    Task<List<LecturerResponse>> GetAllLecturer();
    Task<LecturerResponse> GetLecturerById(Guid id);
    Task UpdateLecturer(Guid id, LecturerRequest lecturerRequest);
    Task DeleteLecturer(Guid id);
}
