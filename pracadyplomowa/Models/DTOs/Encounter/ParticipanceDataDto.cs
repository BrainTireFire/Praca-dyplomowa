using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.DTOs.Encounter;

public record ParticipanceDataDto
{
    public int InitiativeOrder { get; set; }
    public bool IsSurprised { get; set; }
    public int NumberOfActionsTaken { get; set; }
    public int NumberOfBonusActionsTaken { get; set; }
    public int NumberOfAttacksTaken { get; set; }
    public int DistanceTraveled { get; set; }
    public ParticipanceCharacterSummaryDto Character { get; set; }
    public ICollection<FieldDto> OccupiedFields { get; set; }
}