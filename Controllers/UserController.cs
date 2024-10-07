using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogin _user;
        public UserController(IUserLogin user)
        {
            _user = user;
        }
        [HttpGet("allUsers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserLogin>))]
        public IActionResult GetUsers()
        {
            var users = _user.GetUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("getUser/{userId}")]
        [ProducesResponseType(200, Type = typeof(UserLogin))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_user.UserExists(userId))
                return NotFound();

            var user = _user.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("getPost/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        [ProducesResponseType(400)]
        public IActionResult GetPostByUserId(int userId)
        {
            var posts = _user.GetPostByUserId(userId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(posts);
        }
        [HttpGet("Likes/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Like>))]
        [ProducesResponseType(400)]
        public IActionResult GetLikesByUserId(int userId)
        {
            var likes = _user.GetLikesByUserId(userId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(likes);
        }
        [HttpGet("Comments/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsByUserId(int userId)
        {
            var cmnts = _user.GetCommentsByUserId(userId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(cmnts);
        }
        [HttpGet("Followers/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Follower>))]
        [ProducesResponseType(400)]
        public IActionResult GetFollowersByUserId(int userId)
        {
            var followers = _user.GetFollowersByUserId(userId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(followers);
        }
        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserLogin userCreate)
        {

            if (userCreate == null)
                return BadRequest(ModelState);

            var user = _user.GetUsers()
                .Where(c => c.UserName.Trim().ToUpper() == userCreate.UserName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_user.CreateUser(userCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully created" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest(new { error = "Username and password are required" });
            }

            // Check if the user exists and validate credentials
            var user = await _user.ValidateUserAsync(loginModel.UserName, loginModel.Password);

            if (user != null)
            {
                return Ok(new { valid = true, message = "Login successful", user });
            }
            else
            {
                return Unauthorized(new { valid = false, message = "Invalid username or password" });
            }
        }

        [HttpPut("update/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserLogin updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userId != updatedUser.Id)
                return BadRequest(ModelState);

            if (!_user.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_user.UpdateUser(updatedUser))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Updated Successfully" });
        }
        [HttpDelete("delete/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_user.UserExists(userId))
            {
                return NotFound();
            }

            var userToDelete = _user.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_user.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }

            return Ok("Deleted Successfully");
        }
        [HttpGet("countPosts/{userId}")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<IActionResult> CountofLikes(int userId)
        {
            if (!_user.UserExists(userId))
                return NotFound();
            var count = await _user.NbOfPostsByUserId(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(count);
        }
        [HttpGet("countFollowers/{userId}")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<IActionResult> CountofFollowers(int userId)
        {
            if (!_user.UserExists(userId))
                return NotFound();
            var count = await _user.NbOfFollowersByUserId(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(count);
        }
        [HttpGet("search/{searchTerm}")]
        public IActionResult Search( string searchTerm)
        {
            var users = _user.SearchUsers(searchTerm);
            return Ok(users);
        }
        [HttpGet("GetOtherUsersPosts/{loggedInUserId}")]
        public IActionResult GetOtherUsersPosts(int loggedInUserId)
        {
            var posts = _user.GetOtherUsersPosts(loggedInUserId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (posts == null || !posts.Any())
            {
                return NotFound(new { message = "No posts found from other users." });
            }
            return Ok(posts);
        }
    }
   
}
