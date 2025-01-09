using AutoMapper;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Class;
using pracadyplomowa.Repository.Encounter;
using pracadyplomowa.Repository.Field;
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.Race;

namespace pracadyplomowa.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public UnitOfWork(AppDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public IBoardRepository BoardRepository => new BoardRepository(_context, _mapper);
    public ICampaignRepository CampaignRepository => new CampaignRepository(_context);
    public ICharacterRepository CharacterRepository => new CharacterRepository(_context);
    public IEncounterRepository EncounterRepository => new EncounterRepository(_context);
    public IFieldRepository FieldRepository => new FieldRepository(_context);
    public IParticipanceDataRepository ParticipanceDataRepository => new ParticipanceDataRepository(_context);
    public IClassRepository ClassRepository => new ClassRepository(_context);
    public IEffectBlueprintRepository EffectBlueprintRepository => new EffectBlueprintRepository(_context);
    public IEffectGroupRepository EffectGroupRepository => new EffectGroupRepository(_context);
    public IEffectInstanceRepository EffectInstanceRepository => new EffectInstanceRepository(_context);
    public IEquipmentSlotRepository EquipmentSlotRepository => new EquipmentSlotRepository(_context);
    public IImmaterialResourceBlueprintRepository ImmaterialResourceBlueprintRepository =>
        new ImmaterialResourceBlueprintRepository(_context);
    public IItemRepository ItemRepository => new ItemRepository(_context);
    public IItemCostRequirementRepository ItemCostRequirementRepository => new ItemCostRequirementRepository(_context);
    public IItemFamilyRepository ItemFamilyRepository => new ItemFamilyRepository(_context);
    public IPowerRepository PowerRepository => new PowerRepository(_context);
    public IRaceRepository RaceRepository => new RaceRepository(_context);
    public IShopRepository ShopRepository => new ShopRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}