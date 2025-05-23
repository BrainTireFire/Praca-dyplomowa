using pracadyplomowa.Repository.AuctionLog;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Class;
using pracadyplomowa.Repository.Encounter;
using pracadyplomowa.Repository.Field;
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.Race;

namespace pracadyplomowa.Repository.UnitOfWork;

public interface IUnitOfWork
{
    IBoardRepository BoardRepository { get; }
    ICampaignRepository CampaignRepository { get; }
    ICharacterRepository CharacterRepository { get; }
    IEncounterRepository EncounterRepository { get; }
    IFieldRepository FieldRepository { get; }
    IParticipanceDataRepository ParticipanceDataRepository { get; }
    IClassRepository ClassRepository { get; }
    IEffectBlueprintRepository EffectBlueprintRepository { get; }
    IEffectGroupRepository EffectGroupRepository { get; }
    IEffectInstanceRepository EffectInstanceRepository { get; }
    IEquipmentSlotRepository EquipmentSlotRepository { get; }
    IImmaterialResourceBlueprintRepository ImmaterialResourceBlueprintRepository { get; }
    IItemRepository ItemRepository { get; }
    IItemCostRequirementRepository ItemCostRequirementRepository { get; }
    IItemFamilyRepository ItemFamilyRepository { get; }
    ILanguageRepository LanguageRepository { get; }
    IPowerRepository PowerRepository { get; }
    IRaceRepository RaceRepository { get; }
    IShopRepository ShopRepository { get; }
    IActionLogRepository ActionLogRepository { get; }
    Task<int> SaveChangesAsync();
    bool HasChanges();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}