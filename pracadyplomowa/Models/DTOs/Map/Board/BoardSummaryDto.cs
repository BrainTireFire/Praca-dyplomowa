using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Models.DTOs.Board;

public class BoardSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public ICollection<FieldDto> Fields { get; set; } = new List<FieldDto>();
}