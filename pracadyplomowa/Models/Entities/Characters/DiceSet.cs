using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    [ComplexType]
    public class DiceSet
    {
        public int d20 { get; set; }
        public int d12 { get; set; }
        public int d10 { get; set; }
        public int d8 { get; set; }
        public int d6 { get; set; }
        public int d4 { get; set; }
        public int d100 { get; set; }
    }
}