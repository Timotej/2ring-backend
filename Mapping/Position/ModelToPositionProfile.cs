using AutoMapper;

namespace project
{
    public class ModelToPositionProfile : Profile
    {
        public ModelToPositionProfile()
        {
            CreateMap<Position, PositionResource>();
        }
    }
}