using System;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(IUserRepository userRepository) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMemebersAsync();
        
        return Ok(users);
    }
    [Authorize]
     [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>>
    GetUsers(string username)
    {
        var user = await userRepository.GetMembersAsync(username);
        if(user == null) return NotFound();
        return user;
    }
}
