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
                var ResourceLevel = jsonObject.TryGetProperty("resourceLevel", out var resourceLevelProp) ? resourceLevelProp.GetInt32() : 0;
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
                switch (effectType)
                {
                    case "actions":
                        dto = new ActionEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = jsonObject.TryGetProperty("effectTypeBody", out var effectTypeBodyProp_actions)
                                ? JsonSerializer.Deserialize<ActionEffectBlueprintFormDto.ActionSubeffectBlueprintFormDto>(effectTypeBodyProp_actions.GetRawText(), options)
                                : null
                        };
                        break;
                    case "healing":
                        dto = new HealingEffectBlueprintFormDto(){
                            Id = id,
                            Name = Name,
                            Description = Description,
                            ResourceLevel = ResourceLevel,
                            ResourceAmount = ResourceAmount,
                            SavingThrowSuccess = SavingThrowSuccess,
                            Conditional = Conditional,
                            IsImplemented = IsImplemented,
                            HasNoEffectInCombat = HasNoEffectInCombat,
                            EffectType = EffectType,
                            EffectTypeBody = jsonObject.TryGetProperty("effectTypeBody", out var effectTypeBodyProp_healing)
                                ? JsonSerializer.Deserialize<HealingEffectBlueprintFormDto.HealingSubeffectBlueprintFormDto>(effectTypeBodyProp_healing.GetRawText(), options)
                                : null
                        };
                        break;
                    default:
                        throw new JsonException($"Unknown effectType: {effectType}");
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