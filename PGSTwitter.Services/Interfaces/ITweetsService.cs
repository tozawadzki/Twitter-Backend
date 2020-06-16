namespace PGSTwitter.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITweetsService
    {
        Task<TweetResponse> GetTweet(int id);
        Task<IEnumerable<TweetResponse>> GetTweets();
        Task<TweetResponse> CreateTweet(TweetRequest tweetRequest, string userId);
        Task<ServiceStatus> UpdateTweet(int id, TweetRequest updatedTweetRequest, string userId);
        Task<ServiceStatus> DeleteTweet(int id, string userId);
    }
}
