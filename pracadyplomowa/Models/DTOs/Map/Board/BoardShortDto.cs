namespace pracadyplomowa.Models.DTOs.Board;

public class BoardShortDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
}