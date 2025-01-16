using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Board : ObjectWithOwner
    {
        //Properties
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int SizeX { get; set; }  
        public int SizeY { get; set; }

        //Relationship
        public virtual Encounter? R_Encounter { get; set; }
        public virtual ICollection<Field> R_ConsistsOfFields { get; set; } = new List<Field>();

        public Board() { }
        
        public Board(int ownerId, string name, int sizeX, int sizeY, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SizeX = sizeX;
            SizeY = sizeY;
            Description = description;
            R_OwnerId = ownerId;
        }
        
        public void AddField(Field field)
        {
            ArgumentNullException.ThrowIfNull(field);

            field.AssignToBoard(this);
            R_ConsistsOfFields.Add(field);
        }

        public Field[,] GetFieldsAs2DTable(){
            var grid = new Field[SizeX, SizeY];
            foreach(var field in R_ConsistsOfFields){
                grid[field.PositionX, field.PositionY] = field;
            }
            return grid;
        }
    }
}