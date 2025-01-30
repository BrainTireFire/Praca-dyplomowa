namespace pracadyplomowa.Models.DTOs.Session;

public class PowerCastOverlayDto
{
    public int SourceId { get; set; }
    public int CampaignId { get; set; }
    public int PowerId { get; set; }
    public ICollection<int> PowerTargetIds { get; set; } = new List<int>();
}