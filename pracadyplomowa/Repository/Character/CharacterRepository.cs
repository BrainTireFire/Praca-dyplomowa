using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Repository
{
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {

        public CharacterRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId, CharacterParams characterParams)
        {
            var query = _context.Characters
                    .Where(c => c.R_OwnerId == OwnerId)
                    .Include(c => c.R_CharacterBelongsToRace)
                    .Include(c => c.R_CharacterHasLevelsInClass)
                    .ThenInclude(cl => cl.R_Class)
                    .AsQueryable();

            // Filtering
            query = query.ApplyFilter(characterParams.ClassName, c =>
                c.R_CharacterBelongsToRace.Name);

            query = query.ApplyFilter(characterParams.Name, c =>
                c.Name);

            // Sorting
            query = characterParams.OrderBy switch
            {
                "name" => query.OrderBy(c => c.Name),
                "nameDesc" => query.OrderByDescending(c => c.Name),
                _ => query.OrderBy(c => c.Name)
            };

            var charactersSumaries = query.Select(c => new CharacterSummaryDto(
                c.Id,
                c.Name,
                c.Description,
                c.R_CharacterBelongsToRace.Name,
                c.R_CharacterHasLevelsInClass.First().R_Class.Name
            ));

            return await PagedList<CharacterSummaryDto>.CreateAsync(charactersSumaries, characterParams.PageNumber, characterParams.PageSize);
        }

        public Task<Character> GetByIdWithAll(int Id)
        {

            var characterLevel = _context.Characters
            .Where(c => c.Id == Id).Include(c => c.R_CharacterHasLevelsInClass).Count();

            var character = _context.Characters
            .Where(c => c.Id == Id)

            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel))
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels)
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels)
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_EquipmentSlots)

            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_PowersToPrepare)

            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_EffectsGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_PowersAlwaysAvailableGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_PowersToPrepareGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ResourcesGranted)


            .Include(c => c.R_PowersKnown)
                .ThenInclude(p => p.R_EffectBlueprints)
            .Include(c => c.R_PowersKnown)
                .ThenInclude(p => p.R_UsesImmaterialResource)

            .Include(c => c.R_PowersPrepared)
                .ThenInclude(p => p.R_EffectBlueprints)
            .Include(c => c.R_PowersPrepared)
                .ThenInclude(p => p.R_UsesImmaterialResource)

            .Include(c => c.R_AffectedBy)
                .ThenInclude(eg => eg.R_OwnedByGroup)

            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(b => b.R_EffectsOnEquip)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(b => b.R_EquipItemGrantsAccessToPower)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(b => b.R_ItemIsEquippableInSlots)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(b => b.R_EquipData)
                        .ThenInclude(b => b.R_Slots)

            .Include(c => c.R_EquippedItems)
                .ThenInclude(ed => ed.R_Item)
                    .ThenInclude(b => b.R_EffectsOnEquip)
            .Include(c => c.R_EquippedItems)
                .ThenInclude(ed => ed.R_Item)
                    .ThenInclude(b => b.R_EquipItemGrantsAccessToPower)
            .Include(c => c.R_EquippedItems)
                .ThenInclude(ed => ed.R_Item)
                    .ThenInclude(b => b.R_ItemIsEquippableInSlots)

            .Include(c => c.R_ImmaterialResourceInstances)
                .ThenInclude(iri => iri.R_Blueprint)

            .AsSplitQuery() // IMPORTANT !!!!! https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithChoiceGroups(int Id)
        {
            var characterLevel = _context.Characters
            .Where(c => c.Id == Id).Include(c => c.R_CharacterHasLevelsInClass).Count();

            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_AffectedBy)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel))
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels)
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels)
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_Resources)
                            .ThenInclude(r => r.R_Blueprint)

            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_PowersAlwaysAvailable)

            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_EffectsGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_PowersAlwaysAvailableGranted)

            .AsSplitQuery() // IMPORTANT !!!!! https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithClassLevels(int Id)
        {
            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_Class)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetCharacterEquipmentAndSlots(int id){
            var character = _context.Characters
            .Where(c => c.Id == id)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_ItemIsEquippableInSlots)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_EquipData)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_ItemInItemsFamily)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_EquipmentSlots)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithCustomResources(int Id)
        {
            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_ImmaterialResourceInstances)
                .ThenInclude(iri => iri.R_Blueprint)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithKnownPowers(int Id)
        {
            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_PowersKnown)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithPreparedPowers(int Id)
        {
            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_PowersPrepared)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithPowersToPrepare(int Id)
        {
            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_UsedChoiceGroups)
            .ThenInclude(c => c.R_PowersToPrepareGranted)
            .FirstAsync();
            return character;
        }
    }
}