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
        
        public void UpdateParticipanceData(ParticipanceData newParticipanceData)
        {
            if (newParticipanceData == null)
            {
                throw new ArgumentNullException(nameof(newParticipanceData), "ParticipanceData cannot be null.");
            }

            // Remove this field from the old participance, if any
            if (R_OccupiedBy != null)
            {
                // Remove the field from all old participance's occupied fields
                foreach (var field in R_OccupiedBy.R_OccupiedFields.ToList())
                {
                    field.R_OccupiedBy = null;  // Clear the reference to this field in the old participance
                }

                // Optionally, clear all occupied fields (depending on how you want to manage them)
                R_OccupiedBy.R_OccupiedFields.Clear();
            }

            // Assign the new participance
            R_OccupiedBy = newParticipanceData;
            R_OccupiedById = newParticipanceData.Id;

            // Add this field to the new participance if not already in the list
            if (!newParticipanceData.R_OccupiedFields.Contains(this))
            {
                newParticipanceData.R_OccupiedFields.Add(this);
            }
        }

    }
}