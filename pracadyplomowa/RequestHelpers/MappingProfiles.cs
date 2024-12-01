using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Board;
using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.RequestHelpers;
using static pracadyplomowa.Models.DTOs.PowerFormDto;

namespace pracadyplomowa;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<Class, ClassDTO>();
        CreateMap<Race, RaceDTO>();
        CreateMap<ItemFamily, ItemFamilyDto>();
        CreateMap<ItemFamilyDto, ItemFamily>();
        CreateMap<ImmaterialResourceBlueprint, ImmaterialResourceBlueprintDto>();
        CreateMap<ImmaterialResourceBlueprintDto, ImmaterialResourceBlueprint>();
        CreateMap<PowerCompactDto, Power>();
        CreateMap<Power, PowerCompactDto>();
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
        CreateMap<ItemCostRequirement, ItemFamilyWithWorthDto>()
            .ForMember(
                dest => dest.Worth, opt => opt.MapFrom(src => src.Value)
            )
            .ForMember(
                dest => dest.Name, opt => opt.MapFrom(src => src.R_ItemFamily.Name)
            )
            .ForMember(
                dest => dest.Id, opt => opt.MapFrom(src => src.R_ItemFamily.Id)
            );
        CreateMap<EffectBlueprint, EffectBlueprintDto>()
            .ForMember(
                dest => dest.ResourceLevel, opt => opt.MapFrom(src => src.Level)
            )
            .ForMember(
                dest => dest.SavingThrowSuccess, opt => opt.MapFrom(src => src.Saved)
            );
        CreateMap<Power, PowerFormDto>()
            .ForMember(
                dest => dest.ActionType, opt => opt.MapFrom(src => src.RequiredActionType.ToString())
            )
            .ForMember(
                dest => dest.CastableBy, opt => opt.MapFrom(src => src.CastableBy.ToString())
            )
            .ForMember(
                dest => dest.PowerType, opt => opt.MapFrom(src => src.PowerType.ToString())
            )
            .ForMember(
                dest => dest.AreaShape, opt => opt.MapFrom(src => src.AreaShape.ToString())
            )
            .ForMember(
                dest => dest.SavingThrow, opt => opt.MapFrom(src => src.SavingThrow.ToString())
            )
            .ForMember(
                dest => dest.SavingThrowBehaviour, opt => opt.MapFrom(src => src.SavingThrowBehaviour.ToString())
            )
            .ForMember(
                dest => dest.SavingThrowRoll, opt => opt.MapFrom(src => src.SavingThrowRoll.ToString())
            )
            .ForMember(
                dest => dest.UpcastBy, opt => opt.MapFrom(src => src.UpcastBy.ToString())
            )
            .ForMember(
                dest => dest.ClassForUpcasting, 
                opt => opt.MapFrom(src => src.R_ClassForUpcasting)
            )
            .ForMember(
                dest => dest.ImmaterialResourceUsed, 
                opt => opt.MapFrom(src => src.R_UsesImmaterialResource)
            )
            .ForMember(
                dest => dest.MaterialResourcesUsed, 
                opt => opt.MapFrom(src => src.R_ItemsCostRequirement)
            )
            .ForMember(
                dest => dest.EffectBlueprints, 
                opt => opt.MapFrom(src => src.R_EffectBlueprints)
            );

        CreateMap<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.ResourceLevel,
                opt => opt.MapFrom(src => src.Level)
            )
            .ForMember(
                dest => dest.SavingThrowSuccess,
                opt => opt.MapFrom(src => src.Saved)
            )
            .ForMember(
                dest => dest.SavingThrowSuccess,
                opt => opt.MapFrom(src => src.Saved)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src is MovementEffectBlueprint ? "movementEffect" :
                src is ActionEffectBlueprint ? "actions" :
                src is SavingThrowEffectBlueprint ? "savingThrow" :
                src is AbilityEffectBlueprint ? "abilityCheck" : "x")
            );

        CreateMap<ValueEffectBlueprint, EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>()
            .ConvertUsing<DiceSetConverter>();

        CreateMap<ActionEffectBlueprint, EffectBlueprintFormDto.ActionEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.ActionEffectBlueprintFormDto.ActionSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.ActionEffectType
                })
            );

        CreateMap<MovementEffectBlueprint, EffectBlueprintFormDto.MovementEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.MovementEffectBlueprintFormDto.MovementSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.MovementEffectType
                })
            );

        CreateMap<SavingThrowEffectBlueprint, EffectBlueprintFormDto.SavingThrowEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.SavingThrowEffectBlueprintFormDto.SavingThrowSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.SavingThrowEffectType,
                })
            );

        CreateMap<AbilityEffectBlueprint, EffectBlueprintFormDto.AbilityEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.AbilityEffectBlueprintFormDto.AbilitySubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.AbilityEffectType,
                })
            );

        CreateMap<SkillEffectBlueprint, EffectBlueprintFormDto.SkillEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.SkillEffectBlueprintFormDto.SkillSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.SkillEffectType,
                })
            );

        CreateMap<ResistanceEffectBlueprint, EffectBlueprintFormDto.ResistanceEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.ResistanceEffectBlueprintFormDto.ResistanceSubeffectBlueprintFormDto(){
                    effectType = src.ResistanceEffectType,
                })
            );

        CreateMap<AttackRollEffectBlueprint, EffectBlueprintFormDto.AttackRollEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.AttackRollEffectBlueprintFormDto.AttackRollSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.AttackRollEffectType,
                })
            );

        CreateMap<ArmorClassEffectBlueprint, EffectBlueprintFormDto.ArmorClassEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.ArmorClassEffectBlueprintFormDto.ArmorClassSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                })
            );

        CreateMap<ProficiencyEffectBlueprint, EffectBlueprintFormDto.ProficiencyEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.ProficiencyEffectBlueprintFormDto.ProficiencySubeffectBlueprintFormDto(){
                    GrantsProficiencyInItemFamilyId = src.R_GrantsProficiencyInItemFamilyId,
                    effectType = src.ProficiencyEffectType
                })
            );

        CreateMap<HealingEffectBlueprint, EffectBlueprintFormDto.HealingEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.HealingEffectBlueprintFormDto.HealingSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                })
            );

        CreateMap<MagicEffectBlueprint, EffectBlueprintFormDto.MagicEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.MagicEffectBlueprintFormDto.MagicSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                })
            );

        CreateMap<SizeEffectBlueprint, EffectBlueprintFormDto.SizeEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.SizeEffectBlueprintFormDto.SizeSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.SizeEffectType,
                })
            );

        CreateMap<InitiativeEffectBlueprint, EffectBlueprintFormDto.InitiativeEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.InitiativeEffectBlueprintFormDto.InitiativeSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                })
            );

        CreateMap<DamageEffectBlueprint, EffectBlueprintFormDto.DamageEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.DamageEffectBlueprintFormDto.DamageSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.DamageEffectType,
                })
            );

        CreateMap<HitpointEffectBlueprint, EffectBlueprintFormDto.HitpointEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.HitpointEffectBlueprintFormDto.HitpointSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.HitpointEffectType,
                })
            );

        CreateMap<AttackPerAttackActionEffectBlueprint, EffectBlueprintFormDto.AttackPerAttackActionEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.AttackPerAttackActionEffectBlueprintFormDto.AttackPerAttackActionSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src),
                    effectType = src.AttackPerAttackActionEffectType,
                })
            );

        CreateMap<StatusEffectBlueprint, EffectBlueprintFormDto.StatusEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.StatusEffectBlueprintFormDto.StatusSubeffectBlueprintFormDto(){
                    effectType = src.StatusEffectType
                })
            );

        CreateMap<MovementCostEffectBlueprint, EffectBlueprintFormDto.MovementCostEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new EffectBlueprintFormDto.MovementCostEffectBlueprintFormDto.MovementCostSubeffectBlueprintFormDto(){
                    effectType = src.MovementCostEffectType
                })
            );
    }
}
