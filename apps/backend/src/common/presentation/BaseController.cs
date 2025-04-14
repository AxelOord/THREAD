using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thread.Domain.Primitives.Interfaces;

namespace Thread.Presentation;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<TEntity, TDto, TDbContext> : ControllerBase
where TDbContext : DbContext
where TEntity : class, IEntity
where TDto : IDto
{

    /// <summary>
    /// Get entity by  id
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public ActionResult GetById(Guid id, CancellationToken cancellationToken)
    {
        return new OkObjectResult(id);
    }

    /// <summary>
    /// Get all entities
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult GetAll(CancellationToken cancellationToken)
    {
        return new OkResult();
    }

    /// <summary>
    /// Delete entity by id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id, CancellationToken cancellationToken)
    {
        return new NoContentResult();
    }
}

