using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : Controller
    {
        private readonly IComments _comment;
        public CommentController(IComments comment)
        {
            _comment = comment;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public IActionResult GetComments()
        {
            var cmnts = _comment.GetComments();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cmnts);
        }
        [HttpGet("{commentId}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int commentId)
        {
            if (!_comment.CommentExists(commentId))
                return NotFound();

            var cmnt = _comment.GetComment(commentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cmnt);
        }
        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComment([FromBody] Comment commentCreate)
        {

            if (commentCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_comment.Create(commentCreate))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok(new {message = "Successfully created"});
        }
    }
}
