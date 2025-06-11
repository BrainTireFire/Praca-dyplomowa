using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs
{
    public class ShopCharacterDto
    {
        public List<ItemGetDto> Items { get; set; } = [];
    }
}