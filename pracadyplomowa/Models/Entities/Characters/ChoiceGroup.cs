using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class ChoiceGroup : ObjectWithId
    {
        public int NumberToChoose { get; set; }

        public List<Power> Powers { get; set; } = [];

        public List<EffectBlueprint> Effects { get; set; } = [];
    }
}