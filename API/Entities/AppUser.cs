using System;
namespace API.Entities
{
	public class AppUser
	{
		public int Id { get; set; }

		public string UserName { get; set; } = "";

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public AppUser()
		{
		}

        public AppUser(string userName, byte[] passwordHash, byte[] passwordSalt)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
	
