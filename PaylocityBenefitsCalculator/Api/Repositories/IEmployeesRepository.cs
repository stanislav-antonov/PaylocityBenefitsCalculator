using Api.Models;

namespace Api.Repository
{
    public interface IEmployeesRepository
    {
        public Task<IEnumerable<Employee>> GetAll();

        public Task<Employee?> Get(int id);
    }
}
