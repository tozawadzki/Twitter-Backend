namespace PGSTwitter.Services.Implementations
{
    using Interfaces;
    using Repositories.Models;
    using UserModels;

    public class UsersMapper : IUsersMapper
    {
        public UserDTO UserToDto(TwitterUser user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public TwitterUser DtoToUser(UserDTO userDto)
        {
            return new TwitterUser()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };
        }

        public TwitterUser DtoToUser(NewUserDTO newUserDto)
        {
            return new TwitterUser()
            {
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Email = newUserDto.Email,
                UserName = newUserDto.Email
            };
        }
    }
}
