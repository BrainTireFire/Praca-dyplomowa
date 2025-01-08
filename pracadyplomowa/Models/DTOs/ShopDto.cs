using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Models.DTOs
{
    public class ShopDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        public string Description { get; set; }

        public ShopDto(Shop shop)
        {
            Id = shop.Id;
            Name = shop.Name;
            Type = shop.Type;
            Location = shop.Location;
            Description = shop.Description;
        }
    }
}