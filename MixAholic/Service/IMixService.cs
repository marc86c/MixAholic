using MixAholicCommon.Model;

namespace MixAholic.Service
{
    public interface IMixService
    {
        public Task<List<Mix>> GetMixes();
        public Task<Mix> GetMix(int mixId);
        public Task<Mix> CreateMix(Mix mix);
        public Task<bool> IsOwner(int mixId);

        public Task UpdateMix(Mix mix);
        public Task RemoveMix(int mixId);
        public Task<Rating> RateMix(Rating rating);
    }
}
