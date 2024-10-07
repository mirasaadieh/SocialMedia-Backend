using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : Controller
    {
        private readonly ILikes _like;
        public LikeController(ILikes like)
        {
            _like = like;
        }
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Like>))]
        public IActionResult GetLikes()
        {
            var likes = _like.GetLikes();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(likes);
        }
        
        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLike([FromBody] Like likeCreate)
        {
            var like = _like.GetLikes()
               .Where(c => c.UserId == likeCreate.UserId)
               .FirstOrDefault();
            if (likeCreate == null || !ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            //if (like == null)
            //{
            //    ModelState.AddModelError("", "like already exists");
            //    return StatusCode(422, ModelState);
            //}
            if (likeCreate == null)
                return BadRequest(ModelState);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_like.CreateLike(likeCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            var likeId = likeCreate.Id;
            return Ok(likeId);
        }
        [HttpDelete("delete/{likeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RemovePost(int likeId)
        {
            if (!_like.LikeExists(likeId))
            {
                return NotFound();
            }

            var liketoRemove = _like.GetLike(likeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_like.RemoveLike(liketoRemove))
            {
                ModelState.AddModelError("", "Something went wrong removing like");
            }

            return Ok(new {message =  "Deleted Successfully"});
        }
    }
}
