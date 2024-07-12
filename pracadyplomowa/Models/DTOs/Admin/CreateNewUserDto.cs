using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa;

public class CreateNewUserDto
{
    [Required]
    public string Username { get; set; }
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string Role { get; set; }
}
