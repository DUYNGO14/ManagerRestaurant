
using Blazored.Toast.Services;
using ManagerRestaurant.Application.Common;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Domain.Model;
using ManagerRestaurant.Web.Components.BaseComponents;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ManagerRestaurant.Web.Components.Pages.Restaurant
{
    public partial class Home
    {
        //?SearchPhrase=q&PageNumber=1&PageSize=2&SortBy=Name&SortDirection=0'
        [Inject]
        public ApiClient? ApiClient { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }

        public int PageNumber { get; set; } = 1;
        public string SearchPhrase { get; set; }= string.Empty;
        public string SortBy { get; set; } = string.Empty;

        public SortDirection SortDirection { get; set; }
        public AppModal AppModal { get; set; }
        public int DeleteId { get; set; }
        public int TotalPage { get; set; }
        public List<RestaurantDto>? RestaurantDtos { get; set; }
        public PageResult<RestaurantDto>? Model { get; set; }
        public List<string> Category = new List<string> { "USA", "Canada", "UK", "Australia" };
        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }
        protected async Task LoadData()
        {
            var res = await ApiClient.GetFromJsonAsync<ResponseApi>($"api/restaurants?SearchPhrase={SearchPhrase}&PageNumber={PageNumber}&PageSize=5&SortBy={SortBy}&SortDirection=0");
            if (res != null && res.Success)
            {
                Model = JsonConvert.DeserializeObject<PageResult<RestaurantDto>>(res.Data.ToString());
                RestaurantDtos = Model.Items.ToList();
                TotalPage = Model.TotalPage;
            }

        }

        protected async Task Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchPhrase))
            {
                await LoadData();
            }
        }
        private async Task NextPage()
        {
            if (PageNumber < TotalPage)
            {
                PageNumber++;
                await LoadData();
            }
        }

        private async Task PreviousPage()
        {
            if (PageNumber > 1)
            {
                PageNumber--;
                await LoadData();
            }
        }

        protected async Task HandleDelete()
        {
            var res = await ApiClient.DeleteAsync<ResponseApi>($"/api/restaurants/{DeleteId}");
            if (res != null && res.Success)
            {
                ToastService.ShowSuccess("Deleted restaurant successflly!");
                await LoadData();
                AppModal.Close();
            }
        }


    }
}
