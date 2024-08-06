
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController (DataContext context) : BaseApiController
{
    
    [HttpPost("register")] // /api/Users/id
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto){

        if(await UserExists(registerDto.Username)) return BadRequest("Username is already taken.");

        using var hmac = new HMACSHA512();

        var user = new AppUser{
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };    

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;

    }

    [HttpPost("login")]
    public async Task<ActionResult<AppUser>> Login(LoginDto LoginDto){
        
        var user = await context.Users.FirstOrDefaultAsync(
            x=> x.UserName == LoginDto.Username
        );

        if(user == null) return Unauthorized("Invalid Username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(LoginDto.Password));
        
        for(int i=0;i<ComputeHash.Length;i++){
            if(ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return user;

    }

    private async Task<bool> UserExists(string username){

        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower()); //Bob!=bob

    }
}
