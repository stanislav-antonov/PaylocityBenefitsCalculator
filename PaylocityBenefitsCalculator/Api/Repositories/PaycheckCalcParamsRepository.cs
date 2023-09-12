using Api.Models;
using Api.Repository;

namespace Api.Repositories;

public class PaycheckCalcParamsRepository : IPaycheckCalcParamsRepository
{
    public async Task<PaycheckCalcParams> Get()
    {
        var paycheckCalcParams = new PaycheckCalcParams()
        {
            PayPeriodsPerYear = FakeData.payPeriodsPerYear,
            EmployeeCostPerMonth = FakeData.employeeCostPerMonth,
            EmployeeSalaryThreshold = FakeData.employeeSalaryThreshold,
            EmployeeSalaryPercent = FakeData.employeeSalaryPercent,
            DependentCostPerMonth = FakeData.dependentCostPerMonth,
            DependentAgeThreshold = FakeData.dependentAgeThreshold,
            DependentAgeCostPerMonth = FakeData.dependentCostPerMonth,
        }; 

        return await Task.FromResult(paycheckCalcParams);
    }
}