namespace PGSTwitter.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserInfoResponse
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
