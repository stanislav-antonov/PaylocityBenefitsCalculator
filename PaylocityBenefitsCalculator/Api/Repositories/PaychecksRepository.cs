using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class PaychecksRepository : IPaychecksRepository
{
    private readonly ApiDbContext _apiDbContext;

    public PaychecksRepository(ApiDbContext apiDbContext)
    {
        _apiDbContext = apiDbContext;
    }

    public async Task<IEnumerable<Paycheck>> GetAll()
    {
        return await _apiDbContext.Paychecks.Include(e => e.Employee).ToListAsync();
    }

    public async Task<Paycheck?> Get(int id)
    {
        return await _apiDbContext.Paychecks.Include(e => e.Employee).FirstOrDefaultAsync(d => d.Id == id);
    }
}
