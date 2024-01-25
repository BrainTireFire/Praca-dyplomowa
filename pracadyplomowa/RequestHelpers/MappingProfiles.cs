using AutoMapper;

namespace pracadyplomowa;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, User>();
    }
}
