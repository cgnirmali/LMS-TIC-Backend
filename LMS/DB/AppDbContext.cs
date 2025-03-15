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
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Staff> Staffs { get; set; }


        //Course tables

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Course>()
        //        .HasOne(c => c.Batch)
        //        .WithMany(b => b.Course)
        //        .HasForeignKey(c => c.BatchId);
        //}

        //Course assets tables
        public DbSet<Material> Materials { get; set; }
        public DbSet<Assesment> Assesments { get; set; }
        public DbSet<AssesmentSubmission> AssesmentsSubmissions { get; set; }

        //Time Table
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<MarkingAttendence> MarkingAttences { get; set; }

        //Quiz Exam



    }
}