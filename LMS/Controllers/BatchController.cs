using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
          _batchService = batchService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BatchResponseDTO>> GetBatch(Guid id)
        {
            var batch = await _batchService.GetBatchByIdAsync(id);
            if (batch == null)
            {
                return NotFound();
            }
            return Ok(batch);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchResponseDTO>>> GetBatches()
        {
            var batches = await _batchService.GetAllBatchesAsync();
            return Ok(batches);
        }

        [HttpPost]
        public async Task<ActionResult<BatchResponseDTO>> CreateBatch(BatchRequestDTO request)
        {
            var batch = await _batchService.CreateBatchAsync(request);
            return CreatedAtAction(nameof(GetBatch), new { id = batch.Id }, batch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBatch(Guid id, BatchRequestDTO request)
        {
            await _batchService.UpdateBatchAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(Guid id)
        {
            await _batchService.DeleteBatchAsync(id);
            return NoContent();
        }
    }
}
