namespace pracadyplomowa.Models.Entities
{
    public class ObjectWithOwner : ObjectWithId
    {
        public User Owner {get; set;}

        public int OwnerId {get; set;}
    }
}