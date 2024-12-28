using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository;

namespace pracadyplomowa.Services
{
    public class CharacterService(ICharacterRepository characterRepository) : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository = characterRepository;

        public bool CheckExistenceAndReadEditAccess(int characterId, int userId, List<Character.AccessLevels> requiredAccessLevels, out ActionResult errorResult, out List<Character.AccessLevels> grantedAccessLevels){
            if(!_characterRepository.GetCharactersForAccessAnalysis([characterId]).TryGetValue(characterId, out var characterToAnalyze)){ 
                grantedAccessLevels = [];
                errorResult = new NotFoundObjectResult("Character with specified Id was not found");
                return false;
            }
            if(!characterToAnalyze.HasAccess(userId, out grantedAccessLevels) || requiredAccessLevels.Intersect(grantedAccessLevels).Count() != requiredAccessLevels.Count){
                errorResult = new UnauthorizedObjectResult("Insufficient privileges to execute this operation");
                return false;
            }
            else{
                errorResult = new OkObjectResult("Operation allowed");
                return true;
            }
        }
    }
}