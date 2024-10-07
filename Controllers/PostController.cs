using Microsoft.AspNetCore.Mvc;
using SocialMedia.Entity.Views;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPosts _posts;
        //configure firebase
        //private static string apiKey = "AIzaSyBJ2Od3qj93pGl5yuaUq9TSFBGmIwLsUWQ";
        //private static string bucket = "social-media-69270.appspot.com";

        public PostController(IPosts posts) { 
            _posts = posts;
        }
        [HttpGet]
        public IActionResult GetPosts() {
            var posts = _posts.GetPosts();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }
  
        [HttpGet("countLikes/{postId}")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<IActionResult> CountofLikes(int postId)
        {
            if (!_posts.PostExists(postId))
                return NotFound();
            var count = await _posts.CountofLikesByPostId(postId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(count);
        }
        [HttpGet("countComments/{postId}")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<IActionResult> CountofComments(int postId)
        {
            if (!_posts.PostExists(postId))
                return NotFound();
            var count = await _posts.CountofCommentsByPostId(postId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(count);
        }
        [HttpGet("Comments/{postId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsByPostId(int postId)
        {
            var cmnts = _posts.GetCommentsByPostId(postId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(cmnts);
        }
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePost([FromBody] Post postCreate)
        {

            if (postCreate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_posts.CreatePost(postCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok(new {message = "Successfully created"});
        }
        [HttpPut("{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePost(int postId, [FromBody] Post updatedPost)
        {
            if (updatedPost == null)
                return BadRequest(ModelState);

            if (postId != updatedPost.Id)
                return BadRequest(ModelState);

            if (!_posts.PostExists(postId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_posts.UpdatePost(updatedPost))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated Successfully");
        }
        [HttpDelete("delete/{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePost(int postId)
        {
            if (!_posts.PostExists(postId))
            {
                return NotFound();
            }

            var postToDelete = _posts.GetPost(postId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_posts.DeletePost(postToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting post");
            }

            return Ok(new {message = "Deleted Successfully", postToDelete });
        }

        //[HttpGet("GetOtherUsersPosts/{loggedInUserId}")]
        //public IActionResult GetOtherUsersPosts(int loggedInUserId)
        //{
        //    var posts = _posts.GetOtherUsersPosts(loggedInUserId);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (posts == null || !posts.Any())
        //    {
        //        return NotFound(new { message = "No posts found from other users." });
        //    }
        //    return Ok(posts);
        //}

    }
}
