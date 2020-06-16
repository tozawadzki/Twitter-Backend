namespace PGSTwitter.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Interfaces;
    using Services.Models;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TweetsController : ControllerBase
    {
        private readonly ITweetsService _tweetsService;

        public TweetsController(ITweetsService tweetsService)
        {
            _tweetsService = tweetsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTweets()
        {
            var tweets = await _tweetsService.GetTweets();
            return Ok(tweets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTweet(int id)
        {
            var tweet = await _tweetsService.GetTweet(id);
            if (tweet == null)
            {
                return NotFound();
            }

            return Ok(tweet);
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateTweet(TweetRequest tweetRequest)
        {
            var createdTweet = await _tweetsService
                .CreateTweet(tweetRequest, User.GetId());

            return CreatedAtAction(nameof(GetTweet),
                new { id = createdTweet.Id },
                createdTweet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTweet(int id, TweetRequest updatedTweetRequest)
        {
            var actionResult = await _tweetsService.UpdateTweet(id, updatedTweetRequest, User.GetId());

            return actionResult switch
            {
                ServiceStatus.Success => NoContent(),
                ServiceStatus.NotFound => BadRequest(),
                ServiceStatus.UnauthorizedAction => Unauthorized(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            var actionResult = await _tweetsService.DeleteTweet(id, User.GetId());

            return actionResult switch
            {
                ServiceStatus.Success => NoContent(),
                ServiceStatus.NotFound => BadRequest(),
                ServiceStatus.UnauthorizedAction => Unauthorized(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
