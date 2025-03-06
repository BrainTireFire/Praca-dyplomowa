using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterInsertDto : IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public bool IsNpc { get; set; }
        
        [Required]
        public int RaceId { get; set; }

        public int? StartingClassId { get; set; } // Nullable for NPCs
        
        [Required]
        public int Strength { get; set; }
        
        [Required]
        public int Dexterity { get; set; }
        
        [Required]
        public int Constitution { get; set; }
        
        [Required]
        public int Intelligence { get; set; }
        
        [Required]
        public int Wisdom { get; set; }
        
        [Required]
        public int Charisma { get; set; }
        
        public CharacterInsertDto() { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsNpc && StartingClassId == null)
            {
                yield return new ValidationResult("StartingClassId is required for non-NPC characters.", new[] { nameof(StartingClassId) });
            }
        }
    }
}
