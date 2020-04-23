namespace PGSTwitter.Services.TweetModels
{
    using System.ComponentModel.DataAnnotations;

    public class NewTweetDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
