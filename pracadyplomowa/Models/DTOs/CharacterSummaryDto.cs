using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterSummaryDto(int id, string name, string description, string characterClass, string race)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public string Class { get; set; } = characterClass;
        public string Race { get; set; } = race;
    }
}