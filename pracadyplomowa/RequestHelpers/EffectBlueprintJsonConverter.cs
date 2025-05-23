using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.RequestHelpers
{
    public class EffectBlueprintJsonConverter : JsonConverter<EffectBlueprintFormDto>
    {
        public override EffectBlueprintFormDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = jsonDocument.RootElement;
                
                // Extract the "effectType" field
                var effectType = jsonObject.GetProperty("effectType").GetString();

                // other properties
                var id = jsonObject.TryGetProperty("id", out var idProp) && idProp.ValueKind != JsonValueKind.Null
                        ? idProp.GetInt32()
                        : (int?)null;
                var Name = jsonObject.TryGetProperty("name", out var nameProp) ? nameProp.GetString() ?? string.Empty : string.Empty;
                var Description = jsonObject.TryGetProperty("description", out var descProp) ? descProp.GetString() ?? string.Empty : string.Empty;
                var DurationLeft = jsonObject.TryGetProperty("durationLeft", out var resourceLevelProp) ? resourceLevelProp.GetInt32() : 0;
                var ResourceLevel = jsonObject.TryGetProperty("resourceLevel", out var durationLeftProp) ? durationLeftProp.GetInt32() : 0;
                var ResourceAmount = jsonObject.TryGetProperty("resourceAmount", out var resourceAmountProp) ? resourceAmountProp.GetInt32() : 0;
                var SavingThrowSuccess = jsonObject.TryGetProperty("savingThrowSuccess", out var savingThrowSuccessProp) && savingThrowSuccessProp.GetBoolean();
                var Conditional = jsonObject.TryGetProperty("conditional", out var conditionalProp) && conditionalProp.GetBoolean();
                var IsImplemented = jsonObject.TryGetProperty("isImplemented", out var isImplementedProp) && isImplementedProp.GetBoolean();
                var HasNoEffectInCombat = jsonObject.TryGetProperty("hasNoEffectInCombat", out var hasNoEffectInCombatProp) && hasNoEffectInCombatProp.GetBoolean();
                var EffectType = effectType;
                var EffectTypeBody = jsonObject.TryGetProperty("effectTypeBody", out var effectTypeBodyProp);

                // Create a new DTO instance and populate all properties
                EffectBlueprintFormDto dto = new EffectBlueprintFormDto();

                // Deserialize EffectTypeBody based on effectType
#pragma warning disable CS8601 // Possible null reference assignment.
                switch (effectType)
                {
                    case "movementEffect":
                        dto = new MovementEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<MovementEffectBlueprintFormDto.MovementSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "savingThrow":
                        dto = new SavingThrowEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<SavingThrowEffectBlueprintFormDto.SavingThrowSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "abilityCheck":
                        dto = new AbilityEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<AbilityEffectBlueprintFormDto.AbilitySubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "skillCheck":
                        dto = new SkillEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<SkillEffectBlueprintFormDto.SkillSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "resistance":
                        dto = new ResistanceEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<ResistanceEffectBlueprintFormDto.ResistanceSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "attackBonus":
                        dto = new AttackRollEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<AttackRollEffectBlueprintFormDto.AttackRollSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "armorClassBonus":
                        dto = new ArmorClassEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<ArmorClassEffectBlueprintFormDto.ArmorClassSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "proficiency":
                        dto = new ProficiencyEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<ProficiencyEffectBlueprintFormDto.ProficiencySubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "language":
                        dto = new LanguageEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<LanguageEffectBlueprintFormDto.LanguageSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "healing":
                        dto = new HealingEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<HealingEffectBlueprintFormDto.HealingSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "actions":
                        dto = new ActionEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<ActionEffectBlueprintFormDto.ActionSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "magicItemStatus":
                        dto = new MagicEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<MagicEffectBlueprintFormDto.MagicSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "size":
                        dto = new SizeEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<SizeEffectBlueprintFormDto.SizeSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "initiative":
                        dto = new InitiativeEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<InitiativeEffectBlueprintFormDto.InitiativeSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "damage":
                        dto = new DamageEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<DamageEffectBlueprintFormDto.DamageSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "hitpoints":
                        dto = new HitpointEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<HitpointEffectBlueprintFormDto.HitpointSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "attacksPerAction":
                        dto = new AttackPerAttackActionEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<AttackPerAttackActionEffectBlueprintFormDto.AttackPerAttackActionSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "statusEffect":
                        dto = new StatusEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<StatusEffectBlueprintFormDto.StatusSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "movementCost":
                        dto = new MovementCostEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<MovementCostEffectBlueprintFormDto.MovementCostSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    case "dummy":
                        dto = new DummyEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            DurationLeft = DurationLeft,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = EffectTypeBody
                                ? JsonSerializer.Deserialize<DummyEffectBlueprintFormDto.DummySubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                                : null
                        };
                        break;
                    default:
                        throw new JsonException($"Unknown effectType: {effectType}");
#pragma warning restore CS8601 // Possible null reference assignment.
                }

                return dto;
            }
        }

        public override void Write(Utf8JsonWriter writer, EffectBlueprintFormDto value, JsonSerializerOptions options) // FOR SOME REASON THIS NEVER GETS CALLED
        {
            // Use the default serialization behavior
            var x = JsonSerializer.Serialize(value, options);
            JsonSerializer.Serialize(writer, value, options);
        }
    }

}