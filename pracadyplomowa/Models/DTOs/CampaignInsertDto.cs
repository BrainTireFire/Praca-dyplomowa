using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs
{
    public class CampaignInsertDto(string name, bool isNpc, string description)
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = name;

        public bool IsNpc { get; set; } = isNpc;
        public string Description { get; set; } = description;
    }
}
