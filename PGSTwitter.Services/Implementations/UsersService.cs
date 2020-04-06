namespace PGSTwitter.Services.Implementations
{
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Repositories.Models;
    using UserModels;

    public class UsersService : IUsersService
    {
        private readonly UserManager<TwitterUser> _userManager;
        private readonly IUsersMapper _mapper;

        public UsersService(UserManager<TwitterUser> userManager, IUsersMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDTO> AuthenticateUser(LoginDataDTO loginDataDto)
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

            var userDto = _mapper.UserToDto(user);
            return userDto;
        }

        public async Task<IdentityResult> CreateUser(NewUserDTO newUserDto)
        {
            var user = _mapper.DtoToUser(newUserDto);
            var result = await _userManager.CreateAsync(user, newUserDto.Password);
            return result;
        }

        public async Task<UserDTO> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDto = _mapper.UserToDto(user);
            return userDto;
        }

        public async Task<UserDTO> FindUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userDto = _mapper.UserToDto(user);
            return userDto;
        }
    }
}
