
using Microsoft.AspNetCore.Components;
using MixAholic.Common;
using MixAholic.Service;
using MixAholicCommon.Model;

namespace MixAholic.Components.Pages
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public UserState UserState { get; set; }

        [Inject]
        public IMixService MixService { get; set; }

        public List<Mix> Mixes { get; set; } = new List<Mix>();

        private bool isCreateOpen;
        private Mix NewMix;
        protected async override Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(UserState.SessionKey))
            {
                NavigationManager.NavigateTo("/Login");
                return;
            }

            Mixes = await MixService.GetMixes();
        }

        public void OpenCreate()
        {
            if (isCreateOpen)
            {
                NewMix = null;
            }
            else
            {
				NewMix = new Mix(); 
            }

			isCreateOpen = !isCreateOpen;
		}

        public async Task Save()
        {
            var createdMix = await MixService.CreateMix(NewMix);
            Mixes.Add(createdMix);
            OpenCreate();
        }

        public void OpenMix(int mixId)
        {
            NavigationManager.NavigateTo($"/Mix/{mixId}");
        }

        public void AddIngredient()
        {
            NewMix.Ingredients.Add(new Ingredient());
        }

        public void RemoveIngredient(int index)
        {
            NewMix.Ingredients.RemoveAt(index);
        }
	}
}
