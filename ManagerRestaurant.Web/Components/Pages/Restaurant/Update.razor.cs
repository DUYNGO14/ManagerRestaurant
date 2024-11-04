using Blazored.Toast.Services;
using ManagerRestaurant.Application.Restaurants.command.update;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Domain.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ManagerRestaurant.Web.Components.Pages.Restaurant
{
    public partial class Update
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
       [Inject ]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ApiClient ApiClient { get; set; }

        public UpdateRestaurantCommand Model { get; set; } = new();
        public List<string> Categories = new List<string> { "USA", "Canada", "UK", "Australia", "ThaiLan", "Lao", "Mexico", "VietNam" };
        protected override async Task OnInitializedAsync()
        {

            var res = await ApiClient.GetFromJsonAsync<ResponseApi>($"/api/restaurants/{Id}");
            if (res != null && res.Success)
            {
                var restau = JsonConvert.DeserializeObject<RestaurantDto>(res.Data.ToString());
                Model = new UpdateRestaurantCommand()
                {
                    Id = restau.Id,
                    Name = restau.Name,
                    Category = restau.Category,
                    ContactEmail = restau.ContactEmail,
                    ContactNumber = restau.ContactNumber,
                    Description = restau.Description,
                };
            }
            await base.OnInitializedAsync();

        }
        public async Task Submit()
        {
            var res = await ApiClient.PatchAsync<ResponseApi,UpdateRestaurantCommand>($"/api/restaurants/{Id}", Model);
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Update restaurants successflly");
                NavigationManager.NavigateTo("/restaurants");
            }
        }

    }
}
