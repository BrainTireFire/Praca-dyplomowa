using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.DTOs.Map.Field;

namespace pracadyplomowa.Models.DTOs.Board;

public class BoardUpdateDto
{
    [MaxLength(50)]
    public string? Name { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Range(1, 100, ErrorMessage = "X must be between 1 and 100")]
    public int? SizeX { get; set; }
    
    [Range(1, 100, ErrorMessage = "Y must be between 1 and 100")]
    public int? SizeY { get; set; }
    
    public ICollection<FieldUpdateDto>? Fields { get; set; } = new List<FieldUpdateDto>();
}