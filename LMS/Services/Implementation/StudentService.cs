using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace LMS.Services.Implementation
{
    public class StudentService : IStudentService
    {
    private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository ;


        public StudentService(IStudentRepository studentRepository,IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            
        }

        public async Task<Info>AddNewStudent(StudentRequest studentRequest)
        {
            var existstudent = await _studentRepository.GetStudentByEmail(studentRequest.UserEmail);
            if (existstudent == null)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UTEmail = studentRequest.UTEmail,
                    CreatedDate = DateTime.Now,
                    role = Assets.Enums.Role.Student,
                    Password = GenerateRandomString(6),

                };
                await _userRepository.AddUserAsync(user);

                var student = new Student();
                student.UserEmail = studentRequest.UserEmail;
                student.FirstName = studentRequest.FirstName;
                student.LastName = studentRequest.LastName;
                student.UTNumber = studentRequest.UTNumber;
                student.Gender = studentRequest.Gender;
                student.NIC = studentRequest.NIC;
                student.Address = studentRequest.Address;
                student.PhoneNumber = studentRequest.PhoneNumber;           
                student.CreatedDate = DateTime.Now;
                student.status = Status.Ongoing;
                student.UserId = user.Id;

                await _studentRepository.AddNewStudent(student);

                var mailRequest = new MailRequest
                {
                    User = user,
                    Type = EmailType.Register,
                    UTPassword = studentRequest.UTPassword,
                    Student = student,
                    
                };

                return new Info { text = "Student Added Sucessfully " };
            }

            return new Info { text = " Student Already Exist " };
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        public async Task<bool> UpdateStudentAsync(Guid studentId, Student updatedStudent, string? newPassword, string? UTEmail)
        {
            return await _studentRepository.UpdateStudentAsync(studentId, updatedStudent, newPassword,UTEmail);
        }


        public async Task<bool> DeleteStudent(Guid id)
        {
            return await _studentRepository.DeleteStudent(id);
        }

        public async Task<Student?> GetStudentByIdAsync(Guid studentId)
        {
            return await _studentRepository.GetStudentByIdAsync(studentId);
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
