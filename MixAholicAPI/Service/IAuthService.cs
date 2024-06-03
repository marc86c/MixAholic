namespace MixAholicAPI.Service
{
    public interface IAuthService
    {
        public bool Register(string username, string password);
        public string Login(string username, string password);
        public int ValidateSessionKey(string sessionKey);
    }
}
