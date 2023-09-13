using Api.Models;
using Api.Repository;

namespace Api.Repositories;

public class PaycheckCalcParamsRepository : IPaycheckCalcParamsRepository
{
    public async Task<PaycheckCalcParams> Get()
    {
        var paycheckCalcParams = new PaycheckCalcParams()
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
}