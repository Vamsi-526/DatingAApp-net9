using System;
using System.Security.Cryptography;
using API.Entities;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using System.Globalization;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers;
[Route("api/[controller]")]

public class AccountController(DataContext context) : BaseApiController
{
     [HttpPost("Regster")]
     public async Task <ActionResult<UserDto>> Register(RegisterDto registerDto)
     {
         if(await UserExists(registerDto.Username)) return BadRequest("UserName is Taken");

         return Ok();
        /*using var hmac = new HMACSHA512();
        var user= new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDto
        {
         UserName = user.UserName,
         Token = tokenService.CreateToken(user)
        };*/
     }
     [HttpPost("login")]

     public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
     {
        var user= await context.Users.FirstOrDefaultAsync(x =>x.UserName == loginDto.Username.ToLower());
        if(user ==null) return Unauthorized("Invalid username");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for(int i=0; i<computedHash.Length; i++)
        {
                   if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }
        return user;
     }
     private async Task<bool> UserExists(String username)
     {
        return await context.Users.AnyAsync(x=> x.UserName.ToLower() ==username.ToLower());
     }
}
