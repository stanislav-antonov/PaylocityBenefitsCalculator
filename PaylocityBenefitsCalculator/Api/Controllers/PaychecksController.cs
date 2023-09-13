using Api.Dtos.Paycheck;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaychecksController : AbstractController
{
    private readonly IPaychecksRepository _paychecksRepository;

    public PaychecksController(IPaychecksRepository paychecksRepository, IMapper mapper, 
        ILogger<PaychecksController> logger) 
        : base(mapper, logger)
    {
        _paychecksRepository = paychecksRepository;
    }
    
    [SwaggerOperation(Summary = "Get paycheck by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int id)
    {
        return await Handle<GetPaycheckDto>(async () => {
            return await _paychecksRepository.Get(id);
        });
    }

    [SwaggerOperation(Summary = "Get all paychecks")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetPaycheckDto>>>> GetAll()
    {
        return await Handle<List<GetPaycheckDto>>(async () => {
            return await _paychecksRepository.GetAll();
        });
    }
}
