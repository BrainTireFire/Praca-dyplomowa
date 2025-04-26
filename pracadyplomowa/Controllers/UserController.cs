using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs.User;
using pracadyplomowa.Services.User;

namespace pracadyplomowa.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpPut("edit/email")]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateUserDto updateUserDto)
    {
        var userId = User.GetUserId();

        if (updateUserDto.NewEmail == null)
        {
            return BadRequest(new ApiResponse(400, "New email cannot be null."));  
        }
        
        var result = await _userService.UpdateUserEmailAsync(userId, updateUserDto.NewEmail);
        
        return result ?? NoContent();
    }

    [Authorize]
    [HttpPut("edit/username")]
    public async Task<IActionResult> UpdateUsername([FromBody] UpdateUserDto updateUserDto)
    {
        var userId = User.GetUserId();
        
        if (updateUserDto.NewUsername == null)
        {
            return BadRequest(new ApiResponse(400, "New username cannot be null."));  
        }
        
        var result = await _userService.UpdateUsernameAsync(userId, updateUserDto.NewUsername);
        return result ?? NoContent();
    }

    [Authorize]
    [HttpPut("edit/password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserDto updateUserDto)
    {
        var userId = User.GetUserId();
        
        if (updateUserDto.NewPassword == null)
        {
            return BadRequest(new ApiResponse(400, "New password cannot be null."));  
        }
        
        var result = await _userService.UpdatePasswordAsync(userId, updateUserDto.NewPassword);
        return result ?? NoContent();
    }
    
    [Authorize]
    [HttpDelete("edit/delete")]
    public async Task<ActionResult> DeleteUser()
    {
        var userId = User.GetUserId();
        var result = await _userService.DeleteUserAsync(userId);
        return result ?? NoContent();
    }
  
}