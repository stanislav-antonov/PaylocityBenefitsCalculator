using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public abstract class AbstractController : ControllerBase
{
    private readonly IMapper _mapper;

    protected AbstractController(IMapper mapper)
    {
        _mapper = mapper;
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
            Console.Error.WriteLine(ex.ToString()); // Log an exception

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}