using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class CastPowerResultDto {
        public Dictionary<int, HitType> HitMap { get; set; } = new();
        public Dictionary<int, string> NameMap { get; set; } = new();
    }
}