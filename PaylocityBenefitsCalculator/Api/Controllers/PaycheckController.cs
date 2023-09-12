using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaycheckController : AbstractController
{
    private readonly IPaycheckService _paycheckService;

    public PaycheckController(IPaycheckService paycheckService, IMapper mapper, 
        ILogger<PaycheckController> logger) 
        : base(mapper, logger)
    {
        _paycheckService = paycheckService;
    }
    
    [SwaggerOperation(Summary = "Get paycheck by employee id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int id)
    {
        return await Handle<GetPaycheckDto>(async () => {
            return await _paycheckService.CalculatePaycheck(id);
        });
    }
}
