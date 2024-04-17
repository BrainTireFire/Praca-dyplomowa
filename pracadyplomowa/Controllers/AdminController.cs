using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Authorization.AuthorizationAttributes;
using pracadyplomowa.Models.DTOs;

namespace pracadyplomowa;

public class AdminController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    public AdminController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        var users = await _userManager.Users
            .Include(r => r.UserRoles)
            .ThenInclude(r => r.Role)
            .OrderBy(u => u.UserName)
            .Select(u => new
            {
                u.Id,
                Username = u.UserName,
                Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            })
            .ToListAsync();

        return Ok(users);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [Ownership("Id")]
    [HttpPost("users-with-roles")]
    public ActionResult TestPost([FromBody] ObjectDTO test) //this method was prepared to test custom policy provider
    {
        Console.WriteLine($"Test Post {test.Id}");
        return Ok();
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
    {
        var selectedRoles = roles.Split(",").ToArray();

        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return NotFound("User not found...");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

        if (!result.Succeeded)
        {
            return BadRequest("Failed while adding the role...");
        }

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

        if (!result.Succeeded)
        {
            return BadRequest("Failed while removing the role...");
        }

        return Ok(await _userManager.GetRolesAsync(user));
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("delete-user/{username}")]
    public async Task<ActionResult> DeleteUserByUsername(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound("User not found...");
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest("Failed while deleting the user...");
        }

        return Ok();
    }


    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("create-user")]
    public async Task<ActionResult> CreateNewUser(CreateNewUserDto createNewUserDto)
    {
        var user = new User
        {
            UserName = createNewUserDto.Username.ToLower(),
            Email = createNewUserDto.Email
        };

        if (await _userManager.Users.AnyAsync(u => u.UserName == user.UserName))
        {
            return BadRequest("Username already exists...");
        }

        var result = await _userManager.CreateAsync(user, createNewUserDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest("Failed while creating the user...");
        }

        if (!string.IsNullOrEmpty(createNewUserDto.Role))
        {
            result = await _userManager.AddToRoleAsync(user, createNewUserDto.Role);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
        }

        return Ok();
    }


}
