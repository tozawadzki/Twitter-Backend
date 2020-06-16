namespace PGSTwitter.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TweetRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
