using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterSummaryDto(int id, string name, string description, string characterClass, string race)
    {
        [Required]
        public int Id { get; set; } = id;
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = name;
        
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = description;
        
        [Required]
        [MaxLength(50)]
        public string Class { get; set; } = characterClass;
        
        [Required]
        [MaxLength(50)]
        public string Race { get; set; } = race;
    }
}