namespace PGSTwitter.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces;
    using Models;
    using Repositories.Interfaces;
    using Repositories.Models;

    public class TweetsService : ITweetsService
    {
        private readonly ITweetsRepository _tweetsRepository;
        private readonly IMapper _mapper;

        public TweetsService(ITweetsRepository tweetsRepository, IMapper mapper)
        {
            _tweetsRepository = tweetsRepository;
            _mapper = mapper;
        }

        public async Task<TweetResponse> CreateTweet(
            TweetRequest tweetRequest, string userId)
        {
            var tweet = _mapper.Map<TweetRequest, Tweet>(tweetRequest);
            tweet.UserID = userId;
            tweet.CreationDate = DateTime.UtcNow;
            tweet.LastTimeOfEdit = tweet.CreationDate;

            await _tweetsRepository.CreateTweet(tweet);

            var tweetDto = _mapper.Map<Tweet, TweetResponse>(tweet);
            return tweetDto;
        }

        public async Task<TweetResponse> GetTweet(int id)
        {
            var tweet = await _tweetsRepository.GetTweet(id);

            if (tweet == null)
            {
                return null;
            }

            var tweetDto = _mapper.Map<Tweet, TweetResponse>(tweet);
            return tweetDto;
        }

        public async Task<IEnumerable<TweetResponse>> GetTweets()
        {
            var tweets = await _tweetsRepository.GetTweets();
            var tweetsDtos = tweets
                .Select(t => _mapper.Map<Tweet, TweetResponse>(t));

            return tweetsDtos;
        }

        public async Task<ServiceStatus> UpdateTweet(int id,
            TweetRequest updatedTweetRequest, string userId)
        {
            var tweet = await _tweetsRepository.GetTweet(id);

            if (tweet == null)
            {
                return ServiceStatus.NotFound;
            }

            if (!tweet.UserID.Equals(userId))
            {
                return ServiceStatus.UnauthorizedAction;
            }

            tweet.TweetContent = updatedTweetRequest.Content;
            tweet.LastTimeOfEdit = DateTime.UtcNow;
            await _tweetsRepository.UpdateTweet(tweet.ID, tweet);

            return ServiceStatus.Success;
        }

        public async Task<ServiceStatus> DeleteTweet(int id, string userId)
        {
            var tweet = await _tweetsRepository.GetTweet(id);

            if (tweet == null)
            {
                return ServiceStatus.NotFound;
            }

            if (!tweet.UserID.Equals(userId))
            {
                return ServiceStatus.UnauthorizedAction;
            }

            await _tweetsRepository.DeleteTweet(id);
            return ServiceStatus.Success;
        }
    }
}
