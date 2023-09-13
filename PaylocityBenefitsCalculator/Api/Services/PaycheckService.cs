using Api.Models;
using Api.Repositories;
using Api.Repository;

namespace Api.Services;

public class PaycheckService : IPaycheckService
{
    private const int defaultPaycheckProfileId = 1;
    
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IPaychecksRepository _paychecksRepository;
    private readonly IPaycheckProfilesRepository _paycheckProfilesRepository;
    private readonly ApiDbContext _apiDbContext;

    public PaycheckService(IEmployeesRepository employeesRepository,
        IPaychecksRepository paychecksRepository,
        IPaycheckProfilesRepository paycheckProfilesRepository,
        ApiDbContext apiDbContext)
    {
        _employeesRepository = employeesRepository;
        _paychecksRepository = paychecksRepository;
        _paycheckProfilesRepository = paycheckProfilesRepository;
        _apiDbContext = apiDbContext;
    }

    public async Task<Paycheck?> CalculatePaycheck(int employeeId, bool store = false)
    {
        var employee = await _employeesRepository.Get(employeeId);
        if (employee == null)
        {
            return null; // not found
        }

        if (!ValidateEmployee(employee))
        {
            throw new ApplicationException($"Invalid employee with id = {employee.Id}");
        }

        var paycheckProfile = await _paycheckProfilesRepository.Get(defaultPaycheckProfileId);
        if (paycheckProfile == null)
        {
            throw new ApplicationException($"Missing paycheck profile with id = {defaultPaycheckProfileId}");
        }

        decimal employeeGrossSalary = employee.Salary;
        decimal employeeNetSalary = employee.Salary;

        // Employees that make more than {employeeSalaryThreshold} per year will incur
        // an additional {employeeSalaryPercent} of their yearly salary in benefits costs
        if (employeeGrossSalary > paycheckProfile.EmployeeSalaryThreshold)
        {
            employeeNetSalary -= employeeNetSalary * (decimal)(paycheckProfile.EmployeeSalaryPercent * 0.01);
        }

        // Employee's base cost per month (for benefits)
        employeeNetSalary -= (12 * paycheckProfile.EmployeeCostPerMonth);

        foreach (var dependent in employee.Dependents)
        {
            // Each dependent represents an additional {dependentCostPerMonth} cost
            // per month (for benefits)
            employeeNetSalary -= (12 * paycheckProfile.DependentCostPerMonth);

            // Dependents that are over {dependentAgeThreshold} years old will incur
            // an additional {dependentAgeCostPerMonth} per month
            if (dependent.DateOfBirth.Age() > paycheckProfile.DependentAgeThreshold)
            {
                employeeNetSalary -= (12 * paycheckProfile.DependentAgeCostPerMonth);
            }
        }

        decimal employeeGrossPay = employeeGrossSalary / paycheckProfile.PayPeriodsPerYear;
        decimal employeeNetPay = employeeNetSalary / paycheckProfile.PayPeriodsPerYear;

        var paycheck = new Paycheck()
        {
            EmployeeId = employeeId,
            Employee = employee,
            EmployeeGrossPay = Decimal.Round(employeeGrossPay, 2),
            EmployeeNetPay = Decimal.Round(employeeNetPay, 2),
            EmployeeGrossSalary = Decimal.Round(employeeGrossSalary, 2),
            EmployeeNetSalary = Decimal.Round(employeeNetSalary, 2),
            PayPeriods = paycheckProfile.PayPeriodsPerYear,
        };

        if (store)
        {
            await _apiDbContext.Paychecks.AddAsync(paycheck);
            await _apiDbContext.SaveChangesAsync();
        }
        
        return paycheck;
    }

    private static bool ValidateEmployee(Employee employee)
    {
        // An employee may only have 1 spouse or domestic partner (not both)
        return employee.Dependents.Count(d => d.Relationship == Relationship.Spouse
            || d.Relationship == Relationship.DomesticPartner) <= 1;
    }
}