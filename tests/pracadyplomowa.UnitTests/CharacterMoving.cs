using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using Xunit;

namespace pracadyplomowa.UnitTests.CharacterTests
{
    public class CharacterMoving
    {
        [Fact]
        public void Test1()
        {
            Class testClass = new("Test class")
            {
                Id = 1
            };
            for (int i = 0; i < 20; i++){
                testClass.R_ClassLevels.Add(new ClassLevel(i){Id = i, R_Class = testClass, R_ClassId = testClass.Id, HitPoints = 5});
            }
            Race race = new(){Name = "test", Size = Size.Large, Speed = 30};
            for (int i = 0; i < 20; i++){
                race.R_RaceLevels.Add(new RaceLevel(){Id = i, R_Race = race, R_RaceId = race.Id, Level = i});
            }
            EquipmentSlot slot1 = new(){
                Name = "Main hand",
                Type = SlotType.MainHand
            };
            slot1.R_Races.Add(race);
            EquipmentSlot slot2 = new(){
                Name = "Off hand",
                Type = SlotType.OffHand
            };
            slot2.R_Races.Add(race);
            race.R_EquipmentSlots.AddRange([slot1, slot2]);
            Character character = new("Test", false, 14, 8, 6, 4, 2, 12, testClass.R_ClassLevels[0], race, -1, 0);
            Character target = new("Test", false, 14, 8, 6, 4, 2, 12, testClass.R_ClassLevels[0], race, -1, 0);
            Encounter enc = new Encounter(){
                Id = 1,
            };
            Board board = new Board(){
                R_Encounter = enc,
            };
            enc.R_Board = board;
            enc.R_BoardId = board.Id;
            Field field1 = new Field(){
                PositionX = 1,
                PositionY = 1,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field2 = new Field(){
                PositionX = 1,
                PositionY = 2,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field3 = new Field(){
                PositionX = 1,
                PositionY = 3,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field4 = new Field(){
                PositionX = 1,
                PositionY = 4,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field5 = new Field(){
                PositionX = 1,
                PositionY = 5,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field6 = new Field(){
                PositionX = 1,
                PositionY = 6,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field7 = new Field(){
                PositionX = 2,
                PositionY = 1,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field8 = new Field(){
                PositionX = 2,
                PositionY = 2,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field9 = new Field(){
                PositionX = 2,
                PositionY = 3,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field10 = new Field(){
                PositionX = 2,
                PositionY = 4,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field11 = new Field(){
                PositionX = 2,
                PositionY = 5,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field12 = new Field(){
                PositionX = 2,
                PositionY = 6,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field13 = new Field(){
                PositionX = 3,
                PositionY = 1,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field14 = new Field(){
                PositionX = 3,
                PositionY = 2,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field15 = new Field(){
                PositionX = 3,
                PositionY = 3,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field16 = new Field(){
                PositionX = 3,
                PositionY = 4,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field17 = new Field(){
                PositionX = 3,
                PositionY = 5,
                R_Board = board,
                R_BoardId = board.Id
            };
            Field field18 = new Field(){
                PositionX = 3,
                PositionY = 6,
                R_Board = board,
                R_BoardId = board.Id
            };
            List<Field> x = [field1, field2, field3, field4, field5, field6, field7, field8, field9, field10, field11, field12];
            x.ForEach(board.AddField);
            ParticipanceData part1 = new ParticipanceData(){
                Id = 1,
                R_Encounter = enc,
                R_EncounterId = enc.Id,
                R_Character = character,
                R_CharacterId = character.Id,
                R_OccupiedField = field7
            };
            field1.R_OccupiedBy = part1;
            field1.R_OccupiedById = part1.Id;
            ParticipanceData part2 = new ParticipanceData(){
                Id = 2,
                R_Encounter = enc,
                R_EncounterId = enc.Id,
                R_Character = target,
                R_CharacterId = target.Id,
                R_OccupiedField = field5
            };
            field5.R_OccupiedBy = part2;
            field5.R_OccupiedById = part2.Id;
            enc.R_Participances.Add(part1);
            enc.R_Participances.Add(part2);

            List<Field> path = [field7, field8, field9, field10, field11, field12];

            var traversablePath = character.CanTraversePath(path);
            Assert.Same(traversablePath[0], field7);
            Assert.Same(traversablePath[1], field8);
            Assert.Same(traversablePath[2], field9);
            // Assert.Same(traversablePath[3], field4);
            Assert.Subset(path.ToHashSet(), traversablePath.ToHashSet());
            Assert.Equal(3, traversablePath.Count);
        }
    }
}