using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : Controller
    {
        private readonly IFollower _follower;
        public FollowerController(IFollower follower)
        {
            _follower = follower;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Follower>))]
        public IActionResult GetFollowers()
        {
            var followers = _follower.GetFollowers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(followers);
        }
        [HttpGet("{followerId}")]
        [ProducesResponseType(200, Type = typeof(Follower))]
        [ProducesResponseType(400)]
        public IActionResult Getfollower(int followerId)
        {
            if (!_follower.FollowerExists(followerId))
                return NotFound();

            var follower = _follower.GetFollower(followerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(follower);
        }
        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatFollower([FromBody] Follower followerCreate)
        {
            if (followerCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_follower.CreateFollower(followerCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok(new {message = "Successfully created"});
        }
        [HttpDelete("delete/{followerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFollower(int followerId)
        {
            if (!_follower.FollowerExists(followerId))
            {
                return NotFound();
            }
            try
            {
                var followerToDelete = _follower.GetFollower(followerId);
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_follower.RemoveFollower(followerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting follower");
            }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., return a 404 status in an API
                Console.WriteLine(ex.Message);
            }
          

            return Ok(new {message = "Deleted Successfully"});
        }
        [HttpGet("isFollowing/{followerUserId}/{targetUserId}")]
        public async Task<IActionResult> IsFollowing(int followerUserId, int targetUserId)
        {
            var isFollowing = await _follower.IsFollowing(followerUserId, targetUserId);  
            return Ok(isFollowing);
        }
    }
}
