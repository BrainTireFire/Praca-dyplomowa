using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs
{
    public class ItemGetDto
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int Weight { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public CoinPurseDto Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}