using LMS.DB;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace LMS.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {

        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public StudentRepository(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task AddNewStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

     
        //public async Task<ICollection<Student>> GetAllStudents()
        //{
        //    var data = await _context.Students.Include(s => s.User).ToListAsync();
        //    return data;
        //}




        public async Task<StudentGroupDto> GetStudentByIdAsync(Guid studentId)
        {
            var student = await (from s in _context.Students
                                 join g in _context.Groups on s.GroupId equals g.Id
                                 join u in _context.Users on s.UserId equals u.Id
                                 where s.Id == studentId
                                 select new StudentGroupDto
                                 {
                                     StudentId = s.Id,
                                     UTNumber = s.UTNumber,
                                     FirstName = s.FirstName,
                                     LastName = s.LastName,
                                     UserEmail = s.UserEmail,
                                     PhoneNumber = s.PhoneNumber,
                                     status = s.status,
                                     Gender = s.Gender,
                                     Address = s.Address,
                                     GroupName = g.Name,
                                     UTEmail = u.UTEmail
                                 }).FirstOrDefaultAsync();

            return student;
        }


        public async Task<List<StudentGroupDto>> GetAllStudents()
        {

         
            var data = await (from student in _context.Students
                              join Group in _context.Groups on student.GroupId equals Group.Id
                              join u in _context.Users on student.UserId equals u.Id
                              select new StudentGroupDto
                              {
                                  StudentId = student.Id,
                                  UTNumber = student.UTNumber,
                                  FirstName = student.FirstName,
                                  LastName = student.LastName,
                                  UserEmail = student.UserEmail,
                                  PhoneNumber = student.PhoneNumber,
                                  status = student.status,
                                  Gender = student.Gender,
                                  Address = student.Address,    
                                  GroupName = Group.Name,
                                  UTEmail = u.UTEmail
                              }).ToListAsync();
        

            return data;
        }





        public async Task<bool> DeleteStudent(Guid studentId)
        {
         
                var student = await _context.Students.FindAsync(studentId);
                if (student != null)
                {
                    var user = await _context.Users.FindAsync(student.UserId);
                    if (user != null)
                    {
                        _context.Users.Remove(user); // Remove User
                    }

                    _context.Students.Remove(student); // Remove Student
                    await _context.SaveChangesAsync(); // ✅ Save changes

                    
                    return true;
                }
                return false;
        }
           


        public async Task<Student> GetStudentByEmail(string email)
        {
            var student = await _context.Students.SingleOrDefaultAsync(d => d.UserEmail == email);
            return student;
        }

     

        public async Task<bool> UpdateStudentAsync(Guid studentId, UpdatedStudentDto updatedStudent)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var student = await _context.Students.FindAsync(studentId);
                if (student == null)
                {
                    return false; // Student not found
                }

                //var user = await _context.Users.FindAsync(student.UserId);
                //if (user == null)
                //{
                //    return false; // User not found
                //}

                // ✅ Update Student details
                student.UTNumber = updatedStudent.UTNumber;
                student.NIC = updatedStudent.NIC;
                student.FirstName = updatedStudent.FirstName;
                student.LastName = updatedStudent.LastName;
                student.UserEmail = updatedStudent.UserEmail;
                student.PhoneNumber = updatedStudent.PhoneNumber;
                student.Gender = updatedStudent.Gender;
                student.Address = updatedStudent.Address;
                student.UserEmail = updatedStudent.UserEmail;

                //// ✅ Update User details
                //user.UTEmail = UTEmail; // Sync email
                //if (!string.IsNullOrEmpty(newPassword))
                //{
                //    user.Password = newPassword; // Hash & update password if provided
                //}

                _context.Students.Update(student);
                //_context.Users.Update(user);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }




        //public async Task VerifyRegister(Guid id)
        //{ 
        //var data =await GetStudentById(id);
        //    data.AdminVerify = true;    
        //    await _context.SaveChangesAsync();

        //    var sameUser = await _context.Users.FirstOrDefaultAsync(x => x.Id ==data.UserId);
        //    sameUser.IsVerified = true;
        //    await _context.SaveChangesAsync();
        //}

    }
}
