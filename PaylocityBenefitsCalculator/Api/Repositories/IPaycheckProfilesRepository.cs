using Api.Models;

namespace Api.Repositories;

public interface IPaycheckProfilesRepository
{
    public Task<PaycheckProfile?> Get(int id);

    public Task<IEnumerable<PaycheckProfile>> GetAll();
}