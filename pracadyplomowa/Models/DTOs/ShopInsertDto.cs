using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs
{
    public class ShopInsertDto(string name, string type, string location, string description, int campaignId)
    {
        [MaxLength(50)]
        public string Name { get; set; } = name;
        [MaxLength(50)]
        public string Type { get; set; } = type;
        [MaxLength(50)]
        public string Location { get; set; } = location;
        public string Description { get; set; } = description;
        [Required]
        public int CampaignId { get; set; } = campaignId;
    }
}