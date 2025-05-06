using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Posts;
using MrKatsuWebAPI.Application.Replies;
using MrKatsuWebAPI.DTO.Paging;
using MrKatsuWebAPI.DTO.PostRequest;
using MrKatsuWebAPI.DTO.ReplyRequest;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostsController(IPostService _postService, IReplyService _replyService) : BaseController
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetPosts([FromQuery]PagingRequest request)
        {
            var result = await _postService.GetPosts(request);
            return Ok(result);
        }
        [HttpGet("get-post")]
        public async Task<IActionResult> GetPost(int id, [FromQuery] PagingRequest request)
        {
            var result = await _postService.GetPostById(id, request);
            return Ok(result);
        }
        [HttpGet("get-replies")]
        public async Task<IActionResult> GetReplies(int postId, [FromQuery] PagingRequest request)
        {
            var result = await _postService.GetRepliesByPostId(postId, request);
            return Ok(result);
        }
        [HttpPost("create-post"), Authorize]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _postService.CreatePost(request, userId.Value);
            return Ok(result);
        }
        [HttpPut("update-post"), Authorize]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _postService.UpdatePost(id, request, userId.Value);
            return Ok(result);
        }
        [HttpDelete("delete-post"), Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _postService.DeletePost(id, userId.Value);
            return Ok(result);
        }
        [HttpPost("create-reply"), Authorize]
        public async Task<IActionResult> CreateReply([FromBody] ReplyRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _replyService.CreateReply(request, userId.Value);
            return Ok(result);
        }
        [HttpPut("update-reply"), Authorize]
        public async Task<IActionResult> UpdateReply(int id, [FromBody] ReplyRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _replyService.UpdateReply(id, request, userId.Value);
            return Ok(result);
        }
        [HttpDelete("delete-reply"), Authorize]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _replyService.DeleteReply(id, userId.Value);
            return Ok(result);
        }
        [HttpPost("create-notify"), Authorize]
        public async Task<IActionResult> CreateNotification([FromBody] PostRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _postService.CreateNotification(request, userId.Value);
            return Ok(result);
        }
        [HttpGet("get-notify")]
        public async Task<IActionResult> GetNotifications()
        {
            var result = await _postService.GetNotifications();
            return Ok(result);
        }
        [HttpGet("delete-notify"), Authorize]
        public async Task<IActionResult> DeleteNotify(int id)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();
            var result = await _postService.DeleteNotify(id, userId.Value);
            return Ok(result);
        }
    }
}
