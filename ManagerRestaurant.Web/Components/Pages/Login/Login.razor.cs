using ManagerRestaurant.Web.Authencation;
using Blazored.Toast.Services;
using ManagerRestaurant.Application.Users.Command.LoginUser;
using ManagerRestaurant.Domain.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
namespace ManagerRestaurant.Web.Components.Pages.Login
{
    public partial class Login
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }
        public UserLogin loginModel { get; set; } = new();
        public string error { get; set; } = string.Empty;

        private async Task HandleLogin()
        {
            var res = await ApiClient.PostAccount<ResponseApi, UserLogin>("/api/account/singin", loginModel);
            if (res != null && res.Data != null)
            {
                await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAuthencaticated(res);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                error = "Email or Password errorr!";
            }
        }
    }
}
