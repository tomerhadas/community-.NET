using CommunityEventHub.Models.Dto;
using CommunityEventHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventRegistrationsController : ControllerBase
    {
        private readonly EventRegistrationService _service;

        public EventRegistrationsController(EventRegistrationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regs = await _service.GetAllAsync();
            return Ok(regs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reg = await _service.GetByIdAsync(id);
            if (reg == null) return NotFound();
            return Ok(reg);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventRegistrationDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already registered"))
                    return Conflict(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
