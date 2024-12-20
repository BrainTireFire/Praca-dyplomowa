using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs
{
    public class CampaignInsertDto(string name, string description)
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = name;

        public string Description { get; set; } = description;
    }
}
