using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa;

public class LoginDto
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 8)]
    public string Password { get; set; }
}
