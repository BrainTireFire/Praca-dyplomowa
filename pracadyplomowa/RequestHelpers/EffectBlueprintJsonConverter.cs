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
    public class EffectBlueprintJsonConverter : JsonConverter<EffectBlueprintFormDtoWrapper>
{
    public override EffectBlueprintFormDtoWrapper Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            var jsonObject = jsonDocument.RootElement;
            
            // Extract the "effectType" field
            var formData = jsonObject.GetProperty("formData");
            var effectType = formData.GetProperty("effectType").GetString();

            // other properties
            var id = formData.TryGetProperty("id", out var idProp) && idProp.ValueKind != JsonValueKind.Null
                    ? idProp.GetInt32()
                    : (int?)null;
            var Name = formData.TryGetProperty("name", out var nameProp) ? nameProp.GetString() ?? string.Empty : string.Empty;
            var Description = formData.TryGetProperty("description", out var descProp) ? descProp.GetString() ?? string.Empty : string.Empty;
            var ResourceLevel = formData.TryGetProperty("resourceLevel", out var resourceLevelProp) ? resourceLevelProp.GetInt32() : 0;
            var ResourceAmount = formData.TryGetProperty("resourceAmount", out var resourceAmountProp) ? resourceAmountProp.GetInt32() : 0;
            var SavingThrowSuccess = formData.TryGetProperty("savingThrowSuccess", out var savingThrowSuccessProp) && savingThrowSuccessProp.GetBoolean();
            var Conditional = formData.TryGetProperty("conditional", out var conditionalProp) && conditionalProp.GetBoolean();
            var IsImplemented = formData.TryGetProperty("isImplemented", out var isImplementedProp) && isImplementedProp.GetBoolean();
            var HasNoEffectInCombat = formData.TryGetProperty("hasNoEffectInCombat", out var hasNoEffectInCombatProp) && hasNoEffectInCombatProp.GetBoolean();
            var EffectType = effectType;
            var EffectTypeBody = formData.TryGetProperty("effectTypeBody", out var effectTypeBodyProp)
                            ? JsonSerializer.Deserialize<EffectBlueprintFormDto.ActionEffectBlueprintFormDto.ActionSubeffectBlueprintFormDto>(effectTypeBodyProp.GetRawText(), options)
                            : null;

            // Create a new DTO instance and populate all properties
            EffectBlueprintFormDtoWrapper dtoWrapper = new EffectBlueprintFormDtoWrapper();

            // Deserialize EffectTypeBody based on effectType
            switch (effectType)
            {
                case "actions":
                    dtoWrapper.formData = new EffectBlueprintFormDto.ActionEffectBlueprintFormDto(){
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
                        EffectTypeBody = formData.TryGetProperty("effectTypeBody", out var effectTypeBodyProp2)
                            ? JsonSerializer.Deserialize<EffectBlueprintFormDto.ActionEffectBlueprintFormDto.ActionSubeffectBlueprintFormDto>(effectTypeBodyProp2.GetRawText(), options)
                            : null
                    };
                    break;
                default:
                    throw new JsonException($"Unknown effectType: {effectType}");
            }

            return dtoWrapper;
        }
    }

        public override void Write(Utf8JsonWriter writer, EffectBlueprintFormDtoWrapper value, JsonSerializerOptions options)
        {
            throw new UnreachableException(); // this should never be executed
        }
    }

}