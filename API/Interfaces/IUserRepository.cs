using System;
using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool>SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?>GetUserByUserNameAync(string username);
    Task<IEnumerable<MemberDto>>GetMemebersAsync();
    Task<MemberDto>GetMembersAsync(string username);

}
