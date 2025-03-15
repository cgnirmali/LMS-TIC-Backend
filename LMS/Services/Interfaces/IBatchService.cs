using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;

namespace LMS.Services.Interfaces
{
    public interface IBatchService
    {
        Task<BatchResponseDTO> GetBatchByIdAsync(Guid id);
        Task<IEnumerable<BatchResponseDTO>> GetAllBatchesAsync();
        Task<BatchResponseDTO> CreateBatchAsync(BatchRequestDTO request);
        Task UpdateBatchAsync(Guid id, BatchRequestDTO request);
        Task DeleteBatchAsync(Guid id);
    }
}
