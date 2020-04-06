namespace PGSTwitter.Repositories.Models
{
    using Microsoft.AspNetCore.Identity;

    public class TwitterUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
