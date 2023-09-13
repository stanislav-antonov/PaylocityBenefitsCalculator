using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class PaycheckProfilesRepository : IPaycheckProfilesRepository
{
    private readonly ApiDbContext _apiDbContext;

    public PaycheckProfilesRepository(ApiDbContext apiDbContext)
    {
        _apiDbContext = apiDbContext;
    }

    public async Task<IEnumerable<PaycheckProfile>> GetAll()
    {
        return await _apiDbContext.PaycheckProfiles.ToListAsync();
    }

    public async Task<PaycheckProfile?> Get(int id)
    {
        return await _apiDbContext.PaycheckProfiles.FirstOrDefaultAsync(p => p.Id == id);
    }

    /*
    public async Task<PaycheckProfile> Get()
    {
        var paycheckCalcParams = new PaycheckProfile()
        {
            PayPeriodsPerYear = MockData.payPeriodsPerYear,
            EmployeeCostPerMonth = MockData.employeeCostPerMonth,
            EmployeeSalaryThreshold = MockData.employeeSalaryThreshold,
            EmployeeSalaryPercent = MockData.employeeSalaryPercent,
            DependentCostPerMonth = MockData.dependentCostPerMonth,
            DependentAgeThreshold = MockData.dependentAgeThreshold,
            DependentAgeCostPerMonth = MockData.dependentCostPerMonth,
        }; 

        return await Task.FromResult(paycheckCalcParams);
    }
    */
}