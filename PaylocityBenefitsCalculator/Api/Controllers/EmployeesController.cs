using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Repository;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : AbstractController
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IPaycheckService _paycheckService;

    public EmployeesController(IEmployeesRepository employeesRepository,
        IPaycheckService paycheckService, IMapper mapper, ILogger<EmployeesController> logger) 
        : base(mapper, logger)
    {
        _paycheckService = paycheckService;
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

    [SwaggerOperation(Summary = "Get paycheck for employee id")]
    [HttpGet("{id}/Paycheck")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> GetPaycheck(int id)
    {
        return await Handle<GetPaycheckDto>(async () => {
            return await _paycheckService.CalculatePaycheck(id);
        });
    }
}
