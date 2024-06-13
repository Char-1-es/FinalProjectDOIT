using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FinalProjectDOIT.Entities;

namespace FinalProjectDOIT.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly TopicService _topicService;

        public CommentController(CommentService commentService, TopicService topicService)
        {
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
            _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
        }

        [HttpGet("topic/{id}")]
        public async Task<IActionResult> GetCommentsByTopic(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound(new ApiResponse
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Topic not available"
                });
            }

            var comments = topic.Comments.ToList();
            return Ok(new ApiResponse
            {
                StatusCode = 200,
                IsSuccess = true,
                Result = comments,
                Message = "Comments retrieved successfully"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comments = await _commentService.GetCommentByIdAsync(id);
            if (comments == null)
            {
                return NotFound(new ApiResponse
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Comment not available"
                });
            }

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                IsSuccess = true,
                Result = comments,
                Message = "Comment retrieved successfully"
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDTO)
        {
            var topic = await _topicService.GetTopicByIdAsync(commentDTO.TopicId);
            if (topic == null)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Incorrect topic ID"
                });
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var createdCommentDTO = await _commentService.CreateCommentAsync(commentDTO, userId);
            return CreatedAtAction(nameof(GetCommentById), new ApiResponse
            {
                StatusCode = 201,
                IsSuccess = true,
                Result = createdCommentDTO,
                Message = "Comment successfully created"
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDTO commentDTO)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new ApiResponse
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Comment not available"
                });
            }

            var topic = await _topicService.GetTopicByIdAsync(commentDTO.TopicId);
            if (topic == null || topic.Status == TopicStatus.Inactive)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Topic is inactive or unavailable"
                });
            }

            string? currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (comment.UserEmail != currentUserEmail)
            {
                return Unauthorized();
            }

            await _commentService.UpdateCommentAsync(commentDTO);
            return Ok(new ApiResponse
            {
                StatusCode = 200,
                IsSuccess = true,
                Result = commentDTO,
                Message = "Comment successfully updated"
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new ApiResponse
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Comment not available"
                });
            }

            string? currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (comment.UserEmail != currentUserEmail)
            {
                return Unauthorized();
            }

            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}