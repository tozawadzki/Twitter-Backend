namespace PGSTwitter.Services.Interfaces
{
    using System.Threading.Tasks;
    using Models;

    public interface ITokenService
    {
        Task<string> GetToken(UserLoginRequest loginDataDto);
    }
}
