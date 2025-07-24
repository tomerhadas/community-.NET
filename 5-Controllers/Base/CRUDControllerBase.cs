using CommunityEventHub.Controllers.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventHub.Controllers.Base;

[ApiController]
public abstract class CRUDControllerBase<TEntity, TCreateDto, TService, TId> : ControllerBase, ICRUD<TEntity, TCreateDto, TId>
    where TEntity : class
    where TCreateDto : class
    where TService : class
{
    protected readonly TService _service;
    protected readonly IValidator<TCreateDto>? _validator;


    protected CRUDControllerBase(TService service, IValidator<TCreateDto> validator)
    {
        _service = service;
        _validator = validator;
    }

    protected CRUDControllerBase(TService service)
    {
        _service = service;
        _validator = null;
    }


    [HttpGet]
    public abstract Task<IActionResult> GetAll();

    [HttpGet("{id}")]
    public abstract Task<IActionResult> GetById(TId id);

    [HttpPost]
    public abstract Task<IActionResult> Create([FromBody] TCreateDto dto);

    [HttpPut("{id}")]
    public abstract Task<IActionResult> Update(TId id, [FromBody] TCreateDto dto);

    [HttpDelete("{id}")]
    public abstract Task<IActionResult> Delete(TId id);

    protected IActionResult? ValidateDto(TCreateDto dto)
    {
        if (_validator == null)
            return null;

        var result = _validator.Validate(dto);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => e.ErrorMessage));

        return null;
    }
}