using AutoMapper;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<Class, ClassDTO>();
        CreateMap<Race, RaceDTO>();
    }
}
