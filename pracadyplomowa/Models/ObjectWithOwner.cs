using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models
{
    public class ObjectWithOwner
    {
        public int Id { get; set; }
        public User Owner {get; set;}

        public int OwnerId {get; set;}
    }
}