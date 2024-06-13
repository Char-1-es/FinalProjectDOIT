using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FinalProjectDOIT.Entities;
using System.Net;


namespace FinalProjectDOIT.Controllers
{
    [ApiController]
    [Route("api/topics")]
    public class TopicController : ControllerBase
    {
        private readonly TopicService _topicService;

        public TopicController(TopicService topicService)
        {
            _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var topics = await _topicService.GetAllTopicsAsync();
            return Ok(CreateResponse(topics, 200, true, "Topics retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicById(int id)
        {
            try
            {
                var topicDetail = await _topicService.GetTopicByIdAsync(id);
                if (topicDetail == null)
                {
                    return NotFound(CreateResponse(null, 404, false, "Topic not available"));
                }
                return Ok(CreateResponse(topicDetail, 200, true, "Topics retrieved successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(CreateResponse(null, 400, false, ex.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTopic([FromBody] TopicDTO topicDTO)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            string userEmail = User.Identity.Name;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var createdTopicDTO = await _topicService.CreateTopicAsync(topicDTO, userEmail, userId);
            return CreatedAtAction(nameof(GetTopicById), CreateResponse(createdTopicDTO, 201, true, "Topic successfully created"));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicDTO topicDTO)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                {
                    return NotFound(CreateResponse(null, 404, false, "Topic not available"));
                }

                string? currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                if (topic.UserEmail != currentUserEmail)
                {
                    return Unauthorized();
                }

                await _topicService.UpdateTopicAsync(id, topicDTO);
                return Ok(CreateResponse(null, 200, true, "Topic successfully updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(CreateResponse(null, 400, false, ex.Message));
            }
        }

        [HttpPatch("{id}/state")]
        [Authorize]
        public async Task<IActionResult> UpdateTopicState(int id, [FromBody] TopicState state)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                {
                    return NotFound(CreateResponse(null, 404, false, "Topic not available"));
                }

                string? currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                if (topic.UserEmail != currentUserEmail)
                {
                    return Unauthorized();
                }

                await _topicService.UpdateTopicStateAsync(id, state);
                return Ok(CreateResponse(null, 200, true, "Topic state updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(CreateResponse(null, 400, false, ex.Message));
            }
        }

        [HttpPatch("{TopicId}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateTopicStatus(int id, [FromBody] TopicStatus status)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                {
                    return NotFound(CreateResponse(null, 404, false, "Topic not Available"));
                }

                string? currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                if (topic.UserEmail != currentUserEmail)
                {
                    return Unauthorized();
                }

                await _topicService.UpdateTopicStatus(id, status);
                return Ok(CreateResponse(null, 200, true, "Topic status updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(CreateResponse(null, 400, false, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound(CreateResponse(null, 404, false, "Topic not available"));
            }

            string? currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (topic.UserEmail != currentUserEmail)
            {
                return Unauthorized();
            }

            await _topicService.DeleteTopicAsync(id);
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
