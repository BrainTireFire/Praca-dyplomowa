using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.DTOs
{
    public class CampaignDto(int id, string name, string description)
    {
        public int Id { get; set; } = id;
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = name;

        public string Description { get; set; } = description;
    }
}