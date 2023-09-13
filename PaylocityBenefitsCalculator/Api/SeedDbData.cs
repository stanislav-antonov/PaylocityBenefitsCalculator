using Api.Models;

namespace Api;

public static class SeedDbData
{
    public static readonly PaycheckProfile PaycheckProfile = new()
    {
        Id = 1,

        // 26 paychecks per year with deductions spread as evenly as possible on each paycheck
        PayPeriodsPerYear = 26,

        // Employees have a base cost of $1,000 per month(for benefits)
        EmployeeCostPerMonth = 1000,

        // Employees that make more than $80,000 per year will incur an additional 2% of their
        // yearly salary in benefits costs
        EmployeeSalaryThreshold = 80000,
        EmployeeSalaryPercent = 2,

        // Each dependent represents an additional $600 cost per month (for benefits)
        DependentCostPerMonth = 600,

        // Dependents that are over 50 years old will incur an additional $200 per month
        DependentAgeThreshold = 50,
        DependentAgeCostPerMonth = 200,
    };

    public static readonly List<Dependent> Dependents = new()
    {
        new()
        {
            Id = 1,
            FirstName = "Spouse",
            LastName = "Morant",
            Relationship = Relationship.Spouse,
            DateOfBirth = new DateTime(1998, 3, 3),
        },
        new()
        {
            Id = 2,
            FirstName = "Child1",
            LastName = "Morant",
            Relationship = Relationship.Child,
            DateOfBirth = new DateTime(2020, 6, 23),
        },
        new()
        {
            Id = 3,
            FirstName = "Child2",
            LastName = "Morant",
            Relationship = Relationship.Child,
            DateOfBirth = new DateTime(2021, 5, 18),
        },
        new()
        {
            Id = 4,
            FirstName = "DP",
            LastName = "Jordan",
            Relationship = Relationship.DomesticPartner,
            DateOfBirth = new DateTime(1974, 1, 2),
        }
    };

    public static readonly List<Employee> Employees = new()
    {
        new()
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        },
        new()
        {
            Id = 2,
            FirstName = "Ja",
            LastName = "Morant",
            Salary = 92365.22m,
            DateOfBirth = new DateTime(1999, 8, 10),
            Dependents = new List<Dependent>
            {
                Dependents.First(d => d.Id == 1),
                Dependents.First(d => d.Id == 2),
                Dependents.First(d => d.Id == 3),
            }
        },
        new()
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = 143211.12m,
            DateOfBirth = new DateTime(1963, 2, 17),
            Dependents = new List<Dependent>
            {
                Dependents.First(d => d.Id == 4),
            }
        }
    };
}
