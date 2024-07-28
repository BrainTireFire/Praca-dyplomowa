using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 8)]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
}
