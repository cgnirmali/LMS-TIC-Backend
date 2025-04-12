using LMS.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //common tables
        public DbSet<User> Users { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        //users tables
        public DbSet<Student> Students { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<ScheduleDetail> ScheduleDetails { get; set; }
        public DbSet<Holiday> Holiday { get; set; }

      

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Staff> Staffs { get; set; }


        //Course tables

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        

        //Course assets tables
        public DbSet<Material> Materials { get; set; }
        public DbSet<Assesment> Assesments { get; set; }
        public DbSet<AssesmentSubmission> AssesmentsSubmissions { get; set; }

        //Time Table
        public DbSet<Attendance> Attendances { get; set; }
       
       

        //Quiz Exam
        public DbSet<Subject_Quiz> Subjects_Quizes { get; set; }    
        public DbSet<Subject_quiz_question> Subjects_quiz_questions { get; set; }
        public DbSet<QuizExam> QuizExams { get; set; }
        public DbSet<Questions> Questionses { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<StudentAttempts> StudentAttemptses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
     .HasOne(s => s.User)
     .WithMany()
     .HasForeignKey(s => s.UserId)
     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject_quiz_question>()
        .HasOne(sqq => sqq.Subject_Quiz)
        .WithMany(sq => sq.SubjectsQuizQuestions)
        .HasForeignKey(sqq => sqq.Subject_QuizId)
        .OnDelete(DeleteBehavior.Restrict);

       

            
            modelBuilder.Entity<AssesmentSubmission>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Assesmentsubmission)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

          

            modelBuilder.Entity<StudentAttempts>()
                .HasOne(a => a.Student)
                .WithMany(s => s.StudentAttempts)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}