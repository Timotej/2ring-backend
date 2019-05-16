using AutoMapper;

namespace project
{
    public class EmployeeToModelProfile : Profile
    {
        public EmployeeToModelProfile()
        {
            CreateMap<SaveEmployeeResource, Employee>();
        }
    }
}