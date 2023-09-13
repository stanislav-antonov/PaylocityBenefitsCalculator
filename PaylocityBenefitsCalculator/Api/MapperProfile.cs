using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using AutoMapper;

namespace Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<Dependent, GetDependentDto>();
            CreateMap<Paycheck, GetPaycheckDto>();
                //.ForMember(m => m.EmployeeFirstName, mm => mm.MapFrom(d => d.Employee.FirstName));

            

        }
    }
}
