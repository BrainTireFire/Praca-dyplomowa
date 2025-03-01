using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs.Encounter;

public class SetEncounterPositionDto
{
    public bool IsActive { get; set; }
    public ICollection<UpdateFieldDto> FieldsToUpdate { get; set; } = new List<UpdateFieldDto>();
}