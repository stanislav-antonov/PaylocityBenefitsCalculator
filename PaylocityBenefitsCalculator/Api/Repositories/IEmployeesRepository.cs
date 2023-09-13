using Api.Models;

namespace Api.Repository;

public interface IEmployeesRepository
{
    public Task<Employee?> Get(int id);
    public Task<IEnumerable<Employee>> GetAll();
}
