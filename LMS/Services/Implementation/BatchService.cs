using LMS.DB.Entities;
using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services.Implementation
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;

        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public async Task<BatchResponseDTO> GetBatchByIdAsync(Guid id)
        {
            var batch = await _batchRepository.GetByIdAsync(id);
            return new BatchResponseDTO
            {
                Id = batch.Id,
                CreatedDate = batch.CreatedDate,
                Name = batch.Name,
               
            };
        }

        public async Task<IEnumerable<BatchResponseDTO>> GetAllBatchesAsync()
        {
            var batches = await _batchRepository.GetAllAsync();
            return batches.Select(batch => new BatchResponseDTO
            {
                Id = batch.Id,
                CreatedDate = batch.CreatedDate,
                Name = batch.Name,
                //Courses = batch.Course.Select(course => new CourseResponseDTO
                //{
                //    Id = course.Id,
                //    CreatedDate = course.CreatedDate,
                //    Name = course.Name,
                //    BatchId = course.BatchId
                //}).ToList()
            });
        }

        public async Task<BatchResponseDTO> CreateBatchAsync(BatchRequestDTO request)
        {
            var batch = new Batch
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Name = request.Name
            };

            await _batchRepository.AddAsync(batch);

            return new BatchResponseDTO
            {
                Id = batch.Id,
                CreatedDate = batch.CreatedDate,
                Name = batch.Name
            };
        }

        public async Task UpdateBatchAsync(Guid id, BatchRequestDTO request)
        {
            var batch = await _batchRepository.GetByIdAsync(id);
            if (batch != null)
            {
                batch.Name = request.Name;
                await _batchRepository.UpdateAsync(batch);
            }
        }

        public async Task DeleteBatchAsync(Guid id)
        {
            await _batchRepository.DeleteAsync(id);
        }
    }
}
