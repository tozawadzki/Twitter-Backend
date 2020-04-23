namespace PGSTwitter.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TweetModels;

    public enum Status
    {
        Success,
        TweetNotFound,
        UserNotMatched
    }

    public class TweetsServiceResult
    {
        public Status Status { get; set; }

        public TweetsServiceResult(Status status)
        {
            Status = status;
        }

        public static implicit operator TweetsServiceResult(Status status) =>
            new TweetsServiceResult(status);
    }

    public class TweetsServiceResult<T>
        : TweetsServiceResult where T : class
    {
        public T Object { get; set; }

        public TweetsServiceResult(Status status, T obj)
            : base(status)
        {
            Object = obj;
        }

        public static implicit operator TweetsServiceResult<T>(ValueTuple<Status, T> t) =>
            new TweetsServiceResult<T>(t.Item1, t.Item2);
        public static implicit operator TweetsServiceResult<T>(Status status) =>
            new TweetsServiceResult<T>(status, null);
    }

    public interface ITweetsService
    {
        Task<TweetsServiceResult<TweetDTO>> GetTweet(int id);
        Task<TweetsServiceResult<IEnumerable<TweetDTO>>> GetTweets();
        Task<TweetsServiceResult<TweetDTO>> CreateTweet(NewTweetDTO newTweetDto, string userId);
        Task<TweetsServiceResult> UpdateTweet(int id, NewTweetDTO updatedTweetDto, string userId);
        Task<TweetsServiceResult> DeleteTweet(int id, string userId);
    }
}
