namespace pracadyplomowa.Models.DTOs.Account;

public class ValidateAuthDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public Boolean IsAuthenticated { get; set; }
    public IList<string> Roles { get; set; }
}