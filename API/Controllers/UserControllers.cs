using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class UserControllers(DataContext context) : ControllerBase
{
    public DataContext Context { get; }

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers(){
        var users = context.Users.ToList();

        return users;
    }

    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUsers(int id)
    {
        int id = context.Users.Find(id);

        return id;
    }
}
