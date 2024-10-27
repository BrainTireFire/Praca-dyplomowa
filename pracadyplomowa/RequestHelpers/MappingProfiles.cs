using AutoMapper;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Board;
using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<Class, ClassDTO>();
        CreateMap<Race, RaceDTO>();
        CreateMap<Models.Entities.Campaign.Board, BoardSummaryDto>()
            .ForMember(
                dest => dest.Fields,
                opt => opt.MapFrom(
                    src => src.R_ConsistsOfFields
                )
            );
        CreateMap<Models.Entities.Campaign.Board, BoardShortDto>();
        CreateMap<BoardCreateDto, Models.Entities.Campaign.Board>();
        CreateMap<FieldDto, Field>();
        CreateMap<Field, FieldDto>();
        CreateMap<FieldUpdateDto, Field>();
        CreateMap<BoardUpdateDto, Models.Entities.Campaign.Board>();
    }
}
