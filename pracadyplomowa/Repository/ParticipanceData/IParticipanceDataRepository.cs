using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository;

public interface IParticipanceDataRepository : IBaseRepository<ParticipanceData>
{
    void RemoveByCharacterId(int characterId);
    Task<ParticipanceData?> GetByCharacterIdAndEncounterIdWithCharacter(int characterId, int encounterId);
}