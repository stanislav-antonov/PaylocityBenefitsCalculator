namespace Api.Models;

public class Paycheck
{
    public int Id { get; set; }
    public decimal EmployeeGrossPay { get; set; }
    public decimal EmployeeNetPay { get; set; }
    public int PayPeriods { get; set; }
    public decimal EmployeeGrossSalary { get; set; }
    public decimal EmployeeNetSalary { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
