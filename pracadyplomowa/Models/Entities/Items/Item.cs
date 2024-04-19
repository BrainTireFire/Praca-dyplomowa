using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities;

namespace pracadyplomowa.Models
{
    public class Item : ObjectWithOwner
    {
        public string Name { get; set; }
    }
}