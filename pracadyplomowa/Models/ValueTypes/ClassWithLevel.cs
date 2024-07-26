using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.ValueTypes
{
    public class ClassWithLevel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Level { get; set; }
    }
}