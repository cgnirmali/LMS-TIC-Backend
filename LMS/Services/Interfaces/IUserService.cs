namespace LMS.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> loginUser(string email, string password);

    }
}
