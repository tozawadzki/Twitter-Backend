namespace PGSTwitter.Repositories.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class TwitterUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Tweet> Tweets { set; get; }
    }
}
