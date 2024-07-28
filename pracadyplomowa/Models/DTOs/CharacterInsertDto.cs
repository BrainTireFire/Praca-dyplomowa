using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterInsertDto(string name, int raceId, int startingClassId, int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = name;
        
        [Required]
        public int RaceId { get; set; } = raceId;
        
        [Required]
        public int StartingClassId { get; set; } = startingClassId;
        
        [Required]
        public int Strength { get; set; } = strength;
        
        [Required]
        public int Dexterity { get; set; } = dexterity;
        
        [Required]
        public int Constitution { get; set; } = constitution;
        
        [Required]
        public int Intelligence { get; set; } = intelligence;
        
        [Required]
        public int Wisdom { get; set; } = wisdom;
        
        [Required]
        public int Charisma { get; set; } = charisma;
    }
}