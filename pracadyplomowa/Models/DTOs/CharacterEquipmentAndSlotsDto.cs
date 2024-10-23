using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterEquipmentAndSlotsDto
    {
        public List<Item> Items {get; set;} = [];
        public List<Slot> Slots {get; set;} = [];

        public class Item {
            public int Id { get; set;}
            public string Name { get; set;} = "";
            public ItemFamily ItemFamily { get; set;} = new ItemFamily();
            public List<Slot> Slots { get; set;} = new List<Slot>();
            public List<Slot> EquippableInSlots { get; set;} = new List<Slot>();
        }
        public class Slot {
            public int Id { get; set;}
            public string Name {get; set;} = "";
        }

            public class ItemFamily {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}