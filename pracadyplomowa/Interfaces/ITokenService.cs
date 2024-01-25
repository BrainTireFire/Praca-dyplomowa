namespace pracadyplomowa;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}
