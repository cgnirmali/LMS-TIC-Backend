
using LMS.DB;

using LMS.DB.Entities.Email;

using LMS.Repositories.Implementation;
using LMS.Repositories.Interfaces;
using LMS.Services.Implementation;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // db configuration
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

            // Register EmailConfig
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


            // Register Email services
            builder.Services.AddScoped<EmailService>();
            //builder.Services.AddScoped<SendMailRepository>();
            //builder.Services.AddScoped<EmailServiceProvider>();


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();



            // JWT Authentication Configuration
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        ValidAudience = builder.Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //    };
            //});


            // Ensure EmailConfig is available as a singleton if needed
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);


            builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICourseRepository ,CourseRepository>();  
            builder.Services.AddScoped<ICourseService ,CourseService>();
            builder.Services.AddScoped<IBatchRepository ,BatchRepository>();
            builder.Services.AddScoped<IBatchService ,BatchService>();
            builder.Services.AddScoped<IGroupRepository , GroupRepository>();
            builder.Services.AddScoped<IGroupService , GroupService>();



            // cors policy added
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
