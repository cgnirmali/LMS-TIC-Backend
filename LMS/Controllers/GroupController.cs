using LMS.DTOs.RequestModel;
using LMS.DTOs.ResponseModel;
using LMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batch>>> GetGroups()
        {
            return await _context.Batches.Include(b => b.Course).ToListAsync();
        }

        // GET: api/Group/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Batch>> GetGroup(Guid id)
        {
            var batch = await _context.Batches.Include(b => b.Course).FirstOrDefaultAsync(b => b.Id == id);

            if (batch == null)
            {
                return NotFound();
            }

            return batch;
        }

        // POST: api/Group
        [HttpPost]
        public async Task<ActionResult<Batch>> CreateGroup(Batch batch)
        {
            batch.Id = Guid.NewGuid();
            batch.CreatedDate = DateTime.UtcNow;

            _context.Batches.Add(batch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = batch.Id }, batch);
        }

        // PUT: api/Group/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, Batch batch)
        {
            if (id != batch.Id)
            {
                return BadRequest();
            }

            _context.Entry(batch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Batches.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Group/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
