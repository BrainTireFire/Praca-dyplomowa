using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterInsertDto(string name, int raceId, int startingClassId, int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        public string Name { get; set; } = name;
        public int RaceId { get; set; } = raceId;
        public int StartingClassId { get; set; } = startingClassId;
        public int Strength { get; set; } = strength;
        public int Dexterity { get; set; } = dexterity;
        public int Constitution { get; set; } = constitution;
        public int Intelligence { get; set; } = intelligence;
        public int Wisdom { get; set; } = wisdom;
        public int Charisma { get; set; } = charisma;
    }
}