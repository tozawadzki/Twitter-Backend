namespace PGSTwitter.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Services.TweetModels;

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
            var result = await _tweetsService.GetTweets();

            return result.Status switch
            {
                Status.Success => Ok(result.Object),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTweet(int id)
        {
            var result = await _tweetsService.GetTweet(id);

            return result.Status switch
            {
                Status.Success => Ok(result.Object),
                Status.TweetNotFound => NotFound(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateTweet(NewTweetDTO newTweetDto)
        {
            var result = await _tweetsService
                .CreateTweet(newTweetDto, User.GetId());

            return result.Status switch
            {
                Status.Success => CreatedAtAction(nameof(GetTweet),
                    new { id = result.Object.Id },
                    result.Object),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTweet(int id, NewTweetDTO updatedTweetDto)
        {
            var result = await _tweetsService.UpdateTweet(id, updatedTweetDto, User.GetId());

            return result.Status switch
            {
                Status.Success => Ok(),
                Status.TweetNotFound => BadRequest(),
                Status.UserNotMatched => Unauthorized(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            var result = await _tweetsService.DeleteTweet(id, User.GetId());

            return result.Status switch
            {
                Status.Success => Ok(),
                Status.TweetNotFound => BadRequest(),
                Status.UserNotMatched => Unauthorized(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
