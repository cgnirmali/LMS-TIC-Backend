using LMS.Assets.Enums;
using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Org.BouncyCastle.Asn1.X509;
using System.Text.RegularExpressions;

namespace LMS.Services.Implementation
{
    public class ScheduleDetailService : IScheduleDetailService
    {

        private readonly IScheduleDetailRepository _scheduleDetailRepository;

        public ScheduleDetailService(IScheduleDetailRepository scheduleDetailRepository)
        {
            _scheduleDetailRepository = scheduleDetailRepository;
        }

        public async Task<ScheduleDetail> CreateScheduleAsync(ScheduleDetailRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.StartTime) || string.IsNullOrWhiteSpace(request.EndTime))
            {
                throw new ArgumentException("StartTime and EndTime cannot be null or empty.");
            }

            var scheduledata = await _scheduleDetailRepository.getSheduleById(request.ScheduleId);

            scheduledata.ClassSchedule = ClassSchedule.Class;

            await _scheduleDetailRepository.UpdateScheduleAsync(scheduledata);

            var scheduledetail = new ScheduleDetail
            {
                ScheduleDetailsId = Guid.NewGuid(),
                StartTime = TimeSpan.Parse(request.StartTime),
                EndTime = TimeSpan.Parse(request.EndTime),
                TypeOfClass =request.TypeOfClass,
                Description = request.Description,
                cellNumber = request.cellNumber,
                GroupId = request.GroupId,
                ScheduleId = request.ScheduleId

            };



            await _scheduleDetailRepository.AddScheduleAsync(scheduledetail);
            return scheduledetail;
        }


        public async  Task<ScheduleDetail> UpdateScheduleDetailAsync(Guid ScheduleDetailsId , UpdateScheduleDetailRequestDto request)
        {
            var response = await _scheduleDetailRepository.getSheduleDetailByIdAsync(ScheduleDetailsId);

            if (response == null)
                throw new Exception("Schedule detail not found");


            response.StartTime = TimeSpan.Parse(request.StartTime);
            response.EndTime = TimeSpan.Parse(request.EndTime);
            response.cellNumber = request.cellNumber;
            response.TypeOfClass = request.TypeOfClass;
            response.Description = request.Description;
            response.GroupId = request.GroupId;

            await _scheduleDetailRepository.UpdateScheduleDetailAsync(response);
            
            return response;


        }

        public async Task<ScheduleDetail> getscheduledetailbyId( Guid ScheduleDetailsId)
        {
            var response = await _scheduleDetailRepository.getSheduleDetailByIdAsync(ScheduleDetailsId);

            var data = new ScheduleDetail
            {
                StartTime = response.StartTime,
                EndTime = response.EndTime,
                TypeOfClass = response.TypeOfClass,
                Description = response.Description,
                cellNumber = response.cellNumber,
                GroupId = response.GroupId
            };

            return data;
        }
    }
}
