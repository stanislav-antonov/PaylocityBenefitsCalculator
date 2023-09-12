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

    public EmployeesController(IEmployeesRepository employeesRepository, IMapper mapper, 
        ILogger<EmployeesController> logger) 
        : base(mapper, logger)
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
}
