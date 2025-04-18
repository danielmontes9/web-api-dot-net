namespace web_api_dot_net.Data.Repositories
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);
    }
}
