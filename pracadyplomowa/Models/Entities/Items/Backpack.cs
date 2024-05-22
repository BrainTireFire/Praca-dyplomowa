namespace pracadyplomowa.Models.Entities.Items;

public class Backpack : ObjectWithOwner
{
    //Relationships
    public virtual ICollection<Item> R_BackpackHasItems { get; set; } = [];
}