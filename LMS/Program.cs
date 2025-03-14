
using LMS.DB;
using LMS.Repositories.Implementation;
using LMS.Repositories.Interfaces;
using LMS.Services.Implementation;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<ICourseRepository ,CourseRepository>();  
            builder.Services.AddScoped<ICourseService ,CourseService>();
            builder.Services.AddScoped<IBatchRepository ,BatchRepository>();
            builder.Services.AddScoped<IBatchService ,BatchService>();


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
