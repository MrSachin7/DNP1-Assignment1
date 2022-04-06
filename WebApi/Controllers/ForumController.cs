using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers; 

[ApiController]
[Route("[controller]")]
public class ForumController : ControllerBase {

    private IForumService forumService;

    public ForumController(IForumService forumService) {
        this.forumService = forumService;
    }
}