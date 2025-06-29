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

        public async Task<PagedList<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId, bool isNpc, CharacterParams characterParams)
        {
            var query = _context.Characters
                    .Where(c => c.R_OwnerId == OwnerId)
                    .Where(c => c.IsNpc == isNpc)
                    .Include(c => c.R_CharacterBelongsToRace)
                    .Include(c => c.R_CharacterHasLevelsInClass)
                    .ThenInclude(cl => cl.R_Class)
                    .Include(c => c.R_Campaign)
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
                c.IsNpc,
                c.Name,
                c.Description,
                c.R_CharacterBelongsToRace.Name,
                c.R_CharacterHasLevelsInClass.First().R_Class.Name,
                c.R_CampaignId,
                c.ExperiencePoints
            ));

            return await PagedList<CharacterSummaryDto>.CreateAsync(charactersSumaries, characterParams.PageNumber, characterParams.PageSize);
        }
        
        public Task<List<Character>> GetCharactersByUserId(int userId)
        {
            return _context.Characters
                .Where(c => c.R_OwnerId == userId)
                .Include(c => c.R_Campaign)
                .ToListAsync();
        }

        public Task<Character> GetByIdWithAll(int Id)
        {

            var characterLevel = _context.Characters
            .Where(c => c.Id == Id).Include(c => c.R_CharacterHasLevelsInClass).Count();

            var character = _context.Characters
            .Where(c => c.Id == Id)

            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_EquipmentSlots)

            .Include(c => c.R_CharacterHasLevelsInClass).ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CharacterHasLevelsInClass).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterHasLevelsInClass).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterHasLevelsInClass).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_CharacterHasLevelsInClass).ThenInclude(cl => cl.HitDie)

            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ChoiceGroup).ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ChoiceGroup).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ChoiceGroup).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_EffectsGranted).ThenInclude(e => e.R_TargetedCharacter)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_EffectsGranted).ThenInclude(e => e.R_TargetedItem)
                
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailableGranted).ThenInclude(cg => cg.R_AlwaysAvailableThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailableGranted).ThenInclude(cg => cg.R_ToPrepareThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailableGranted).ThenInclude(p => p.R_ItemsGrantingPower.Where(i => i.R_EquipData != null && i.R_EquipData.R_CharacterId == Id))

            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepareGranted).ThenInclude(cg => cg.R_ToPrepareThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepareGranted).ThenInclude(cg => cg.R_AlwaysAvailableThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepareGranted).ThenInclude(p => p.R_ItemsGrantingPower.Where(i => i.R_EquipData != null && i.R_EquipData.R_CharacterId == Id))

            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ResourcesGranted).ThenInclude(cg => cg.R_Item)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ResourcesGranted).ThenInclude(cg => cg.R_Blueprint)

            .Include(c => c.R_PowersKnown).ThenInclude(p => p.R_EffectBlueprints) // is this needed?
            .Include(c => c.R_PowersKnown).ThenInclude(p => p.R_UsesImmaterialResource) // is this needed?
            .Include(c => c.R_PowersKnown).ThenInclude(p => p.R_AlwaysAvailableThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_PowersKnown).ThenInclude(p => p.R_ToPrepareThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_PowersKnown).ThenInclude(p => p.R_ItemsGrantingPower.Where(i => i.R_EquipData != null && i.R_EquipData.R_CharacterId == Id))

            .Include(c => c.R_PowersPrepared).ThenInclude(ps => ps.R_PreparedPowers).ThenInclude(p => p.R_EffectBlueprints)
            .Include(c => c.R_PowersPrepared).ThenInclude(ps => ps.R_PreparedPowers).ThenInclude(p => p.R_UsesImmaterialResource)
            .Include(c => c.R_PowersPrepared).ThenInclude(ps => ps.R_PreparedPowers).ThenInclude(p => p.R_AlwaysAvailableThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_PowersPrepared).ThenInclude(ps => ps.R_PreparedPowers).ThenInclude(p => p.R_ToPrepareThroughChoiceGroupUsage.Where(cgu => cgu.R_CharacterId == Id))
            .Include(c => c.R_PowersPrepared).ThenInclude(ps => ps.R_PreparedPowers).ThenInclude(p => p.R_ItemsGrantingPower.Where(i => i.R_EquipData != null && i.R_EquipData.R_CharacterId == Id))

            .Include(c => c.R_AffectedBy).ThenInclude(eg => eg.R_OwnedByGroup)
            .Include(c => c.R_AffectedBy).ThenInclude(eg => eg.R_GrantedThrough)
            .Include(c => c.R_AffectedBy).ThenInclude(eg => eg.R_GrantedByEquippingItem)
            .Include(c => c.R_AffectedBy).ThenInclude(eg => eg.R_TargetedItem)

            .Include(c => c.R_CharacterHasBackpack).ThenInclude(b => b.R_BackpackHasItems).ThenInclude(b => b.R_EffectsOnEquip)
            .Include(c => c.R_CharacterHasBackpack).ThenInclude(b => b.R_BackpackHasItems).ThenInclude(b => b.R_EquipItemGrantsAccessToPower)
            .Include(c => c.R_CharacterHasBackpack).ThenInclude(b => b.R_BackpackHasItems).ThenInclude(b => b.R_ItemIsEquippableInSlots)
            .Include(c => c.R_CharacterHasBackpack).ThenInclude(b => b.R_BackpackHasItems).ThenInclude(b => b.R_EquipData).ThenInclude(b => b.R_Slots)

            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_EffectsOnEquip).ThenInclude(eg => eg.R_OwnedByGroup)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_EffectsOnEquip).ThenInclude(e => e.R_GrantedThrough)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_EffectsOnEquip).ThenInclude(e => e.R_TargetedCharacter)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_EffectsOnEquip).ThenInclude(e => e.R_TargetedItem)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_EquipItemGrantsAccessToPower)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_ItemIsEquippableInSlots)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_ItemGrantsResources).ThenInclude(b => b.R_Blueprint)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_AffectedBy).ThenInclude(eg => eg.R_OwnedByGroup)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_AffectedBy).ThenInclude(e => e.R_GrantedThrough)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_AffectedBy).ThenInclude(e => e.R_GrantedByEquippingItem)
            .Include(c => c.R_EquippedItems).ThenInclude(ed => ed.R_Item).ThenInclude(b => b.R_AffectedBy).ThenInclude(e => e.R_TargetedCharacter)

            .Include(c => c.R_ImmaterialResourceInstances).ThenInclude(iri => iri.R_Blueprint)
            .Include(c => c.R_ImmaterialResourceInstances).ThenInclude(iri => iri.R_Item)
            .Include(c => c.R_ImmaterialResourceInstances).ThenInclude(iri => iri.R_ChoiceGroupUsage)
            
            .Include(c => c.R_ConcentratesOn)
            .Include(c => c.UsedHitDice)

            .AsSplitQuery() // IMPORTANT !!!!! https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithChoiceGroups(int Id)
        {
            var characterForLevels = _context.Characters
            .Where(c => c.Id == Id).Include(c => c.R_CharacterHasLevelsInClass).First();
            var characterLevel = characterForLevels.R_CharacterHasLevelsInClass.Count();

            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_AffectedBy)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel))
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel))
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel))
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel))
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_Resources)
                            .ThenInclude(r => r.R_Blueprint)

            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel))
                .ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel))
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel))
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel))
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel))
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_Resources)
                        .ThenInclude(r => r.R_Blueprint)

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
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_Resources)
                        .ThenInclude(r => r.R_Blueprint)

            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_EffectsGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_PowersAlwaysAvailableGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_PowersToPrepareGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ResourcesGranted)
                    .ThenInclude(r => r.R_Blueprint)

            .AsSplitQuery() // IMPORTANT !!!!! https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdForChoice(int Id){
            var characterForLevels = _context.Characters
            .Where(c => c.Id == Id).Include(c => c.R_CharacterHasLevelsInClass).First();
            var characterLevel = characterForLevels.R_CharacterHasLevelsInClass.Count();

            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_AffectedBy)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_Resources).ThenInclude(r => r.R_Blueprint)
            .Include(c => c.R_CharacterBelongsToRace).ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel)).ThenInclude(rl => rl.R_ChoiceGroups).ThenInclude(cg => cg.R_UsageInstances)

            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel)).ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel)).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel)).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel)).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel)).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_Resources).ThenInclude(r => r.R_Blueprint)
            .Include(c => c.R_CharacterHasLevelsInClass.Where(rl => rl.Level <= characterLevel)).ThenInclude(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_UsageInstances)

            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ChoiceGroup).ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ChoiceGroup).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ChoiceGroup).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ChoiceGroup).ThenInclude(cg => cg.R_Resources).ThenInclude(r => r.R_Blueprint)

            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_EffectsGranted)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailableGranted)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepareGranted)
            .Include(c => c.R_UsedChoiceGroups).ThenInclude(cg => cg.R_ResourcesGranted).ThenInclude(r => r.R_Blueprint)

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

        public Task<Character> GetCharacterEquipment(int id)
        {
            var character = _context.Characters
            .Where(c => c.Id == id)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetCharacterWithCoinSack(int id)
        {
            var character = _context.Characters
            .Where(c => c.Id == id)
            .Include(c => c.R_CharacterHasBackpack)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetCharacterEquipmentAndSlots(int id)
        {
            var character = _context.Characters
            .Where(c => c.Id == id)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_ItemIsEquippableInSlots)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_AffectedBy)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_EffectsOnEquip)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_EquipData)
            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)
                    .ThenInclude(i => i.R_ItemInItemsFamily)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_EquipmentSlots)
            .Include(c => c.R_EquippedItems)
                .ThenInclude(r => r.R_Item)
                    .ThenInclude(r => r.R_EffectsOnEquip)
                        .ThenInclude(r => r.R_TargetedCharacter)
            .Include(c => c.R_EquippedItems)
                .ThenInclude(r => r.R_Item)
                    .ThenInclude(r => r.R_EffectsOnEquip)
                        .ThenInclude(r => r.R_TargetedItem)
            .Include(c => c.R_AffectedBy)
            .Include(c => c.R_EquippedItems)
                .ThenInclude(r => r.R_Slots)
            .AsSplitQuery()
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithCustomResources(int Id)
        {
            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_ImmaterialResourceInstances)
                .ThenInclude(iri => iri.R_Blueprint)
            .Include(c => c.R_ImmaterialResourceInstances)
                .ThenInclude(iri => iri.R_ChoiceGroupUsage)
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
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(c => c.R_Class)
                    .ThenInclude(c => c.MaximumPreparedSpellsFormula)
            .Include(c => c.R_AffectedBy)
            .Include(c => c.R_EquippedItems)
                .ThenInclude(c => c.R_Item)
                    .ThenInclude(c => c.R_EffectsOnEquip)
            .Include(c => c.R_PowersPrepared)
                .ThenInclude(ps => ps.R_PreparedPowers)
            .FirstAsync();
            return character;
        }

        public Task<Character> GetByIdWithPowersToPrepare(int Id)
        {
            var character = _context.Characters
            .Where(c => c.Id == Id)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(c => c.R_Class)
                    .ThenInclude(c => c.MaximumPreparedSpellsFormula)
            // .Include(c => c.R_CharacterHasLevelsInClass)
            //     .ThenInclude(c => c.R_Class)
            //         .ThenInclude(c => c.R_ClassLevels.Where(x => x.R_Characters.Select(ch => ch.Id == Id).Any()))
            .Include(c => c.R_AffectedBy)
            .Include(c => c.R_EquippedItems)
                .ThenInclude(c => c.R_Item)
                    .ThenInclude(c => c.R_EffectsOnEquip)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(c => c.R_PowersToPrepareGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(c => c.R_ChoiceGroup)
                    .ThenInclude(c => c.R_GrantedByClassLevel)
                        .ThenInclude(c => c.R_Class)
            .FirstAsync();
            return character;
        }

        public Dictionary<int, Character> GetCharactersForAccessAnalysis(List<int> ids)
        {
            return _context.Characters
            .Where(i => ids.Contains(i.Id))
            .Include(c => c.R_Campaign)
                .ThenInclude(c => c!.R_CampaignHasCharacters)
            .Include(c => c.R_CharactersParticipatesInEncounters)
                .ThenInclude(c => c.R_Encounter)
                    .ThenInclude(c => c.R_Participances)
                        .ThenInclude(c => c.R_Character)
            .AsSplitQuery()
            .ToDictionary(i => i.Id, i => i);
        }
    }
}