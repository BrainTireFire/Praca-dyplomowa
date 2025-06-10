using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Items;

public class Backpack : ObjectWithId
{
    //Owned
    public virtual CoinSack CoinSack { get; set; } = null!;

    //Relationships
    public virtual Character R_BackpackOfCharacter { get; set; } = null!;
    public int R_BackpackOfCharacterId { get; set; }

    public virtual ICollection<Item> R_BackpackHasItems { get; set; } = [];

    public Backpack() : base()
    {
        this.CoinSack = new CoinSack();
    }
}