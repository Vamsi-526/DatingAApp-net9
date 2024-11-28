using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<MemberDto?> GetUserByIdAsync(string username)
    {
    return  await context.Users.Where(x => x.UserName == username)
        .ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();

    }

    public async Task<IEnumerable<MemberDto?>> GetMembersAsync(string username)
    {
        return  await context.Users
        .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        .ToListAsync();
    }

    public  async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await context.Users.Include(x=>x.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync()>0;
    }

    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }

    public Task<AppUser?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser?> GetUserByUserNameAync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MemberDto>> GetMemebersAsync(string username)
    {
        throw new NotImplementedException();
    }

    Task<MemberDto> IUserRepository.GetMembersAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MemberDto>> GetMemebersAsync()
    {
        throw new NotImplementedException();
    }
}
