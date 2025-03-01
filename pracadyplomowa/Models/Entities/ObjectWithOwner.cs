namespace pracadyplomowa.Models.Entities
{
        public abstract class ObjectWithOwner : ObjectWithId
        {
                public User? R_Owner { get; set; }
                public int? R_OwnerId { get; set; }

                public ObjectWithOwner()
                {
                }

                public ObjectWithOwner(ObjectWithOwner self){
                        R_OwnerId = self.R_OwnerId;
                }
        }
}