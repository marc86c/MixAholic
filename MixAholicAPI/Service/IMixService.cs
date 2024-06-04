using MixAholicCommon.Model;

namespace MixAholicAPI.Service
{
    public interface IMixService
    {
        public Mix CreateMix(Mix mix, int userID);
        public void UpdateMix(Mix mix);
        public void RemoveMix(int mixId);
        public Rating RateMix(Rating rating, User user);
        public List<Mix> GetMixes();
        public Mix GetMix(int mixId);
    }
}
