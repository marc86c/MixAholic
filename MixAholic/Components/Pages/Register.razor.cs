using Microsoft.AspNetCore.Components;
using MixAholic.Service;
using MixAholicCommon.Model;

namespace MixAholic.Components.Pages
{
	public partial class Register
	{
		[Inject]
		public IAuthService AuthService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		private AuthModel registerModel = new AuthModel();
		private string ErrorMessage;

		private async Task HandleRegister()
		{
			var result = await AuthService.Register(registerModel.Username, registerModel.Password);
			if (!result)
			{
				ErrorMessage = "Username already exists.";
			}
			else
			{
				ErrorMessage = string.Empty;

				NavigationManager.NavigateTo($"/Login");
				// Redirect to login or other page
			}
		}
	}
}
