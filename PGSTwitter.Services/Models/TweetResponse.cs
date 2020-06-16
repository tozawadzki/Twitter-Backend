namespace PGSTwitter.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TweetResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastEditDate { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
