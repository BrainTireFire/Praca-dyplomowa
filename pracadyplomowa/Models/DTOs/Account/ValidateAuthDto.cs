using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs.Account;

public class ValidateAuthDto
{
    [Required]
    public string Username { get; set; }
    public string Email { get; set; }
    [Required]
    public Boolean IsAuthenticated { get; set; }
    public IList<string> Roles { get; set; }
}