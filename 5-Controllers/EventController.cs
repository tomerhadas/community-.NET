using CommunityEventHub.Controllers.Base;
using CommunityEventHub.Models.Dto;
using CommunityEventHub.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventHub.Controllers;

[Route("api/[controller]")]
public class EventsController : CRUDControllerBase<EventDto, CreateEventDto, EventService, int>
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService, IValidator<CreateEventDto> validator)
        : base(eventService, validator)
    {
        _eventService = eventService;
    }

    public override async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllAsync();
        return Ok(events);
    }

    public override async Task<IActionResult> GetById(int id)
    {
        var ev = await _eventService.GetByIdAsync(id);
        if (ev == null) return NotFound($"Event with id {id} not found");
        return Ok(ev);
    }

    public override async Task<IActionResult> Create([FromBody] CreateEventDto dto)
    {
        var validationResult = ValidateDto(dto);
        if (validationResult != null)
            return validationResult;

        try
        {
            var createdEvent = await _eventService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public override async Task<IActionResult> Update(int id, [FromBody] CreateEventDto dto)
    {
        var validationResult = ValidateDto(dto);
        if (validationResult != null)
            return validationResult;

        try
        {
            var updated = await _eventService.UpdateAsync(id, dto);
            if (updated == null) return NotFound($"Event with id {id} not found");
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public override async Task<IActionResult> Delete(int id)
    {
        var deleted = await _eventService.DeleteAsync(id);
        if (!deleted) return NotFound($"Event with id {id} not found");
        return NoContent();
    }
}
