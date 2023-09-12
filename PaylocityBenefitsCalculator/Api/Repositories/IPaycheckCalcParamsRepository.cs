using Api.Models;

namespace Api.Repositories;

public interface IPaycheckCalcParamsRepository
{
    public Task<PaycheckCalcParams> Get();
}