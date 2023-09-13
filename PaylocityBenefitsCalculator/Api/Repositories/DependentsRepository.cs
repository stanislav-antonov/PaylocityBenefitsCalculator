using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class DependentsRepository : IDependentsRepository
{
    private readonly ApiDbContext _apiDbContext;

    public DependentsRepository(ApiDbContext apiDbContext)
    {
        _apiDbContext = apiDbContext;
    }

    public async Task<IEnumerable<Dependent>> GetAll()
    {
        return await _apiDbContext.Dependents.ToListAsync();
    }

    public async Task<Dependent?> Get(int id)
    {
        return await _apiDbContext.Dependents.FirstOrDefaultAsync(d => d.Id == id);
    }
}