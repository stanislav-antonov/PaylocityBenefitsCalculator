using Api.Models;

namespace Api.Repository;

public interface IPaychecksRepository
{
    public Task<Paycheck?> Get(int id);
    public Task<IEnumerable<Paycheck>> GetAll();
}
