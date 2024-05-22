using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Items;

public class Backpack : ObjectWithId
{
    //Ids and keys
    public int BackpackOfCharacterId { get; set; }
    
    //Relationships
    public virtual Character R_BackpackOfCharacter { get; set; } = null!;
    
    public virtual ICollection<Item> R_BackpackHasItems { get; set; } = [];
}