namespace PGSTwitter.Repositories.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tweet
    {
        [Key]
        public int ID { set; get; }
        public string UserID { set; get; }
        [ForeignKey("UserID")]
        public virtual TwitterUser User { set; get; }
        public DateTime CreationDate { set; get; }
        public DateTime LastTimeOfEdit { set; get; }
        public string TweetContent { set; get; }
        public bool IsDeleted { set; get; }
    }
}
