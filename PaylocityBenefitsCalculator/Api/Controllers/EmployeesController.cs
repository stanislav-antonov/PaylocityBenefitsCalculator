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

    [SwaggerOperation(Summary = "Calculate paycheck by employee id")]
    [HttpGet("{id}/Paycheck/Calculate")]
    public async Task<ActionResult<ApiResponse<CalculatePaycheckDto>>> CalculatePaycheck(int id)
    {
        return await Handle<CalculatePaycheckDto>(async () => {
            return await _paycheckService.CalculatePaycheck(id);
        });
    }

    [SwaggerOperation(Summary = "Calculate paycheck by employee id and persist it")]
    [HttpPost("{id}/Paycheck/CalculateAndStore")]
    public async Task<ActionResult<ApiResponse<CalculateAndStorePaycheckDto>>> CalculateAndStorePaycheck(int id)
    {
        return await Handle<CalculateAndStorePaycheckDto>(async () => {
            return await _paycheckService.CalculatePaycheck(id, store: true);
        });
    }
}
