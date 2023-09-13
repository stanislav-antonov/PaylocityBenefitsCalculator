namespace Api.Dtos.Paycheck;

public class GetPaycheckDto
{
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
    public decimal EmployeeGrossPay { get; set; }
    public decimal EmployeeNetPay { get; set; }
    public int PayPeriods { get; set; }
    public decimal EmployeeGrossSalary { get; set; }
    public decimal EmployeeNetSalary { get; set; }
}