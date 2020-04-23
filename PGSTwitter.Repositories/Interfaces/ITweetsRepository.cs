namespace PGSTwitter.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITweetsRepository
    {
        Task<IEnumerable<Tweet>> GetTweets();
        Task<Tweet> GetTweet(int tweetID);
        Task DeleteTweet(int tweetID);
        Task UpdateTweet(int tweetID, Tweet newTweet);
        Task<int> CreateTweet(Tweet tweet);
        bool TweetExist(int tweetID);   
    }
}
