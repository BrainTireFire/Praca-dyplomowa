using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Items;

public class Backpack : ObjectWithId
{
    //Relationships
    public virtual Character R_BackpackOfCharacter { get; set; } = null!;
    public int BackpackOfCharacterId { get; set; }
    
    public virtual ICollection<Item> R_BackpackHasItems { get; set; } = [];
}