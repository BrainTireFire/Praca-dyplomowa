using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Encounter;

namespace pracadyplomowa.Services.Encounter;

public class EncounterService : IEncounterService
{
    private readonly IEncounterRepository _encounterRepository;
    private readonly IBoardRepository _boardRepository;
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    
    public EncounterService(
        IEncounterRepository encounterRepository, 
        IBoardRepository boardRepository, 
        ICampaignRepository campaignRepository,
        ICharacterRepository characterRepository,
        IAccountRepository accountRepository,
        IMapper mapper
   )
    {
        _encounterRepository = encounterRepository;
        _boardRepository = boardRepository;
        _campaignRepository = campaignRepository;
        _characterRepository = characterRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedList<EncounterShortDto>> GetEncountersAsync(int ownedId, EncounterParams encounterParams)
    {
        var encounters = await _encounterRepository.GetEncounters(ownedId, encounterParams);
        var encounterSummary = _mapper.MapPagedList<Models.Entities.Campaign.Encounter, EncounterShortDto>(encounters);
        return encounterSummary;
    }

    public async Task<EncounterSummaryDto> GetEncounterAsync(int encounterId)
    {
        var encounter = await _encounterRepository.GetEncounterSummary(encounterId);
        var encounterSummary = _mapper.Map<EncounterSummaryDto>(encounter);
        return encounterSummary;
    }

    public async Task<ActionResult> CreateEncounterAsync(int ownerId, CreateEncounterDto createEncounterDto)
    {
        // using var transactionScope = new TransactionScope(
        //     TransactionScopeAsyncFlowOption.Enabled);
        
        try
        {
            var board = _boardRepository.GetById(createEncounterDto.BoardId);
            if (board == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, "Board with Id " + createEncounterDto.BoardId + " does not exist"));
            }
        
            var campaign = _campaignRepository.GetById(createEncounterDto.CampaignId);
            if (campaign == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, $"Campaign with Id {createEncounterDto.CampaignId} does not exist"));
            }
            
            var user = await _accountRepository.GetUserById(ownerId);
            if (user == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, $"User with Id {ownerId} does not exist"));
            }
        
            var characters = new List<Character>();
        
            foreach (var characterId in createEncounterDto.CharactersIds)
            {
                var character = _characterRepository.GetById(characterId);
                if (character == null)
                {
                    return new NotFoundObjectResult(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
                }
                characters.Add(character);
            }
            
            var encounter = new Models.Entities.Campaign.Encounter
            {
                R_Owner = user,
                Name = createEncounterDto.Name,
                R_Campaign = campaign,
                R_Board = board
            };
            
            foreach (var character in characters)
            {
                encounter.AddParticipance(character);
            }
            
            _encounterRepository.Add(encounter);
            await _encounterRepository.SaveChanges();
            
            // transactionScope.Complete();

            return new CreatedResult(string.Empty, null);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(new ApiResponse(400, e.Message));
        }
    }
}