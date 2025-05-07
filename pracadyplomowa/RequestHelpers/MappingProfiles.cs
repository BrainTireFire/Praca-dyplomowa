using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Board;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.RequestHelpers;
using static pracadyplomowa.Models.DTOs.PowerFormDto;

namespace pracadyplomowa;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DiceSet, DiceSetDto>().ReverseMap();
        CreateMap<RegisterDto, User>();
        CreateMap<Class, ClassDTO>();
        CreateMap<Race, RaceDTO>();
        CreateMap<ItemFamily, ItemFamilyDto>();
        CreateMap<ItemFamilyDto, ItemFamily>();
        CreateMap<ImmaterialResourceBlueprint, ImmaterialResourceBlueprintDto>();
        CreateMap<ImmaterialResourceBlueprintDto, ImmaterialResourceBlueprint>();
        CreateMap<PowerCompactDto, Power>();
        CreateMap<Power, PowerCompactDto>();
        CreateMap<Item, ItemListElementDto>()
        .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.R_Owner.UserName));
        CreateMap<Models.Entities.Campaign.Board, BoardSummaryDto>()
            .ForMember(
                dest => dest.Fields,
                opt => opt.MapFrom(
                    src => src.R_ConsistsOfFields
                )
            );
        
        //BOARD FIELD
        CreateMap<Models.Entities.Campaign.Board, BoardShortDto>();
        CreateMap<BoardCreateDto, Models.Entities.Campaign.Board>();
        CreateMap<FieldDto, Field>();
        CreateMap<Field, FieldDto>()
            .ForMember(
                dest => dest.Powers,
                opt => opt.MapFrom(src => src.R_CasterPowers)
            );
        CreateMap<FieldUpdateDto, Field>();
        CreateMap<BoardUpdateDto, Models.Entities.Campaign.Board>();

        //ENCOUNTER
        CreateMap<Encounter, EncounterShortDto>()
            .ForMember(
                dest => dest.Campaign,
                opt => opt.MapFrom(
                    src => src.R_Campaign
                )
            )
            .ForMember(
                dest => dest.Board,
                opt => opt.MapFrom(
                    src => src.R_Board
                )
            )
            .ForMember(
                dest => dest.Participances,
                opt => opt.MapFrom(
                    src => src.R_Participances
                )
            );
        CreateMap<Encounter, EncounterSummaryDto>()
            .ForMember(
                dest => dest.Campaign,
                opt => opt.MapFrom(
                    src => src.R_Campaign
                )
            )
            .ForMember(
                dest => dest.Board,
                opt => opt.MapFrom(
                    src => src.R_Board
                )
            )
            .ForMember(
                dest => dest.Participances,
                opt => opt.MapFrom(
                    src => src.R_Participances
                )
            );
        CreateMap<Campaign, EncounterCampaignDto>()
            .ForMember(
                dest => dest.Members,
                opt => opt.MapFrom(
                    src => src.R_CampaignHasCharacters
                )
            );
        CreateMap<Character, ParticipanceCharacterSummaryDto>()
            .ForMember(
                dest => dest.Size,
                opt => opt.MapFrom(
                    src => new SizeItem(){
                        Order = (int)src.Size,
                        Name = src.Size
                    }
                )
            )
            .ForMember(
                dest => dest.Race,
                opt => opt.MapFrom(
                    src => src.R_CharacterBelongsToRace.Name
                )
            )
            .ForMember(dest => dest.Class,
                opt => opt.MapFrom(src => 
                    src.R_CharacterHasLevelsInClass
                        .OrderByDescending(l => l.Level)
                        .FirstOrDefault() != null ? src.R_CharacterHasLevelsInClass
                        .OrderByDescending(l => l.Level)
                        .FirstOrDefault().R_Class.Name : null
                )
            );
        CreateMap<ParticipanceData, ParticipanceDataDto>()
            .ForMember(
                dest => dest.Character,
                opt => opt.MapFrom(
                    src => src.R_Character
                )
            )
            .ForMember(
                dest => dest.OccupiedField,
                opt => opt.MapFrom(
                    src => src.R_OccupiedField
                )
            );
        
        CreateMap<ItemCostRequirement, ItemCostRequirementDto>()
            // .ForMember(
            //     dest => dest.Worth, opt => opt.MapFrom(src => new CoinPurseDto(){
            //         GoldPieces = src.Price.GoldPieces,
            //         SilverPieces = src.Price.SilverPieces,
            //         CopperPieces = src.Price.CopperPieces
            //     })
            // )
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
                opt => opt.MapFrom(src => src.R_ClassForUpcastingId)
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
                opt => opt.MapFrom(src => src.ClassForUpcasting) // Explicitly cast to nullable
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
                src is SizeEffectBlueprint ? "size" :
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

//--------------ITEMS---------------------------
        CreateMap<Item, ItemFormDto>()
        .ForMember(dest => dest.ItemFamilyId, opt => opt.MapFrom(src => src.R_ItemInItemsFamilyId))
        .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.R_OwnerId));
        CreateMap<ItemFormDto, Item>()
        .ForMember(dest => dest.R_ItemInItemsFamilyId, opt => opt.MapFrom(src => src.ItemFamilyId));

        CreateMap<CoinSack, CoinPurseDto>();
        CreateMap<CoinPurseDto, CoinSack>();

        CreateMap<Apparel, ApparelFormDto>()
            .IncludeBase<Item, ItemFormDto>()
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => "apparel"))
            .ForMember(dest => dest.ItemTypeBody, opt => opt.MapFrom((src) => new ApparelFormDto.Body(){
                IsSpellFocus = src.IsSpellFocus,
                OccupiesAllSlots = src.OccupiesAllSlots,
                MinimumStrength = src.StrengthRequirement,
                ArmorClass = src.ArmorClass,
                DisadvantageOnStealth = src.StealthDisadvantage,
                EffectsOnWearer = src.R_EffectsOnEquip.Select(effect => new EquippableItemFormDto.Body.EffectBlueprintDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                EffectsOnItem = src.R_AffectedBy.Select(effect => new EquippableItemFormDto.Body.EffectBlueprintDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                Powers = src.R_EquipItemGrantsAccessToPower.Select(effect => new EquippableItemFormDto.Body.PowerDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                ResourcesOnEquip = src.R_ItemGrantsResources
                    .GroupBy(x => new {x.R_BlueprintId, x.Level, x.R_Blueprint.Name})
                    .Select(g => new RangedWeaponFormDto.Body.ResourceDto(){
                        BlueprintId = g.Key.R_BlueprintId,
                        Name = g.Key.Name,
                        Count = g.Count(),
                        Level = g.Key.Level
                    }).ToList(),
                Slots = src.R_ItemIsEquippableInSlots.Select(slot => new EquippableItemFormDto.Body.SlotDto(){
                    Id = slot.Id,
                    Name = slot.Name
                }).ToList()
            }));
        CreateMap<ApparelFormDto, Apparel>()
            .IncludeBase<ItemFormDto, Item>()
            .ForMember(dest => dest.IsSpellFocus, opt => opt.MapFrom(src => src.ItemTypeBody.IsSpellFocus))
            .ForMember(dest => dest.OccupiesAllSlots, opt => opt.MapFrom(src => src.ItemTypeBody.OccupiesAllSlots))
            .ForMember(dest => dest.StrengthRequirement, opt => opt.MapFrom(src => src.ItemTypeBody.MinimumStrength))
            .ForMember(dest => dest.StealthDisadvantage, opt => opt.MapFrom(src => src.ItemTypeBody.DisadvantageOnStealth))
            .ForMember(dest => dest.ArmorClass, opt => opt.MapFrom(src => src.ItemTypeBody.ArmorClass));

        CreateMap<MeleeWeapon, MeleeWeaponFormDto>()
            .IncludeBase<Item, ItemFormDto>()
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => "meleeWeapon"))
            .ForMember(dest => dest.ItemTypeBody, opt => opt.MapFrom((src) => new MeleeWeaponFormDto.Body(){
                IsSpellFocus = src.IsSpellFocus,
                OccupiesAllSlots = src.OccupiesAllSlots,
                Damage = new MeleeWeaponFormDto.Body.DiceSetFormDto(){
                    d100 = src.DamageValue.d100,
                    d20 = src.DamageValue.d20,
                    d12 = src.DamageValue.d12,
                    d10 = src.DamageValue.d10,
                    d8 = src.DamageValue.d8,
                    d6 = src.DamageValue.d6,
                    d4 = src.DamageValue.d4,
                    flat = src.DamageValue.flat,
                },
                VersatileDamage = new MeleeWeaponFormDto.Body.DiceSetFormDto() {
                    d100 = src.VersatileDamageValue.d100,
                    d20 = src.VersatileDamageValue.d20,
                    d12 = src.VersatileDamageValue.d12,
                    d10 = src.VersatileDamageValue.d10,
                    d8 = src.VersatileDamageValue.d8,
                    d6 = src.VersatileDamageValue.d6,
                    d4 = src.VersatileDamageValue.d4,
                    flat = src.VersatileDamageValue.flat,
                },
                DamageType = src.DamageType,
                WeightProperty = src.WeaponWeight,
                Reach = src.Reach,
                Finesse = src.Finesse,
                Thrown = src.Thrown,
                RangeThrown = src.Range,
                EffectsOnWearer = src.R_EffectsOnEquip.Select(effect => new MeleeWeaponFormDto.Body.EffectBlueprintDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                EffectsOnItem = src.R_AffectedBy.Select(effect => new MeleeWeaponFormDto.Body.EffectBlueprintDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                Powers = src.R_EquipItemGrantsAccessToPower.Select(effect => new MeleeWeaponFormDto.Body.PowerDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                ResourcesOnEquip = src.R_ItemGrantsResources
                    .GroupBy(x => new {x.R_BlueprintId, x.Level, x.R_Blueprint.Name})
                    .Select(g => new RangedWeaponFormDto.Body.ResourceDto(){
                        BlueprintId = g.Key.R_BlueprintId,
                        Name = g.Key.Name,
                        Count = g.Count(),
                        Level = g.Key.Level
                    }).ToList(),
                Slots = src.R_ItemIsEquippableInSlots.Select(slot => new MeleeWeaponFormDto.Body.SlotDto(){
                    Id = slot.Id,
                    Name = slot.Name
                }).ToList()
            }));
        CreateMap<MeleeWeaponFormDto, MeleeWeapon>()
            .IncludeBase<ItemFormDto, Item>()
            .ForMember(dest => dest.IsSpellFocus, opt => opt.MapFrom(src => src.ItemTypeBody.IsSpellFocus))
            .ForMember(dest => dest.OccupiesAllSlots, opt => opt.MapFrom(src => src.ItemTypeBody.OccupiesAllSlots))
            .ForPath(dest => dest.DamageValue.d100, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d100))
            .ForPath(dest => dest.DamageValue.d20, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d20))
            .ForPath(dest => dest.DamageValue.d12, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d12))
            .ForPath(dest => dest.DamageValue.d10, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d10))
            .ForPath(dest => dest.DamageValue.d8, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d8))
            .ForPath(dest => dest.DamageValue.d6, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d6))
            .ForPath(dest => dest.DamageValue.d4, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d4))
            .ForPath(dest => dest.DamageValue.flat, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.flat))
            .ForPath(dest => dest.VersatileDamageValue.d100, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.d100))
            .ForPath(dest => dest.VersatileDamageValue.d20, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.d20))
            .ForPath(dest => dest.VersatileDamageValue.d12, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.d12))
            .ForPath(dest => dest.VersatileDamageValue.d10, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.d10))
            .ForPath(dest => dest.VersatileDamageValue.d8, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.d8))
            .ForPath(dest => dest.VersatileDamageValue.d6, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.d6))
            .ForPath(dest => dest.VersatileDamageValue.d4, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.d4))
            .ForPath(dest => dest.VersatileDamageValue.flat, opt => opt.MapFrom(src => src.ItemTypeBody.VersatileDamage.flat))
            .ForMember(dest => dest.DamageType, opt => opt.MapFrom(src => src.ItemTypeBody.DamageType))
            .ForMember(dest => dest.WeaponWeight, opt => opt.MapFrom(src => src.ItemTypeBody.WeightProperty))
            .ForMember(dest => dest.Reach, opt => opt.MapFrom(src => src.ItemTypeBody.Reach))
            .ForMember(dest => dest.Finesse, opt => opt.MapFrom(src => src.ItemTypeBody.Finesse))
            .ForMember(dest => dest.Thrown, opt => opt.MapFrom(src => src.ItemTypeBody.Thrown))
            .ForMember(dest => dest.Range, opt => opt.MapFrom(src => src.ItemTypeBody.RangeThrown));


        CreateMap<RangedWeapon, RangedWeaponFormDto>()
            .IncludeBase<Item, ItemFormDto>()
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => "rangedWeapon"))
            .ForMember(dest => dest.ItemTypeBody, opt => opt.MapFrom((src) => new RangedWeaponFormDto.Body(){
                IsSpellFocus = src.IsSpellFocus,
                OccupiesAllSlots = src.OccupiesAllSlots,
                Damage = new RangedWeaponFormDto.Body.DiceSetFormDto(){
                    d100 = src.DamageValue.d100,
                    d20 = src.DamageValue.d20,
                    d12 = src.DamageValue.d12,
                    d10 = src.DamageValue.d10,
                    d8 = src.DamageValue.d8,
                    d6 = src.DamageValue.d6,
                    d4 = src.DamageValue.d4,
                    flat = src.DamageValue.flat,
                },
                DamageType = src.DamageType,
                WeightProperty = src.WeaponWeight,
                Range = src.Range,
                Loaded = src.Loaded,
                EffectsOnWearer = src.R_EffectsOnEquip.Select(effect => new RangedWeaponFormDto.Body.EffectBlueprintDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                EffectsOnItem = src.R_AffectedBy.Select(effect => new RangedWeaponFormDto.Body.EffectBlueprintDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                Powers = src.R_EquipItemGrantsAccessToPower.Select(effect => new RangedWeaponFormDto.Body.PowerDto(){
                        Id = effect.Id,
                        Name = effect.Name
                    }).ToList(),
                ResourcesOnEquip = src.R_ItemGrantsResources
                    .GroupBy(x => new {x.R_BlueprintId, x.Level, x.R_Blueprint.Name})
                    .Select(g => new RangedWeaponFormDto.Body.ResourceDto(){
                        BlueprintId = g.Key.R_BlueprintId,
                        Name = g.Key.Name,
                        Count = g.Count(),
                        Level = g.Key.Level
                    }).ToList(),
                Slots = src.R_ItemIsEquippableInSlots.Select(slot => new RangedWeaponFormDto.Body.SlotDto(){
                    Id = slot.Id,
                    Name = slot.Name
                }).ToList()
            }));
        CreateMap<RangedWeaponFormDto, RangedWeapon>()
            .IncludeBase<ItemFormDto, Item>()
            .ForMember(dest => dest.IsSpellFocus, opt => opt.MapFrom(src => src.ItemTypeBody.IsSpellFocus))
            .ForMember(dest => dest.OccupiesAllSlots, opt => opt.MapFrom(src => src.ItemTypeBody.OccupiesAllSlots))
            .ForPath(dest => dest.DamageValue.d100, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d100))
            .ForPath(dest => dest.DamageValue.d20, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d20))
            .ForPath(dest => dest.DamageValue.d12, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d12))
            .ForPath(dest => dest.DamageValue.d10, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d10))
            .ForPath(dest => dest.DamageValue.d8, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d8))
            .ForPath(dest => dest.DamageValue.d6, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d6))
            .ForPath(dest => dest.DamageValue.d4, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.d4))
            .ForPath(dest => dest.DamageValue.flat, opt => opt.MapFrom(src => src.ItemTypeBody.Damage.flat))
            .ForMember(dest => dest.DamageType, opt => opt.MapFrom(src => src.ItemTypeBody.DamageType))
            .ForMember(dest => dest.WeaponWeight, opt => opt.MapFrom(src => src.ItemTypeBody.WeightProperty))
            .ForMember(dest => dest.Range, opt => opt.MapFrom(src => src.ItemTypeBody.Range))
            .ForMember(dest => dest.Loaded, opt => opt.MapFrom(src => src.ItemTypeBody.Loaded));

        CreateMap<EquipmentSlot, SlotDto>();

        // CreateMap<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<AbilityEffectBlueprint, AbilityEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<ActionEffectBlueprint, ActionEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<ArmorClassEffectBlueprint, ArmorClassEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<AttackPerAttackActionEffectBlueprint, AttackPerAttackActionEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<AttackRollEffectBlueprint, AttackRollEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<DamageEffectBlueprint, DamageEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<DummyEffectBlueprint, DummyEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<HealingEffectBlueprint, HealingEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<HitpointEffectBlueprint, HitpointEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<InitiativeEffectBlueprint, InitiativeEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<LanguageEffectBlueprint, LanguageEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<MagicEffectBlueprint, MagicEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<MovementCostEffectBlueprint, MovementCostEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<MovementEffectBlueprint, MovementEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<OffHandAttackEffectBlueprint, OffHandAttackEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<ProficiencyEffectBlueprint, ProficiencyEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<ResistanceEffectBlueprint, ResistanceEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<SavingThrowEffectBlueprint, SavingThrowEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<SizeEffectBlueprint, SizeEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<SkillEffectBlueprint, SkillEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<StatusEffectBlueprint, StatusEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();
        // CreateMap<ValueEffectBlueprint, ValueEffectInstance>()
        //     .IncludeBase<EffectBlueprint, EffectInstance>().ReverseMap();

        CreateMap<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src is MovementEffectBlueprint ? "movementEffect" :
                src is SavingThrowEffectInstance ? "savingThrow" :
                src is AbilityEffectInstance ? "abilityCheck" :
                src is SkillEffectInstance ? "skillCheck" :
                src is ResistanceEffectInstance ? "resistance" :
                src is AttackRollEffectInstance ? "attackBonus" :
                src is ArmorClassEffectInstance ? "armorClassBonus" :
                src is ProficiencyEffectInstance ? "proficiency" :
                src is HealingEffectInstance ? "healing" :
                src is ActionEffectInstance ? "actions" :
                src is MagicEffectInstance ? "magicItemStatus" :
                src is SizeEffectInstance ? "size" :
                src is InitiativeEffectInstance ? "initiative" :
                src is DamageEffectInstance ? "damage" :
                src is HitpointEffectInstance ? "hitpoints" :
                src is AttackPerAttackActionEffectInstance ? "attacksPerAction" :
                src is StatusEffectInstance ? "statusEffect" :
                src is MovementCostEffectInstance ? "movementCost" : "UNREACHABLE")
            )
            .ForMember(dest => dest.DurationLeft, opt => {opt.PreCondition( src => src.R_OwnedByGroup != null); opt.MapFrom(src => src.R_OwnedByGroup.DurationLeft);});

        // CreateMap<DiceSet, ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>()
        //     .ConvertUsing<DiceSetToDtoConverter>();

        CreateMap<ActionEffectInstance, ActionEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ActionEffectBlueprintFormDto.ActionSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType
                })
            );

        CreateMap<MovementEffectInstance, MovementEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new MovementEffectBlueprintFormDto.MovementSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType
                })
            );

        CreateMap<SavingThrowEffectInstance, SavingThrowEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new SavingThrowEffectBlueprintFormDto.SavingThrowSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<AbilityEffectInstance, AbilityEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new AbilityEffectBlueprintFormDto.AbilitySubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<SkillEffectInstance, SkillEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new SkillEffectBlueprintFormDto.SkillSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<ResistanceEffectInstance, ResistanceEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ResistanceEffectBlueprintFormDto.ResistanceSubeffectBlueprintFormDto(){
                    effectType = src.EffectType,
                })
            );

        CreateMap<AttackRollEffectInstance, AttackRollEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new AttackRollEffectBlueprintFormDto.AttackRollSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<ArmorClassEffectInstance, ArmorClassEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ArmorClassEffectBlueprintFormDto.ArmorClassSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<ProficiencyEffectInstance, ProficiencyEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new ProficiencyEffectBlueprintFormDto.ProficiencySubeffectBlueprintFormDto(){
                    GrantsProficiencyInItemFamilyId = src.R_GrantsProficiencyInItemFamilyId,
                    effectType = src.ProficiencyEffectType
                })
            );

        CreateMap<HealingEffectInstance, HealingEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new HealingEffectBlueprintFormDto.HealingSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<MagicEffectInstance, MagicEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new MagicEffectBlueprintFormDto.MagicSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<SizeEffectInstance, SizeEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new SizeEffectBlueprintFormDto.SizeSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<InitiativeEffectInstance, InitiativeEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new InitiativeEffectBlueprintFormDto.InitiativeSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                })
            );

        CreateMap<DamageEffectInstance, DamageEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new DamageEffectBlueprintFormDto.DamageSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<HitpointEffectInstance, HitpointEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new HitpointEffectBlueprintFormDto.HitpointSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<AttackPerAttackActionEffectInstance, AttackPerAttackActionEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new AttackPerAttackActionEffectBlueprintFormDto.AttackPerAttackActionSubeffectBlueprintFormDto(){
                    value = context.Mapper.Map<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>(src.DiceSet),
                    effectType = src.EffectType,
                })
            );

        CreateMap<StatusEffectInstance, StatusEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new StatusEffectBlueprintFormDto.StatusSubeffectBlueprintFormDto(){
                    effectType = src.EffectType
                })
            );

        CreateMap<MovementCostEffectInstance, MovementCostEffectBlueprintFormDto>()
            .IncludeBase<EffectInstance, EffectBlueprintFormDto>()
            .ForMember(
                dest => dest.EffectTypeBody,
                opt => opt.MapFrom((src, dest, member, context) => new MovementCostEffectBlueprintFormDto.MovementCostSubeffectBlueprintFormDto(){
                    effectType = src.EffectType
                })
            );

        
        // CreateMap<EffectBlueprintFormDto, EffectBlueprint>()
        //     .ConvertUsing<EffectBlueprintConverter>();

        CreateMap<EffectBlueprintFormDto, EffectInstance>();
            

        CreateMap<ActionEffectBlueprintFormDto, ActionEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<HealingEffectBlueprintFormDto, HealingEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<MovementEffectBlueprintFormDto, MovementEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<SavingThrowEffectBlueprintFormDto, SavingThrowEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<AbilityEffectBlueprintFormDto, AbilityEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<SkillEffectBlueprintFormDto, SkillEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<ResistanceEffectBlueprintFormDto, ResistanceEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => ((ResistanceEffectBlueprintFormDto.ResistanceSubeffectBlueprintFormDto)src.EffectTypeBody).effectType)
            );

        CreateMap<AttackRollEffectBlueprintFormDto, AttackRollEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<ArmorClassEffectBlueprintFormDto, ArmorClassEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<ProficiencyEffectBlueprintFormDto, ProficiencyEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.R_GrantsProficiencyInItemFamilyId,
                opt => opt.MapFrom(src => src.EffectTypeBody.GrantsProficiencyInItemFamilyId)
            )
            .ForMember(
                dest => dest.ProficiencyEffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<HealingEffectBlueprintFormDto, HealingEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<MagicEffectBlueprintFormDto, MagicEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<SizeEffectBlueprintFormDto, SizeEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<InitiativeEffectBlueprintFormDto, InitiativeEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            );

        CreateMap<DamageEffectBlueprintFormDto, DamageEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<HitpointEffectBlueprintFormDto, HitpointEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<AttackPerAttackActionEffectBlueprintFormDto, AttackPerAttackActionEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.RollMoment)
            )
            .ForMember(
                dest => dest.DiceSet,
                opt => opt.MapFrom(src => src.EffectTypeBody.value)
            )
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<StatusEffectBlueprintFormDto, StatusEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );

        CreateMap<MovementCostEffectBlueprintFormDto, MovementCostEffectInstance>()
            .IncludeBase<EffectBlueprintFormDto, EffectInstance>()
            .ForMember(
                dest => dest.EffectType,
                opt => opt.MapFrom(src => src.EffectTypeBody.effectType)
            );
    }
}
