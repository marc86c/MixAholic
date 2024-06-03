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
	}
}
