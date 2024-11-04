using Blazored.Toast.Services;
using ManagerRestaurant.Application.Dishs.command.create;
using ManagerRestaurant.Application.Dishs.dto;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Domain.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ManagerRestaurant.Web.Components.Pages.Dish
{
    public partial class CreateDish
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int RestautantId { get; set; }
        public CreateDishCommand Model { get; set; } = new();
        public string NameRestaurant { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }
        protected async Task LoadData()
        {
            var res = await ApiClient.GetFromJsonAsync<ResponseApi>($"/api/restaurants/{RestautantId}");
            if (res != null && res.Success)
            {
                var restaurantDto = JsonConvert.DeserializeObject<RestaurantDto>(res.Data.ToString());
                NameRestaurant = restaurantDto.Name;
            }
        }
        public async Task Submit()
        {
            var res = await ApiClient.PostAsync<ResponseApi, CreateDishCommand>($"/api/restaurant/{RestautantId}/dishes", Model);
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Create dish successfully!");
                NavigationManager.NavigateTo($"/restaurants/{RestautantId}/dishes");
            }
        }
    }
}
