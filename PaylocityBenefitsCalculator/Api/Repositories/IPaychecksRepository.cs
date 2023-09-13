using Api.Models;

namespace Api.Repository;

public interface IPaychecksRepository
{
    public Task<IEnumerable<Paycheck>> GetAll();

    public Task<Paycheck?> Get(int id);
}
