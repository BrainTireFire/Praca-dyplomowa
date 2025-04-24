using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.DTOs
{
    public class ShopItemDto(int id, string name, int weight, string description, int gold, int silver, int copper, int campaignId)
    {
        [Required]
        public int Id { get; set; } = id;
        [MaxLength(50)]
        public string Name { get; set; } = name;
        public int Weight { get; set; } = weight;
        [MaxLength(50)]
        public string Description { get; set; } = description;
        public CoinPurseDto Price { get; set; } = new CoinPurseDto
        {
            GoldPieces = gold,
            SilverPieces = silver,
            CopperPieces = copper
        };
        [Required]
        public int CampaignId { get; set; } = campaignId;
    }
}