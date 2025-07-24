using Microsoft.AspNetCore.Mvc;

namespace CommunityEventHub.Controllers.Interfaces;


public interface ICRUD<TEntity, TCreateDto, TId>
    where TEntity : class
    where TCreateDto : class
{
   
    Task<IActionResult> GetAll();

    Task<IActionResult> GetById(TId id);

    Task<IActionResult> Create([FromBody] TCreateDto dto);

    Task<IActionResult> Update(TId id, [FromBody] TCreateDto dto);

    Task<IActionResult> Delete(TId id);
}