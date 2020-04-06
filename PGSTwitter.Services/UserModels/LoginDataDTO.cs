namespace PGSTwitter.Services.UserModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDataDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
