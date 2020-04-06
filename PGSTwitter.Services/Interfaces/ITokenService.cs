namespace PGSTwitter.Services.Interfaces
{
    using System.Threading.Tasks;
    using UserModels;

    public interface ITokenService
    {
        Task<string> GetToken(LoginDataDTO loginDataDto);
    }
}
