namespace pracadyplomowa;

public class LoginResult
{
    public User User { get; set; }
    public string ErrorMessage { get; set; }
    public bool Succeeded => User != null && string.IsNullOrEmpty(ErrorMessage);
}
