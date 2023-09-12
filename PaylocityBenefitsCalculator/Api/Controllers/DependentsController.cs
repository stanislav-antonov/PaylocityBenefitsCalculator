using Api.Dtos.Dependent;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : AbstractController
{
    private readonly IDependentsRepository _dependentsRepository;

    public DependentsController(IDependentsRepository dependentsRepository, IMapper mapper)
        : base(mapper)
    {
        _dependentsRepository = dependentsRepository;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        return await Handle<GetDependentDto>(async () => {
            return await _dependentsRepository.Get(id);
        });
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        return await Handle<List<GetDependentDto>>(async () => {
            return await _dependentsRepository.GetAll();
        });
    }
}
