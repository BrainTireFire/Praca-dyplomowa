using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class ClassDTO
    {

        public int Id { get; set; }
        
        public required string Name { get; set; }
    }
}