using Api.Models;
using Api.Repositories;
using Api.Repository;

namespace Api.Services;

public class PaycheckService : IPaycheckService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IPaycheckCalcParamsRepository _paycheckCalcParamsRepository;

    public PaycheckService(IEmployeesRepository employeesRepository, 
        IPaycheckCalcParamsRepository paycheckCalcParamsRepository)
    {
        _employeesRepository = employeesRepository;
        _paycheckCalcParamsRepository = paycheckCalcParamsRepository;
    }

    public async Task<Paycheck?> CalculatePaycheck(int employeeId)
    {
        var employee = await _employeesRepository.Get(employeeId);
        if (employee == null)
        {
            return null; // not found
        }

        // An employee may only have 1 spouse or domestic partner (not both)
        var isValid = employee.Dependents.Count(d => d.Relationship == Relationship.Spouse 
            || d.Relationship == Relationship.DomesticPartner) <= 1;
        
        if (!isValid)
        {
            throw new ApplicationException($"Unable to calculate a paycheck for an employee id {employeeId}");
        }

        decimal employeeGrossSalary = employee.Salary;
        decimal employeeNetSalary = employee.Salary;

        var @params = await _paycheckCalcParamsRepository.Get();

        // Employees that make more than {employeeSalaryThreshold} per year will incur
        // an additional {employeeSalaryPercent} of their yearly salary in benefits costs
        if (employeeGrossSalary > @params.EmployeeSalaryThreshold)
        {
            employeeNetSalary -= employeeNetSalary * (decimal)(@params.EmployeeSalaryPercent * 0.01);
        }

        // Employee's base cost per month (for benefits)
        employeeNetSalary -= (12 * @params.EmployeeCostPerMonth);

        foreach (var dependent in employee.Dependents)
        {
            // Each dependent represents an additional {dependentCostPerMonth} cost
            // per month (for benefits)
            employeeNetSalary -= (12 * @params.DependentCostPerMonth);

            // Dependents that are over {dependentAgeThreshold} years old will incur
            // an additional {dependentAgeCostPerMonth} per month
            if (dependent.DateOfBirth.Age() > @params.DependentAgeThreshold)
            {
                employeeNetSalary -= (12 * @params.DependentAgeCostPerMonth);
            }
        }
        
        decimal employeeGrossPay = employeeGrossSalary / @params.PayPeriodsPerYear;
        decimal employeeNetPay = employeeNetSalary / @params.PayPeriodsPerYear;

        return new Paycheck()
        {
            EmployeeId = employeeId,
            Employee = employee,
            EmployeeGrossPay = Decimal.Round(employeeGrossPay, 2),
            EmployeeNetPay = Decimal.Round(employeeNetPay, 2),
            EmployeeGrossSalary = Decimal.Round(employeeGrossSalary, 2),
            EmployeeNetSalary = Decimal.Round(employeeNetSalary, 2),
            PayPeriods = @params.PayPeriodsPerYear,
        };
    }
}