using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class RollDto
    {
        public RollDto()
        {
        }

        public int Roll { get; set; }
        public int Modifier { get; set; }
        public string Name { get; set; }
        public bool Executed { get; set; }
    }
}