using Microsoft.AspNetCore.Identity;
using MixAholicAPI.Store;
using MixAholicCommon.Model;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace MixAholicAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserStore userStore;

        public AuthService()
        {
            userStore = new UserStore();
        }

        public bool Register(string username, string password)
        {
            if (userStore.Users.Any(u => u.Username == username))
            {
                return false;
            }

            var salt = GenerateSalt();
            var hashedPassword = HashPassword(password, salt);

            int newId = 1;
            if (userStore.Users != null && userStore.Users.Count != 0)
            { 
                newId = userStore.Users.Max(x => x.UserID) + 1;
            }

            var sessionKeyPlain = $"{username}-{newId}-{salt}-SessionKey";

            var sessionKeyBytes = Encoding.UTF8.GetBytes(sessionKeyPlain);

            string sessionKey;
            using (var sha256 = SHA256.Create()) 
            { 
                sessionKey = Convert.ToBase64String(sha256.ComputeHash(sessionKeyBytes));
            }
            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Salt = salt,
                UserID = newId,
                SessionKey = sessionKey
            };

            userStore.Users.Add(user);
            userStore.SaveChanges();

            return true;
        }

        public string Login(string username, string password)
        {
            var user = userStore.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return string.Empty;
            }

            var hashedPassword = HashPassword(password, user.Salt);
            if (hashedPassword != user.PasswordHash)
            {
                return string.Empty;
            }

            return user.SessionKey;
        }

        private string GenerateSalt()
        {
            var buffer = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hash = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hash);
            }
        }

        public int ValidateSessionKey(string sessionKey)
        {
            var user = userStore.Users.FirstOrDefault(x => x.SessionKey == sessionKey);
            if (user == null)
            {
                return 0;
            }
            return user.UserID;
        }

		public User GetUser(int UserId)
		{
            return userStore.Users.FirstOrDefault(x => x.UserID == UserId);
		}
	}
}
