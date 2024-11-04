using Blazored.Toast.Services;
using ManagerRestaurant.Application.Dishs.dto;
using ManagerRestaurant.Application.Restaurants.command.update;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Domain.Model;
using ManagerRestaurant.Web.Components.BaseComponents;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ManagerRestaurant.Web.Components.Pages.Dish
{
    public partial class HomeDish
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public AppModal AppModal { get; set; }

        [Parameter]
        public int RestautantId { get; set; }

        public int DeleteId { get; set; }
        public List<DishDto> Model { get; set; } = new();
        public string NamerRestaurant { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }
        protected async Task LoadData()
        {
            var dish = await ApiClient.GetFromJsonAsync<ResponseApi>($"api/restaurant/{RestautantId}/dishes");
            if (dish != null && dish.Success)
            {
                Model = JsonConvert.DeserializeObject<List<DishDto>>(dish.Data.ToString());
            }

            var res = await ApiClient.GetFromJsonAsync<ResponseApi>($"/api/restaurants/{RestautantId}");
            if (res != null && res.Success)
            {
                var restaurantDto = JsonConvert.DeserializeObject<RestaurantDto>(res.Data.ToString());
                NamerRestaurant = restaurantDto.Name;
            }
        }
        protected async Task HandleDelete()
        {
            var res = await ApiClient.DeleteAsync<ResponseApi>($"api/restaurant/{RestautantId}/dishes/{DeleteId}");
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Deleted dish successflly!");
                await LoadData();
                AppModal.Close();
            }
        }
    }
}
