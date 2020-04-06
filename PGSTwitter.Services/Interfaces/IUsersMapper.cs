namespace PGSTwitter.Services.Interfaces
{
    using Repositories.Models;
    using UserModels;

    public interface IUsersMapper
    {
        UserDTO UserToDto(TwitterUser user);
        TwitterUser DtoToUser(UserDTO userDto);
        TwitterUser DtoToUser(NewUserDTO newUserDto);
    }
}
