﻿namespace Api.Models;

public class Paycheck
{
    public int EmployeeId { get; set; }
    public decimal EmployeeGrossPay { get; set; }
    public decimal EmployeeNetPay { get; set; }
    public int PayPeriods { get; set; }
    public decimal EmployeeYearlyGrossSalary { get; set; }
    public decimal EmployeeYearlyNetSalary { get; set; }
}