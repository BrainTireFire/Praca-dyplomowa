using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Models.DTOs;

public class CampaignJoinDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public CampaignJoinDto(Campaign campaign)
    {
        Id = campaign.Id;
        Name = campaign.Name;
    }
}