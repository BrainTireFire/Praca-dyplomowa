using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs
{
    public class CampaignDto(int id, string name, string description, string invitationLink)
    {
        public int Id { get; set; } = id;
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = name;

        public string Description { get; set; } = description;

        public string InvitationLink { get; set; } = invitationLink;
    }
}