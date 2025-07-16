using CommunityEventHub.Models.Dto;
using CommunityEventHub.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IValidator<CreateUserDto> _validator;

    public UsersController(UserService userService, IValidator<CreateUserDto> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound($"User with id {id} not found");
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var result = _validator.Validate(dto);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => e.ErrorMessage));

        try
        {
            var createdUser = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Email already exists"))
                return Conflict(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateUserDto dto)
    {
        var result = _validator.Validate(dto);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => e.ErrorMessage));

        var updated = await _userService.UpdateAsync(id, dto);
        if (updated == null) return NotFound($"User with id {id} not found");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.DeleteAsync(id);
        if (!deleted) return NotFound($"User with id {id} not found");
        return NoContent();
    }
}
