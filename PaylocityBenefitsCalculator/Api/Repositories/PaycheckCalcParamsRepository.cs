using Api.Models;
using Api.Repository;

namespace Api.Repositories;

public class PaycheckCalcParamsRepository : IPaycheckCalcParamsRepository
{
    public async Task<PaycheckCalcParams> Get()
    {
        var paycheckCalcParams = new PaycheckCalcParams()
        {
            PayPeriodsPerYear = StaticMockData.payPeriodsPerYear,
            EmployeeCostPerMonth = StaticMockData.employeeCostPerMonth,
            EmployeeSalaryThreshold = StaticMockData.employeeSalaryThreshold,
            EmployeeSalaryPercent = StaticMockData.employeeSalaryPercent,
            DependentCostPerMonth = StaticMockData.dependentCostPerMonth,
            DependentAgeThreshold = StaticMockData.dependentAgeThreshold,
            DependentAgeCostPerMonth = StaticMockData.dependentCostPerMonth,
        }; 

        return await Task.FromResult(paycheckCalcParams);
    }
}