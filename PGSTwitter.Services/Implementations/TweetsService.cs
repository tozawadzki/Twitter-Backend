namespace PGSTwitter.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces;
    using Repositories.Interfaces;
    using Repositories.Models;
    using TweetModels;

    public class TweetsService : ITweetsService
    {
        private readonly ITweetsRepository _tweetsRepository;
        private readonly IMapper _mapper;

        public TweetsService(ITweetsRepository tweetsRepository, IMapper mapper)
        {
            _tweetsRepository = tweetsRepository;
            _mapper = mapper;
        }

        public async Task<TweetsServiceResult<TweetDTO>> CreateTweet(NewTweetDTO newTweetDto,
            string userId)
        {
            var tweet = _mapper.Map<NewTweetDTO, Tweet>(newTweetDto);
            tweet.UserID = userId;
            tweet.CreationDate = DateTime.UtcNow;
            tweet.LastTimeOfEdit = tweet.CreationDate;

            await _tweetsRepository.CreateTweet(tweet);

            var tweetDto = _mapper.Map<Tweet, TweetDTO>(tweet);
            return (Status.Success, tweetDto);
        }

        public async Task<TweetsServiceResult<TweetDTO>> GetTweet(int id)
        {
            var tweet = await _tweetsRepository.GetTweet(id);

            if (tweet == null)
            {
                return Status.TweetNotFound;
            }

            var tweetDto = _mapper.Map<Tweet, TweetDTO>(tweet);
            return (Status.Success, tweetDto);
        }

        public async Task<TweetsServiceResult<IEnumerable<TweetDTO>>> GetTweets()
        {
            var tweets = await _tweetsRepository.GetTweets();
            var tweetsDtos = tweets
                .Select(t => _mapper.Map<Tweet, TweetDTO>(t));

            return (Status.Success, tweetsDtos);
        }

        public async Task<TweetsServiceResult> UpdateTweet(int id, NewTweetDTO updatedTweetDto,
            string userId)
        {
            var tweet = await _tweetsRepository.GetTweet(id);

            if (tweet == null)
            {
                return Status.TweetNotFound;
            }

            if (!tweet.UserID.Equals(userId))
            {
                return Status.UserNotMatched;
            }

            tweet.TweetContent = updatedTweetDto.Content;
            tweet.LastTimeOfEdit = DateTime.UtcNow;
            await _tweetsRepository.UpdateTweet(tweet.ID, tweet);

            return Status.Success;
        }

        public async Task<TweetsServiceResult> DeleteTweet(int id, string userId)
        {
            var tweet = await _tweetsRepository.GetTweet(id);

            if (tweet == null)
            {
                return Status.TweetNotFound;
            }

            if (!tweet.UserID.Equals(userId))
            {
                return Status.UserNotMatched;
            }

            await _tweetsRepository.DeleteTweet(id);
            return Status.Success;
        }
    }
}
