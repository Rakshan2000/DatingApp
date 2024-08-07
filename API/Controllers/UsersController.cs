using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
        var users = await context.Users.ToListAsync();
        return users;
    }
    [Authorize]
    [HttpGet("{id:int}")] // /api/Users/id
    public async Task<ActionResult<AppUser>> GetUser(int id){
        var userId = await context.Users.FindAsync(id);

        if(userId==null) return NotFound();
        return userId;
    }
}
