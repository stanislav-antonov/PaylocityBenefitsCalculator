using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using AutoMapper;

namespace Api;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Employee, GetEmployeeDto>();
        CreateMap<Employee, GetEmployeePaycheckDto>();
        CreateMap<Dependent, GetDependentDto>();
        CreateMap<Paycheck, GetPaycheckDto>();
        CreateMap<Paycheck, CalculatePaycheckDto>();
        CreateMap<Paycheck, CalculateAndStorePaycheckDto>();
    }
}