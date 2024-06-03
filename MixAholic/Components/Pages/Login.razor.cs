using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using MixAholic.Common;
using MixAholic.Service;
using MixAholicCommon.Model;
using static System.Net.WebRequestMethods;

namespace MixAholic.Components.Pages
{
    public partial class Login
    {
        [Inject]
        public IAuthService Service { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public UserState UserState { get; set; }

        private AuthModel loginModel = new AuthModel();
        private string ErrorMessage;

        private async Task HandleLogin()
        {
            string result;
            try
            {
                result = await Service.Login(loginModel.Username, loginModel.Password);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Falsches Passwort";
                return;
            }

            ErrorMessage = string.Empty;
            UserState.SessionKey = result;
            NavigationManager.NavigateTo($"/");

        }
    }
}
