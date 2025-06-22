using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers;

[Authorize]
public class ActionLogController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public ActionLogController( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("{encounterId}")]
    public async Task<ActionResult<ActionLogDto[]>> GetActionLogsByEncounterId(int encounterId)
    {
        var actionLogs = await _unitOfWork.ActionLogRepository.GetByEncounterId(encounterId);

        if (actionLogs == null || actionLogs.Length == 0)
        {
            return NotFound($"No action logs found for encounter ID {encounterId}");
        }

        return Ok(actionLogs);
    }

}