using LMS.DB;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace LMS.Repositories.Implementation
{
    public class StudentRepository 
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public StudentRepository(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;


        }

        public async Task<string> Register(RegisterRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (request.Password == request.ConfirmPassword && user.IsEmailConfirmed == true)
            {
                var student = new Student
                {
                    CreatedDate = DateTime.Now,
                    UserId = user.Id,
                    NIC = request.NIC,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Gender = request.Gender,
                    ImageUrl = request.ImageUrl,
                    UTNumber = request.UTNumber,
                    AdminVerify = false,

                };

                await _userRepository.ChangePassword(user.Email,request.Password);
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

            }
            return "Student Registered successfully";
        }
    }
}
