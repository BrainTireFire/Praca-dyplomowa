using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs
{
    public class ShopCharacterDto
    {
        public int Id { get; set; }
        public List<ItemGetDto> Items { get; set; } = [];
        public CoinPurseDto CoinPurse { get; set; }
        public int ItemsWeight { get; set; } = 0;
    }
}