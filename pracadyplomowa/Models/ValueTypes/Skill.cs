using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.ValueTypes
{
    public class Skill
    {
        public string Name { get; set; } = null!;
        public string? Ability { get; set; } = null!;
        public int Value { get; set; }
        public bool Proficient { get; set; }
    }
}