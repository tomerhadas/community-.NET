using CommunityEventHub.Controllers.Base;
using CommunityEventHub.Models.Dto;
using CommunityEventHub.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventHub.Controllers
{
    [Route("api/[controller]")]
    public class EventRegistrationsController : CRUDControllerBase<EventRegistrationDto, CreateEventRegistrationDto, EventRegistrationService, int>
    {
        private readonly EventRegistrationService _service;

        public EventRegistrationsController(
            EventRegistrationService service,
            IValidator<CreateEventRegistrationDto> validator = null)
            : base(service, validator)
        {
            _service = service;
        }

        public override async Task<IActionResult> GetAll()
        {
            var regs = await _service.GetAllAsync();
            return Ok(regs);
        }

        public override async Task<IActionResult> GetById(int id)
        {
            var reg = await _service.GetByIdAsync(id);
            if (reg == null) return NotFound();
            return Ok(reg);
        }

        public override async Task<IActionResult> Create([FromBody] CreateEventRegistrationDto dto)
        {
            // Apply validation if validator is available
            var validationResult = ValidateDto(dto);
            if (validationResult != null)
                return validationResult;

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

        public override async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // Since EventRegistrationController doesn't support Update
        public override async Task<IActionResult> Update(int id, [FromBody] CreateEventRegistrationDto dto)
        {
            // Return method not allowed
            return StatusCode(405, "Method Not Allowed");
        }
    }
}
