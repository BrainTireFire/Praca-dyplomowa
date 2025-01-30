using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs.Session;

public class AuctionLogRequestDto
{
    [Required]
    public string GroupName { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public int CampaignId { get; set; }
    [Required]
    public int EncounterId { get; set; }
}