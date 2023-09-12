using Api.Models;

namespace Api.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await Task.FromResult(FakeData.Employees);
        }

        public async Task<Employee?> Get(int id)
        {
            var employees = await GetAll();
            return employees.FirstOrDefault(e => e.Id == id);
        }
    }
}
