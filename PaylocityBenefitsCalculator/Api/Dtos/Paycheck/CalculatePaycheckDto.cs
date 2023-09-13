using Api.Dtos.Employee;

namespace Api.Dtos.Paycheck;

public class CalculatePaycheckDto
{
    public decimal EmployeeGrossPay { get; set; }
    public decimal EmployeeNetPay { get; set; }
    public int PayPeriods { get; set; }
    public decimal EmployeeGrossSalary { get; set; }
    public decimal EmployeeNetSalary { get; set; }
    public GetEmployeeForPaycheckDto? Employee { get; set; }
}