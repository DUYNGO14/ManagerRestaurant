using Blazored.Toast.Services;
using ManagerRestaurant.Application.Restaurants.command.Create;
using ManagerRestaurant.Domain.Model;
using Microsoft.AspNetCore.Components;

namespace ManagerRestaurant.Web.Components.Pages.Restaurant
{
    public partial class Create
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public CreateRestaurantCommand Model { get; set; } = new();
        public List<string> Categories = new List<string> { "USA", "Canada", "UK", "Australia", "ThaiLan", "Lao", "Mexico", "VietNam" };
        
        public async Task Submit()
        {
            var res = await ApiClient.PostAsync<ResponseApi, CreateRestaurantCommand>("/api/restaurants", Model);
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Create restaurant successfully!");
                NavigationManager.NavigateTo("/restaurants");
            }
        }

    }
}
