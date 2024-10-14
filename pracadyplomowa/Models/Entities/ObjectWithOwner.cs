namespace pracadyplomowa.Models.Entities
{
    public class ObjectWithOwner : ObjectWithId
    {
        public User? R_Owner { get; set; }

        public int? R_OwnerId { get; set; }
    }
}