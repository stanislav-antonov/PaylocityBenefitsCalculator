using Api.Models;

namespace Api.Repository
{
    public class DependentsRepository : IDependentsRepository
    {
        public async Task<IEnumerable<Dependent>> GetAll()
        {
            return await Task.FromResult(FakeData.Dependents);
        }

        public async Task<Dependent?> Get(int id)
        {
            var dependents = await GetAll();
            return dependents.FirstOrDefault(d => d.Id == id);
        }
    }
}
