using Api.Models;
using Api.Repository;

namespace Api.Services;

public class PaycheckService : IPaycheckService
{
    // put the following into configuration, should be extendable

    // 26 paychecks per year with deductions spread as evenly as possible on each paycheck
    private const int payPeriods = 26;

    // employees have a base cost of $1,000 per month(for benefits)
    private decimal employeeCostPerMonth = 1000;

    // employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
    private decimal employeeYealySalaryThreshold = 80000;
    private int employeeYearlySalaryAddition = 2;

    // each dependent represents an additional $600 cost per month (for benefits)
    private decimal dependentCostPerMonth = 600;

    // dependents that are over 50 years old will incur an additional $200 per month
    private int dependentAgeThreshold = 50;
    private decimal dependentAgeCostPerMonth = 200;

    private readonly IEmployeesRepository _employeesRepository;

    public PaycheckService(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
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
            throw new ApplicationException($"Unable to calculate a paycheck for employee id {employeeId}");
        }

        decimal employeeYearlyGrossSalary = employee.Salary;
        decimal employeeYearlyNetSalary = employeeYearlyGrossSalary;
        decimal employeeGrossPay = employeeYearlyGrossSalary / payPeriods;

        // Employees that make more than _employeeYealySalaryAmountThreshold_ per year will incur
        // an additional _employeeYearlySalaryAddition_ of their yearly salary in benefits costs
        if (employeeYearlyGrossSalary > employeeYealySalaryThreshold)
        {
            employeeYearlyNetSalary -= (employeeYearlyNetSalary * (employeeYearlySalaryAddition / 100));
        }

        // Employee's base cost per month (for benefits)
        employeeYearlyNetSalary -= (12 * employeeCostPerMonth);

        foreach (var dependent in employee.Dependents)
        {
            // Each dependent represents an additional {dependentCostPerMonth} cost
            // per month (for benefits)
            employeeYearlyNetSalary -= (12 * dependentCostPerMonth);

            // Dependents that are over {dependentAgeThreshold} years old will incur
            // an additional {dependentAgeCostPerMonth} per month
            if (dependent.DateOfBirth.Age() > dependentAgeThreshold)
            {
                employeeYearlyNetSalary -= (12 * dependentAgeCostPerMonth);
            }
        }

        decimal employeeNetPay = employeeYearlyNetSalary / payPeriods;

        return new Paycheck()
        {
            EmployeeId = employeeId,
            EmployeeNetPay = employeeNetPay,
            EmployeeGrossPay = employeeGrossPay,
            EmployeeYearlyGrossSalary = employeeYearlyGrossSalary,
            EmployeeYearlyNetSalary = employeeYearlyNetSalary,
            PayPeriods = payPeriods
        };
    }
}