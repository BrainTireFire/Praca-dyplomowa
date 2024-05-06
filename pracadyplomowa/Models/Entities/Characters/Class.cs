using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Class : ObjectWithId
    {
        public string Name { get; set; }
        public string MaximumPreparedSpellsFormula { get; set; }
    }
}