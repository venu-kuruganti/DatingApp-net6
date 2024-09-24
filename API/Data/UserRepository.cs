using System;
using API.Interfaces;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
	public class UserRepository : IUserRepository
	{
        private readonly DataContext context;
        private readonly IMapper mapper;

		public UserRepository(DataContext _context, IMapper _mapper)
		{
            context = _context;
            mapper = _mapper;
		}

        public async Task<MemberDto?> GetMemberAsync(string username)
        {
            return await context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();                
        }

        public async Task<IEnumerable<MemberDto?>> GetMembersAsync()
        {
            return await context.Users
                  .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                  .ToListAsync();
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<MemberDto?> GetUserByUsernameAsync(string username)
        {
            return await GetMemberAsync(username); 
        }

        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
            return await GetMembersAsync();

        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}

