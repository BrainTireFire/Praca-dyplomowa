namespace pracadyplomowa.Models.DTOs.Session;

public class WeaponAttackOverlayDto
{
    public int CampaignId { get; set; }
    public int TargetId { get; set; }
    public int SourceId { get; set; }
    public int WeaponId { get; set; }
    public bool IsRanged { get; set; }
    public int Range { get; set; }
}