using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly ApiDbContext _apiDbContext;

    public EmployeesRepository(ApiDbContext apiDbContext)
    {
        _apiDbContext = apiDbContext;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _apiDbContext.Employees.Include(e => e.Dependents)
            .ToListAsync();
    }

    public async Task<Employee?> Get(int id)
    {
        return await _apiDbContext.Employees.Include(e => e.Dependents)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
