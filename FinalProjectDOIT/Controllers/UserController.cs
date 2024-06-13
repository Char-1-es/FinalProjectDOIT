using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace FinalProjectDOIT.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TopicService _topicService;
        private readonly CommentService _commentService;

        public UserController(UserService userService, TopicService topicService, CommentService commentService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        [HttpGet("topic/{id}")]
        public async Task<ActionResult<ApiResponse>> GetUserByTopicId(int id)
        {

            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound(CreateResponse(null, 404, false, "Topic not availaable"));
            }

            var user = await _userService.GetUserByEmailAsync(topic.UserEmail);
            return Ok(CreateResponse(user, 200, true, "User retrieved successfully"));

        }

        [HttpGet("comment/{Commentid}")]
        public async Task<IActionResult> GetUserByCommentId(int comId)
        {
            var comment = await _commentService.GetCommentByIdAsync(comId);
            if (comment == null)
            {
                return NotFound(CreateResponse(null, 404, false, "Comment not found"));
            }

            var user = await _userService.GetUserByEmailAsync(comment.UserEmail);
            return Ok(CreateResponse(user, 200, true, "User retrieved successfully"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(CreateResponse(users, 200, true, "Users retrieved successfully"));
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(CreateResponse(null, 400, false, "Email not be null or empty"));
            }

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(CreateResponse(null, 404, false, "User not found"));
            }

            return Ok(CreateResponse(user, 200, true, "User retrieved successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("email/{email}/lockout")]
        public async Task<IActionResult> LockOutUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(CreateResponse(null, 400, false, "Email not be null or empty"));
            }

            await _userService.LockOutUserAsync(email);
            return NoContent();
        }

        private ApiResponse CreateResponse(object? result, int statusCode, bool isSuccess, string message)
        {
            return new ApiResponse
            {
                Result = result,
                StatusCode = statusCode,
                IsSuccess = isSuccess,
                Message = message
            };
        }
    }
}

