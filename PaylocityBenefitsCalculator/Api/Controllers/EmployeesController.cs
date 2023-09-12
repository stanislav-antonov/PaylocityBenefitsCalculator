using Api.Dtos.Employee;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : AbstractController
{
    private readonly IEmployeesRepository _employeesRepository;

    public EmployeesController(IEmployeesRepository employeesRepository, IMapper mapper) 
        : base(mapper)
    {
        _employeesRepository = employeesRepository;
    }
    
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        return await Handle<GetEmployeeDto>(async () => {
            return await _employeesRepository.Get(id);
        });
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        return await Handle<List<GetEmployeeDto>>(async () => {
            return await _employeesRepository.GetAll();
        });
    }

    /*
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var result = new ApiResponse<GetEmployeeDto>
        {
            Success = false,
        };

        try
        {
            var employee = await _employeesRepository.Get(id);
            if (employee == null)
            {
                result.Error = "Not Found";
                return NotFound(result);
            }
            
            result.Data = _mapper.Map<GetEmployeeDto>(employee);
            result.Success = true;

            return Ok(result);
        }
        catch (Exception ex)
        {
            result.Error = "Internal Server Error";
            Console.Error.WriteLine(ex.ToString()); // Log an exception

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
    
    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Success = false,
        };

        try 
        {
            var employees = await _employeesRepository.GetAll();

            result.Data = _mapper.Map<List<GetEmployeeDto>>(employees);
            result.Success = true;

            return Ok(result);
        }
        catch (Exception ex) 
        {
            result.Error = "Internal Server Error";
            Console.Error.WriteLine(ex.ToString()); // Log an exception

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
    */
}
