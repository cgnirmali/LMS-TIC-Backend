using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services.Implementation
{
    public class StudentService : IStudentService
    {
    private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ICollection<Student>> GetAllStudents()
        { 
        var data = await _studentRepository.GetAllStudents();
            return data;
        }

        public async Task<Student>GetStudentByEmail(string email)
        {
            var data = await _studentRepository.GetStudentByEmail(email);
            return data;
        }
    }
}
