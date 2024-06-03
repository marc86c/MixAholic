using MixAholicCommon.Model;

namespace MixAholic.Service
{
    public interface IAuthService
    {
        public Task<bool> Register(string user, string password);
        public Task<string> Login(string user, string password);

    }
}
