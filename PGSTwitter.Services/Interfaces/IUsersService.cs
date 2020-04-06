namespace PGSTwitter.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using UserModels;

    public interface IUsersService
    {
        Task<UserDTO> AuthenticateUser(LoginDataDTO loginDataDto);
        Task<IdentityResult> CreateUser(NewUserDTO newUserDto);
        Task<UserDTO> FindUserByEmail(string email);
        Task<UserDTO> FindUserById(string id);
    }
}
