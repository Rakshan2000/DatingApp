using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context) : ControllerBase
{
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
        var users = await context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id:int}")] // /api/Users/id
    public async Task<ActionResult<AppUser>> GetUser(int id){
        var userId = await context.Users.FindAsync(id);

        if(userId==null) return NotFound();
        return userId;
    }
}
