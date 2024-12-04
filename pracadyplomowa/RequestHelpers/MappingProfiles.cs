using System.Diagnostics;
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
        CreateMap<ItemCostRequirement, ItemCostRequirementDto>()
            .ForMember(
                dest => dest.Worth, opt => opt.MapFrom(src => new CoinPurseDto(){
                    GoldPieces = src.GoldPieces,
                    SilverPieces = src.SilverPieces,
                    CopperPieces = src.CopperPieces
                })
            )
            .ForMember(
                dest => dest.Name, opt => opt.MapFrom(src => src.R_ItemFamily.Name)
            )
            .ForMember(
                dest => dest.ItemFamilyId, opt => opt.MapFrom(src => src.R_ItemFamily.Id)
            )
            .ForMember(
                dest => dest.Id, opt => opt.MapFrom(src => src.Id)
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
        CreateMap<PowerFormDto, Power>()
            .ForMember(
                dest => dest.R_ClassForUpcastingId,
                opt => opt.MapFrom(src => src.ClassForUpcasting != null ? src.ClassForUpcasting.Id : (int?)null) // Explicitly cast to nullable
            )
            .ForMember(
                dest => dest.R_UsesImmaterialResourceId,
                opt => opt.MapFrom(src => src.ImmaterialResourceUsed != null ? src.ImmaterialResourceUsed.Id : (int?)null)
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
                src is SavingThrowEffectBlueprint ? "savingThrow" :
                src is AbilityEffectBlueprint ? "abilityCheck" :
                src is SkillEffectBlueprint ? "skillCheck" :
                src is ResistanceEffectBlueprint ? "resistance" :
                src is AttackRollEffectBlueprint ? "attackBonus" :
                src is ArmorClassEffectBlueprint ? "armorClassBonus" :
                src is ProficiencyEffectBlueprint ? "proficiency" :
                src is HealingEffectBlueprint ? "healing" :
                src is ActionEffectBlueprint ? "actions" :
                src is MagicEffectBlueprint ? "magicItemStatus" :
                src is SizeEffectBlueprint ? "sizeCheck" :
                src is InitiativeEffectBlueprint ? "initiative" :
                src is DamageEffectBlueprint ? "damage" :
                src is HitpointEffectBlueprint ? "hitpoints" :
                src is AttackPerAttackActionEffectBlueprint ? "attacksPerAction" :
                src is StatusEffectBlueprint ? "statusEffect" :
                src is MovementCostEffectBlueprint ? "movementCost" : "UNREACHABLE")
            );

        CreateMap<DiceSet, ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>()
            .ConvertUsing<DiceSetToDtoConverter>();

        CreateMap<ActionEffectBlueprint, ActionEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ActionEffectBlueprintFormDto.ActionSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.ActionEffectType
                })
            );

        CreateMap<MovementEffectBlueprint, MovementEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new MovementEffectBlueprintFormDto.MovementSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.MovementEffectType
                })
            );

        CreateMap<SavingThrowEffectBlueprint, SavingThrowEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new SavingThrowEffectBlueprintFormDto.SavingThrowSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.SavingThrowEffectType,
                })
            );

        CreateMap<AbilityEffectBlueprint, AbilityEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new AbilityEffectBlueprintFormDto.AbilitySubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.AbilityEffectType,
                })
            );

        CreateMap<SkillEffectBlueprint, SkillEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new SkillEffectBlueprintFormDto.SkillSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.SkillEffectType,
                })
            );

        CreateMap<ResistanceEffectBlueprint, ResistanceEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ResistanceEffectBlueprintFormDto.ResistanceSubeffectBlueprintFormDto(){
                    effectType = src.ResistanceEffectType,
                })
            );

        CreateMap<AttackRollEffectBlueprint, AttackRollEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new AttackRollEffectBlueprintFormDto.AttackRollSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.AttackRollEffectType,
                })
            );

        CreateMap<ArmorClassEffectBlueprint, ArmorClassEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ArmorClassEffectBlueprintFormDto.ArmorClassSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<ProficiencyEffectBlueprint, ProficiencyEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ProficiencyEffectBlueprintFormDto.ProficiencySubeffectBlueprintFormDto(){
                    GrantsProficiencyInItemFamilyId = src.R_GrantsProficiencyInItemFamilyId,
                    effectType = src.ProficiencyEffectType
                })
            );

        CreateMap<HealingEffectBlueprint, HealingEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new HealingEffectBlueprintFormDto.HealingSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<MagicEffectBlueprint, MagicEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new MagicEffectBlueprintFormDto.MagicSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<SizeEffectBlueprint, SizeEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new SizeEffectBlueprintFormDto.SizeSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.SizeEffectType,
                })
            );

        CreateMap<InitiativeEffectBlueprint, InitiativeEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new InitiativeEffectBlueprintFormDto.InitiativeSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<DamageEffectBlueprint, DamageEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new DamageEffectBlueprintFormDto.DamageSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.DamageEffectType,
                })
            );

        CreateMap<HitpointEffectBlueprint, HitpointEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new HitpointEffectBlueprintFormDto.HitpointSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.HitpointEffectType,
                })
            );

        CreateMap<AttackPerAttackActionEffectBlueprint, AttackPerAttackActionEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new AttackPerAttackActionEffectBlueprintFormDto.AttackPerAttackActionSubeffectBlueprintFormDto(){
                    RollMoment = src.RollMoment,
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.AttackPerAttackActionEffectType,
                })
            );

        CreateMap<StatusEffectBlueprint, StatusEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new StatusEffectBlueprintFormDto.StatusSubeffectBlueprintFormDto(){
                    effectType = src.StatusEffectType
                })
            );

        CreateMap<MovementCostEffectBlueprint, MovementCostEffectBlueprintFormDto>()
            .IncludeBase<EffectBlueprint, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new MovementCostEffectBlueprintFormDto.MovementCostSubeffectBlueprintFormDto(){
                    effectType = src.MovementCostEffectType
                })
            );

        
        // CreateMap<EffectBlueprintFormDto, EffectBlueprint>()
        //     .ConvertUsing<EffectBlueprintConverter>();

        CreateMap<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.Level,
                opt => opt.MapFrom(src => src.ResourceLevel)
            )
            .ForMember(
                dest => dest.Saved,
                opt => opt.MapFrom(src => src.SavingThrowSuccess)
            )
            .ForMember(
                dest => dest.Saved,
                opt => opt.MapFrom(src => src.SavingThrowSuccess)
            );

        CreateMap<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto, DiceSet>()
            .ConvertUsing<DtoToDiceSetConverter>();
            

        CreateMap<ActionEffectBlueprintFormDto, ActionEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.ActionEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            )
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<HealingEffectBlueprintFormDto, HealingEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<MovementEffectBlueprintFormDto, MovementEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.MovementEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<SavingThrowEffectBlueprintFormDto, SavingThrowEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.SavingThrowEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<AbilityEffectBlueprintFormDto, AbilityEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.AbilityEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<SkillEffectBlueprintFormDto, SkillEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.SkillEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<ResistanceEffectBlueprintFormDto, ResistanceEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.ResistanceEffectType,
                opt => opt.MapFrom(src => ((ResistanceEffectBlueprintFormDto.ResistanceSubeffectBlueprintFormDto)src.EffectTypeBody).effectType)
            );

        CreateMap<AttackRollEffectBlueprintFormDto, AttackRollEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.AttackRollEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<ArmorClassEffectBlueprintFormDto, ArmorClassEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<ProficiencyEffectBlueprintFormDto, ProficiencyEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.R_GrantsProficiencyInItemFamilyId,
                opt => opt.MapFrom(src => src.EffectTypeBody.GrantsProficiencyInItemFamilyId)
            )
            .ForMember(
                dest => dest.ProficiencyEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<HealingEffectBlueprintFormDto, HealingEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<MagicEffectBlueprintFormDto, MagicEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<SizeEffectBlueprintFormDto, SizeEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.SizeEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<InitiativeEffectBlueprintFormDto, InitiativeEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<DamageEffectBlueprintFormDto, DamageEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.DamageEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<HitpointEffectBlueprintFormDto, HitpointEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.HitpointEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<AttackPerAttackActionEffectBlueprintFormDto, AttackPerAttackActionEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.RollMoment,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.AttackPerAttackActionEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<StatusEffectBlueprintFormDto, StatusEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.StatusEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<MovementCostEffectBlueprintFormDto, MovementCostEffectBlueprint>()
            .IncludeBase<EffectBlueprintFormDto, EffectBlueprint>()
            .ForMember(
                dest => dest.MovementCostEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

            

        
    }
}
