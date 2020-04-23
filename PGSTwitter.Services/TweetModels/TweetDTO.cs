namespace PGSTwitter.Services.TweetModels
{
    using System;

    public class TweetDTO
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string Content { get; set; }
    }
}
