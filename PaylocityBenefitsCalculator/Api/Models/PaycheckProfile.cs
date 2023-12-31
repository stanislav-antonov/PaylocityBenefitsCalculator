﻿namespace Api.Models;

public class PaycheckProfile
{
    public int Id { get; set; }
    public int PayPeriodsPerYear { get; set; }
    public decimal EmployeeCostPerMonth { get; set; }
    public decimal EmployeeSalaryThreshold { get; set; }
    public int EmployeeSalaryPercent { get; set; }
    public decimal DependentCostPerMonth { get; set; }
    public int DependentAgeThreshold { get; set; }
    public decimal DependentAgeCostPerMonth { get; set; }
}
