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
        private readonly EmailService _sendMailService;


        public StudentService(IStudentRepository studentRepository,IUserRepository userRepository, EmailService sendMailService)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _sendMailService = sendMailService; 
        }

        public async Task<Info> AddNewStudent(StudentRequest studentRequest)
        {
            var existstudent = await _studentRepository.GetStudentByEmail(studentRequest.UserEmail);
            var user1 = await _userRepository.GetUserByEmailAsync(studentRequest.UTEmail);
            if ((existstudent == null) && (user1 == null))
                {
                try
                {
                
                    
                        string randomPassword = GenerateRandomString(6);
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(randomPassword);

                        var user = new User
                        {
                            Id = Guid.NewGuid(),
                            UTEmail = studentRequest.UTEmail,
                            CreatedDate = DateTime.Now,
                            role = Assets.Enums.Role.Student,
                            Password = hashedPassword,
                        };

                        await _userRepository.AddUserAsync(user);

                        var student = new Student
                        {
                            UserEmail = studentRequest.UserEmail,
                            FirstName = studentRequest.FirstName,
                            LastName = studentRequest.LastName,
                            UTNumber = studentRequest.UTNumber,
                            Gender = studentRequest.Gender,
                            GroupId = studentRequest.GroupId,
                            NIC = studentRequest.NIC,
                            Address = studentRequest.Address,
                            PhoneNumber = studentRequest.PhoneNumber,
                            CreatedDate = DateTime.Now,
                            status = Status.Ongoing,
                            UserId = user.Id // Make sure this UserId exists in DB
                        };

                        await _studentRepository.AddNewStudent(student);

                        var mailRequest = new MailRequest
                        {
                            User = user,
                            Type = EmailType.Register,
                            UTEmailPassword = studentRequest.UTPassword,
                            UTloginPassword = randomPassword,
                            Student = student,
                        };

                        await _sendMailService.SendEmail(mailRequest);

                        return new Info { text = "Student Added Successfully" };


                    

                    


                
                }
                catch (DbUpdateException ex)
                {
                    // Debug log
                    Console.WriteLine("Database Update Exception: " + ex.Message);
                    Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);

                    return new Info
                    {
                        text = "Error saving student to database. " +
                               "Details: " + ex.InnerException?.Message
                    };
                }
                catch (Exception ex)
                {
                    // General exception
                    Console.WriteLine("Exception: " + ex.Message);
                    return new Info { text = "Unexpected error: " + ex.Message };
                }
            }
            else
            {
                throw new Exception("Allready INsert.");

            }


       
        }


        public static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //djukvfhawi

        public async Task<bool> UpdateStudentAsync(Guid studentId, UpdatedStudentDto updatedStudent)
        {
            return await _studentRepository.UpdateStudentAsync(studentId, updatedStudent);
        }


        public async Task<bool> DeleteStudent(Guid id)
        {
            return await _studentRepository.DeleteStudent(id);
        }

        public async Task<StudentGroupDto?> GetStudentByIdAsync(Guid studentId)
        {
            return await _studentRepository.GetStudentByIdAsync(studentId);
        }
        public async Task<List<StudentGroupDto>> GetAllStudents()
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
