using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Models.DTOs.Board;

public class BoardCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    [Range(1, 100, ErrorMessage = "X must be between 1 and 100")]
    public int SizeX { get; set; }
    
    [Required]
    [Range(1, 100, ErrorMessage = "Y must be between 1 and 100")]
    public int SizeY { get; set; }
    
    [Required (ErrorMessage = "Fields are required")]
    public ICollection<FieldDto> Fields { get; set; }
}