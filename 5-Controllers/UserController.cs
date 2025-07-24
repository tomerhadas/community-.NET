using CommunityEventHub.Controllers.Base;
using CommunityEventHub.Models.Dto;
using CommunityEventHub.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventHub.Controllers;

[Route("api/[controller]")]
public class UsersController : CRUDControllerBase<UserDto, CreateUserDto, UserService, int>
{
    private readonly UserService _userService;

    public UsersController(UserService userService, IValidator<CreateUserDto> validator)
        : base(userService, validator)
    {
        _userService = userService;
    }

    public override async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    public override async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound($"User with id {id} not found");
        return Ok(user);
    }

    public override async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var validationResult = ValidateDto(dto);
        if (validationResult != null)
            return validationResult;

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

    public override async Task<IActionResult> Update(int id, [FromBody] CreateUserDto dto)
    {
        var validationResult = ValidateDto(dto);
        if (validationResult != null)
            return validationResult;

        var updated = await _userService.UpdateAsync(id, dto);
        if (updated == null) return NotFound($"User with id {id} not found");
        return Ok(updated);
    }

    public override async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.DeleteAsync(id);
        if (!deleted) return NotFound($"User with id {id} not found");
        return NoContent();
    }
}
