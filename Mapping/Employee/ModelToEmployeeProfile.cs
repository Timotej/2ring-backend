using AutoMapper;

namespace project
{
    public class ModelToEmployeeProfile : Profile
    {
        public ModelToEmployeeProfile()
        {
            CreateMap<Employee, EmployeeResource>();
        }
    }
}