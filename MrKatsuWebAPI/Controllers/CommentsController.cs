using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Comments;
using MrKatsuWebAPI.Application.Topics;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.CommentRequest;
using System.Security.Claims;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentsController : BaseController
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicService topicService;
        public CommentsController(ICommentRepository commentRepository,
            ITopicRepository topicRepository, ITopicService topicService)
        {
            _commentRepository = commentRepository;
            _topicRepository = topicRepository;
            this.topicService = topicService;
        }
        [HttpGet("topic/{topicId}")]
        public async Task<IActionResult> GetByTopicId(string topicId)
        {
            var comments = await _commentRepository.GetByTopicIdAsync(topicId);
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CommentCreateRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized();
            var result = await topicService.Comment(request, userId);
            return Ok(result);
        }

    }
}
