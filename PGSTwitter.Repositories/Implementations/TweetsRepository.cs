namespace PGSTwitter.Repositories.Implementations
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Interfaces;

    public class TweetsRepository : ITweetsRepository
    {
        private readonly ApplicationDbContext _context;

        public TweetsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tweet>> GetTweets()
        {
            var tweets = await _context.Tweets
                .Where(x => x.IsDeleted == false)
                .Include(t => t.User)
                .ToListAsync();
            return tweets;
        }

        public async Task<Tweet> GetTweet(int tweetID)
        {
            var tweet = await _context.Tweets
                .Where(x => x.ID == tweetID && x.IsDeleted == false)
                .Include(t => t.User)
                .SingleOrDefaultAsync();
            return tweet;
        }
        
        public async Task DeleteTweet(int tweetID)
        {
            var tweet = await _context.Tweets.FindAsync(tweetID);
            if (tweet == null || tweet.IsDeleted == true) return;
            tweet.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateTweet(int tweetID, Tweet newTweet)
        {
            var tweet = await _context.Tweets.FindAsync(tweetID);
            if (tweet == null || tweet.IsDeleted == true) return;
            tweet.TweetContent = newTweet.TweetContent;
            tweet.LastTimeOfEdit = newTweet.LastTimeOfEdit;
            await _context.SaveChangesAsync();
        }
        
        public async Task<int> CreateTweet(Tweet tweet)
        {
            _context.Tweets.Add(tweet);
            await _context.SaveChangesAsync();
            return tweet.ID;
        }

        public bool TweetExist(int tweetID)
        {
            var exist = _context.Tweets.Any(x => x.ID == tweetID && x.IsDeleted == false);
            return exist;
        }
    }
}
