using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Field : ObjectWithId
    {
        //Properties
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public string Color { get; set; } = null!;
        public string? Description { get; set; }
        public FieldCoverType FieldCoverLevel{ get; set; }
        public FieldMovementCostType FieldMovementCost { get; set; }


        //Relationships
        public virtual ICollection<Power> R_CasterPowers { get; set; } = [];
        public virtual Board R_Board { get; set; } = null!;
        public int R_BoardId { get; set; }
        public virtual ParticipanceData? R_OccupiedBy { get; set; }
        public int? R_OccupiedById { get; set; }
        public virtual ICollection<EffectGroup> R_EffectGroupOnField { get; set; } = [];
        
        public Field() { }
        
        public Field(int positionX, int positionY, int positionZ, string color, string fieldCoverLevelStr, string fieldMovementCostStr, string? description = null)
        {
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
            Color = color ?? throw new ArgumentNullException(nameof(color));
            
            if (!Enum.TryParse(fieldCoverLevelStr, true, out FieldCoverType fieldCoverLevel))
            {
                throw new ArgumentException($"Invalid field cover level: {fieldCoverLevelStr}");
            }
            FieldCoverLevel = fieldCoverLevel;
            if (!Enum.TryParse(fieldMovementCostStr, true, out FieldMovementCostType fieldMovementCost))
            {
                throw new ArgumentException($"Invalid field cover level: {fieldCoverLevelStr}");
            }
            FieldMovementCost = fieldMovementCost;
            Description = description;
        }
        
        public void AssignToBoard(Board board)
        {
            R_Board = board ?? throw new ArgumentNullException(nameof(board));
        }
    }
}