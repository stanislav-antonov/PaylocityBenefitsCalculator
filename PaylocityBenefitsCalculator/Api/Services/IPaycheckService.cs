using Api.Models;

namespace Api.Services;

public interface IPaycheckService
{
    public Task<Paycheck?> CalculatePaycheck(int employeeId, bool store = false);
}
