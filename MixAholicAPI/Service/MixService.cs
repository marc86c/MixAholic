using MixAholicAPI.Store;
using MixAholicCommon.Model;

namespace MixAholicAPI.Service
{
    public class MixService : IMixService
    {
        private readonly MixStore mixStore;

        public MixService()
        {
            mixStore = new MixStore();
        }

        public Mix CreateMix(Mix mix, int userID)
        {
			int newId = 1;
			if (mixStore.Mixes != null && mixStore.Mixes.Count != 0)
			{
				newId = mixStore.Mixes.Max(x => x.MixID) + 1;
			}

			mix.MixID = newId;
            mix.CreatorUserID = userID;

            mixStore.Mixes.Add(mix);
            mixStore.SaveChanges();

            return mix;
        }

        public List<Mix> GetMixes()
        {
            return mixStore.Mixes;
        }

        public Rating RateMix(Rating rating, User user)
        {
            var currentMix = mixStore.Mixes.FirstOrDefault(x => x.MixID == rating.MixID);

			int newId = 1;
			if (currentMix.Ratings != null && currentMix.Ratings.Count != 0)
			{
				newId = mixStore.Mixes.Max(x => x.MixID) + 1;
			}

            rating.UserID = user.UserID;
            rating.Username = user.Username;
			currentMix.Ratings.Add(rating);
            UpdateMix(currentMix);

            return rating;
        }

        public void RemoveMix(int mixId)
        {
            var currentMix = mixStore.Mixes.FirstOrDefault(x => x.MixID == mixId);

            mixStore.Mixes.Remove(currentMix);
            mixStore.SaveChanges();
        }

        public void UpdateMix(Mix mix)
        {
            var currentMix = mixStore.Mixes.FirstOrDefault(x => x.MixID == mix.MixID);

            mixStore.Mixes.Remove(currentMix);
            mixStore.Mixes.Add(mix);
            mixStore.SaveChanges();
        }


        public Mix GetMix(int mixId)
        {
            return mixStore.Mixes.FirstOrDefault(x => x.MixID == mixId);
        }
    }
}
