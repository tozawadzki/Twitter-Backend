namespace PGSTwitter.Services.Implementations
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using Repositories.Models;

    public class UsersService : IUsersService
    {
        private readonly UserManager<TwitterUser> _userManager;
        private readonly IMapper _mapper;

        public UsersService(UserManager<TwitterUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserInfoResponse> AuthenticateUser(UserLoginRequest loginDataDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDataDto.Email);
            if (user == null)
            {
                return null;
            }

            bool passwordCorrect = await _userManager.CheckPasswordAsync(user, loginDataDto.Password);
            if (!passwordCorrect)
            {
                return null;
            }

            var userDto = _mapper.Map<TwitterUser, UserInfoResponse>(user);
            return userDto;
        }

        public async Task<IdentityResult> CreateUser(UserCreateRequest newUserDto)
        {
            var user = _mapper.Map<UserCreateRequest, TwitterUser>(newUserDto);
            var result = await _userManager.CreateAsync(user, newUserDto.Password);
            return result;
        }

        public async Task<UserInfoResponse> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDto = _mapper.Map<TwitterUser, UserInfoResponse>(user);
            return userDto;
        }

        public async Task<UserInfoResponse> FindUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userDto = _mapper.Map<TwitterUser, UserInfoResponse>(user);
            return userDto;
        }
    }
}
