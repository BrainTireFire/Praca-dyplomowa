using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Services
{
    public class CharacterService(IUnitOfWork unitOfWork) : ICharacterService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public bool CheckExistenceAndReadEditAccess(int characterId, int userId, List<Character.AccessLevels> requiredAccessLevels, out ActionResult errorResult, out List<Character.AccessLevels> grantedAccessLevels, out Character? characterToAnalyze){
            if(!_unitOfWork.CharacterRepository.GetCharactersForAccessAnalysis([characterId]).TryGetValue(characterId, out characterToAnalyze)){ 
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

        public bool CheckExistenceAndReadEditAccess(int characterId, int userId, List<Character.AccessLevels> requiredAccessLevels, out ActionResult errorResult, out List<Character.AccessLevels> grantedAccessLevels){
            return CheckExistenceAndReadEditAccess(characterId, userId, requiredAccessLevels, out errorResult, out grantedAccessLevels, out var character);
        }
    }
}