using Blazored.Toast.Services;
using ManagerRestaurant.Application.Dishs.command.update;
using ManagerRestaurant.Application.Dishs.dto;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ManagerRestaurant.Web.Components.Pages.Dish
{
    public partial class UpdateDish
    {
        [Parameter]
        public int RestautantId { get; set; }
        [Parameter]
        public int DishId { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ApiClient ApiClient { get; set; }

        public UpdateDishForRestaurantCommand Model { get; set; } = new();
        public DishDto Dish { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {

            await LoadData();
            await base.OnInitializedAsync();

        }
         protected async Task LoadData()
            {
                var res = await ApiClient.GetFromJsonAsync<ResponseApi>($"api/restaurant/{RestautantId}/dishes/{DishId}");
                if (res != null && res.Success)
                {
                Dish = JsonConvert.DeserializeObject<DishDto>(res.Data.ToString());

                Model.RestaurantId = Dish.Id;
                Model.Name = Dish.Name;
                Model.Description = Dish.Description;
                Model.Price = Dish.Price;
                Model.KiloCalories = Dish.KiloCalories;
                }
            }
        public async Task Submit()
        {
            var res = await ApiClient.PatchAsync<ResponseApi, UpdateDishForRestaurantCommand>($"api/restaurant/{RestautantId}/dishes/{DishId}", Model);
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Update dish successflly");
                NavigationManager.NavigateTo($"/restaurants/{RestautantId}/dishes");
            }
        }
    }
}
