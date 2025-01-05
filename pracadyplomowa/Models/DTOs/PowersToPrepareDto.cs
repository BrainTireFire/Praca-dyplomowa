using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class PowersToPrepareDto
    {
        public List<PowerCompactDto> PowerList {get; set;} = [];
        public int numberToChoose {get; set;} = 0;
        public int classId {get; set;} = 0;
        public string className {get; set;} = "";
    }
}