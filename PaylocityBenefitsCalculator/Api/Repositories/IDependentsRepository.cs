using Api.Models;

namespace Api.Repository;

public interface IDependentsRepository
{
    public Task<Dependent?> Get(int id);
    public Task<IEnumerable<Dependent>> GetAll();
}
