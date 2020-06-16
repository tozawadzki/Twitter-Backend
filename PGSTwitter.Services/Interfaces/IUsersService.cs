namespace PGSTwitter.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public interface IUsersService
    {
        Task<UserInfoResponse> AuthenticateUser(UserLoginRequest loginDataDto);
        Task<IdentityResult> CreateUser(UserCreateRequest newUserDto);
        Task<UserInfoResponse> FindUserByEmail(string email);
        Task<UserInfoResponse> FindUserById(string id);
    }
}
