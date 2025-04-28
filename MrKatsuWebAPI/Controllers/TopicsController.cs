using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Topics;
using MrKatsuWebAPI.DTO.TopicRequest;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/topic")]
    [ApiController]
    public class TopicsController : BaseController
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicService _topicService;
        public TopicsController(ITopicRepository topicRepository, ITopicService topicService)
        {
            _topicRepository = topicRepository;
            _topicService = topicService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var topics = await _topicRepository.GetAllAsync();
            return Ok(topics);
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetTopics()
        {
            var topics = await _topicService.GetTopics();
            return Ok(topics);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicDetail(string id, [FromQuery] int skip = 0, [FromQuery] int limit = 10)
        {
            try
            {
                var topic = await _topicService.GetTopicDetail(id, skip, limit);
                return Ok(topic);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    var topic = await _topicRepository.GetByIdAsync(id);
        //    if (topic == null)
        //        return NotFound();
        //    return Ok(topic);
        //}
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] TopicCreateRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _topicRepository.CreateAsync(request, userId);
            return Ok();
        }
    }
}
