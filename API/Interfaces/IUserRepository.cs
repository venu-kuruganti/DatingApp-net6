using System;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
	public interface IUserRepository
	{
		Task<bool> SaveAllAsync();

		Task<IEnumerable<MemberDto>> GetUsersAsync();

		Task<AppUser?> GetUserByIdAsync(int id);

		Task<MemberDto?> GetUserByUsernameAsync(string username);

		Task<MemberDto?> GetMemberAsync(string username);

		Task<IEnumerable<MemberDto?>> GetMembersAsync();
	}
}

