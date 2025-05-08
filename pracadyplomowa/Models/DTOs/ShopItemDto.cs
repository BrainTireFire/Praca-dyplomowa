using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.DTOs
{
    public class ShopItemDto(int id, string name, int weight, string description, CoinPurseDto coinPurseDto, int quantity)
    {
        [Required]
        public int Id { get; set; } = id;
        [MaxLength(50)]
        public string Name { get; set; } = name;
        public int Weight { get; set; } = weight;
        [MaxLength(50)]
        public string Description { get; set; } = description;
        public CoinPurseDto Price { get; set; } = coinPurseDto;
        [Required]
        public int Quantity { get; set; } = quantity;
    }
}