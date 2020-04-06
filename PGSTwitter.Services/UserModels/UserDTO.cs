namespace PGSTwitter.Services.UserModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
