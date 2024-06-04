using Microsoft.AspNetCore.Components;
using MixAholic.Service;
using MixAholicCommon.Model;

namespace MixAholic.Components.Pages
{
	public partial class MixView
	{
		[Parameter]
		public string MixIDString { get; set; }
		public int MixID => int.Parse(MixIDString);

		[Inject]
		public IMixService MixService { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		public Mix Mix { get; set; }
		public bool IsOwner { get; set; }
		public bool isEditing;
		public Rating NewRating { get; set; }
		public bool isRating;
		protected async override Task OnInitializedAsync()
		{
			Mix = await MixService.GetMix(MixID);
			IsOwner = await MixService.IsOwner(MixID);
		}

		public void OpenEdit()
		{ 
			isEditing = !isEditing;
		}

		public async Task Save()
		{
			await MixService.UpdateMix(Mix);
		}

		public async Task Remove()
		{
			await MixService.RemoveMix(MixID);
			NavigationManager.NavigateTo("/");
		}

		public List<int> GetStars(decimal rating)
		{
			const int maxStars = 5;
			List<int> stars = new List<int>();

			int wholeStars = (int)rating;
			stars.AddRange(Enumerable.Repeat(1, wholeStars));

			if (rating % 1 >= 0.5m)
			{
				stars.Add(2);
			}

			stars.AddRange(Enumerable.Repeat(0, maxStars - stars.Count));

			return stars;
		}

		public void HandleRating()
		{
			isRating = !isRating;
			if (isRating)
			{

				NewRating = new Rating()
				{
					MixID = MixID,
				};
			}
			else
			{
				NewRating = null;
			}

		}

		public async Task SaveRating()
		{
			var rating = await MixService.RateMix(NewRating);
			Mix.Ratings.Add(rating);
			HandleRating();
		}
	}
}
