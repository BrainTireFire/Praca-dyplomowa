using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class MovementCostEffectType
    {
        public int MovementCost_Multiplier { get; set; }
    }
}