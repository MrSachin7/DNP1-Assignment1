using System.Runtime.InteropServices;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ForumController : ControllerBase {
    private IForumService forumService;

    public ForumController(IForumService forumService) {
        this.forumService = forumService;
    }

    [HttpPost]
    public async Task<ActionResult<Forum>> AddForumAsync([FromBody] Forum forum) {
        try {
            Forum forumFromServer = await forumService.AddForumAsync(forum);
            return Created($"/{forumFromServer.Id}", forumFromServer);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Forum>> GetForumById([FromRoute] int id) {
        try {
            Forum forum = await forumService.GetForumByIdAsync(id);
            return Ok(forum);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Forum>>> GetAllForumsAsync() {
        try {
            List<Forum> allForumsAsync = await forumService.GetAllForumsAsync();
            return Ok(allForumsAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("/SubForum/{subForumId:int}")]
    public async Task<ActionResult<SubForum>> GetSubForumAsync( [FromRoute] int subForumId) {
        try {
            SubForum? subForumAsync = await forumService.GetSubForumAsync(subForumId);
            return Ok(subForumAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Route("{forumId:int}")]
    public async Task<ActionResult<SubForum>> AddSubForumAsync([FromRoute] int forumId, [FromBody] SubForum subForum) {
        try {
            SubForum addSubForumAsync = await forumService.AddSubForumAsync(subForum, forumId);
            return Created($"/{forumId}/{addSubForumAsync.Id}", addSubForumAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("IncrementView/{forumId:int}")]
    public async Task<ActionResult> IncrementViewOfForumAsync([FromRoute] int forumId) {
        try {
            await forumService.IncrementViewOfForumAsync(forumId);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Route("/Post/{subforumId:int}")]
    public async Task<ActionResult<Post>> AddPostAsync([FromRoute] int subforumId,
        [FromBody] Post post) {
        try {
            Post addPostAsync = await forumService.AddPostAsync(post, subforumId);
            return Created($"/{subforumId}/{addPostAsync.Id}", addPostAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("IncrementViewSubForum/{subforumId:int}")]
    public async Task<ActionResult> IncrementViewOfSubForumAsync([FromRoute] int subforumId) {
        try {
            await forumService.IncrementViewOfSubForumAsync(subforumId);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("/Post/{postId:int}")]
    public async Task<ActionResult<Post>> GetPostAsync(
        [FromRoute] int postId) {
        try {
            Post? postAsync = await forumService.GetPostAsync(postId);
            return Ok(postAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Route("Comment/{postId:int}")]
    public async Task<ActionResult<Comment>> AddCommentToPostAsync(
        [FromRoute] int postId, [FromBody] Comment comment) {
        try {
            Comment commentToPost = await forumService.AddCommentToPost(postId, comment);
            return Created($"/{postId}/{commentToPost.Id}", commentToPost);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    [Route("Comment")]
    public async Task<ActionResult<Comment>> EditCommentAsync(
      [FromBody] Comment editedComment) {
        try {
            Comment editedFromServer = await forumService.EditComment(editedComment);
            return Ok(editedFromServer);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("Comment/{commentId:int}")]
    public async Task<ActionResult<Comment>> DeleteCommentAsync(
        [FromRoute] int commentId) {
        try {
            Comment deleteComment = await forumService.DeleteComment(commentId);
            return Ok(deleteComment);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}