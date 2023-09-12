using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public abstract class AbstractController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    protected AbstractController(IMapper mapper, ILogger logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    protected async Task<ActionResult> Handle<T>(Func<Task<object?>> predicate)
    {
        var result = new ApiResponse<T>
        {
            Success = false,
        };

        try
        {
            var entity = await predicate();
            if (entity == null)
            {
                result.Error = "Not Found";
                return NotFound(result);
            }

            result.Data = _mapper.Map<T>(entity);
            result.Success = true;

            return Ok(result);
        }
        catch (Exception ex)
        {
            result.Error = ex.Message ?? "Internal Server Error";
            _logger.LogError(ex, "Error");

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}