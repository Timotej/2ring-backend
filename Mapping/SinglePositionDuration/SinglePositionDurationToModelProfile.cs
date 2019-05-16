using AutoMapper;

namespace project
{
    public class SinglePositionDurationToModelProfile : Profile
    {
        public SinglePositionDurationToModelProfile()
        {
            CreateMap<SaveSinglePositionDurationResource, SinglePositionDuration>();
        }
    }
}