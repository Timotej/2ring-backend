using AutoMapper;

namespace project
{
    public class PositionToModelProfile : Profile
    {
        public PositionToModelProfile()
        {
            CreateMap<SavePositionResource, Position>();
        }
    }
}