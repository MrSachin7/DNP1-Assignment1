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
    [Route("{forumId:int}/{subForumId:int}")]
    public async Task<ActionResult<SubForum>> GetSubForumAsync([FromRoute] int forumId, [FromRoute] int subForumId) {
        try {
            SubForum? subForumAsync = await forumService.GetSubForumAsync(forumId, subForumId);
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
    [Route("{forumId:int}/{subforumId:int}")]
    public async Task<ActionResult<Post>> AddPostAsync([FromRoute] int forumId, [FromRoute] int subforumId,
        [FromBody] Post post) {
        try {
            Post addPostAsync = await forumService.AddPostAsync(post, forumId, subforumId);
            return Created($"/{forumId}/{subforumId}/{addPostAsync.Id}",addPostAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("IncrementView/{forumId:int}/{subforumId:int}")]
    public async Task<ActionResult> IncrementViewOfSubForumAsync([FromRoute] int forumId, [FromRoute] int subforumId) {
        try {
            await forumService.IncrementViewOfSubForumAsync(forumId, subforumId);
            return Ok();
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{forumId:int}/{subforumId:int}/{postId:int}")]
    public async Task<ActionResult<Post>> GetPostAsync([FromRoute] int forumId, [FromRoute] int subforumId,
        [FromRoute] int postId) {
        try {
            Post? postAsync = await forumService.GetPostAsync(forumId, subforumId, postId);
            return Ok(postAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [Route("{forumId:int}/{subforumId:int}/{postId:int}")]
    public async Task<ActionResult<Comment>> AddCommentToPostAsync([FromRoute] int forumId, [FromRoute] int subForumId,
        [FromRoute] int postId, [FromBody] Comment comment) {
        try {
            Comment commentToPost = await forumService.AddCommentToPost(forumId, subForumId, postId, comment);
            return Created($"/{forumId}/{subForumId}/{postId}/{commentToPost.Id}", commentToPost);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    [Route("{forumId:int}/{subforumId:int}/{postId:int}")]
    public async Task<ActionResult<Comment>> EditCommentAsync([FromRoute] int forumId, [FromRoute] int subforumId,
        [FromRoute] int postId, [FromBody] Comment editedComment) {
        try {
            Comment editedFromServer = await forumService.EditComment(forumId, subforumId, postId, editedComment);
            return Ok(editedFromServer);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{forumId:int}/{subforumId:int}/{postId:int}/{commentId:int}")]
    public async Task<ActionResult<Comment>> DeleteCommentAsync([FromRoute] int forumId, [FromRoute] int subforumId,
        [FromRoute] int postId, [FromRoute] int commentId) {
        try {
            Comment deleteComment = await forumService.DeleteComment(forumId, subforumId, postId, commentId);
            return Ok(deleteComment);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

}