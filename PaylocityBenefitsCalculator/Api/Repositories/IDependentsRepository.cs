using Api.Models;

namespace Api.Repository
{
    public interface IDependentsRepository
    {
        public Task<IEnumerable<Dependent>> GetAll();

        public Task<Dependent?> Get(int id);
    }
}
