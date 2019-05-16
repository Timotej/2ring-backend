using AutoMapper;

namespace project
{
    public class ModelToSinglePositionDurationProfile : Profile
    {
        public ModelToSinglePositionDurationProfile()
        {
            CreateMap<SinglePositionDuration, SinglePositionDurationResource>();
        }
    }
}