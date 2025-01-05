using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs.Encounter;

public class UpdateEncounterDto
{
    public ICollection<UpdateFieldDto> FieldsToUpdate { get; set; } = new List<UpdateFieldDto>();
}