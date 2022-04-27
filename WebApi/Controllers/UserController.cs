using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    private IUserService userService;

    public UserController(IUserService userService) {
        this.userService = userService;
    }

    [HttpGet]
    [Route("{username}") ]
    public async Task<ActionResult<User>> GetUser([FromRoute] string username) {
        try {
            User userAsync = await userService.GetUserAsync(username);
            return Ok(userAsync);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user) {
        try {
            await userService.CreateUserAsync(user.Username,user.Password);
            return Ok(user);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);

        }
    }
}