namespace pracadyplomowa.Repository.User;

public interface IUserRepository : IBaseRepository<pracadyplomowa.User>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> UsernameExistsAsync(string email);
}