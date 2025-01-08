using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterDescriptionUpdateDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}