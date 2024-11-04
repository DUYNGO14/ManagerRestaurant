using ManagerRestaurant.Domain.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using NuGet.Common;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ManagerRestaurant.Web.Authencation
{
    public class CustomAuthStateProvider(ProtectedLocalStorage localStorage) : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionModel = (await localStorage.GetAsync<ResponseApi>("sesionState")).Value;

            var identity = sesionModel == null ? new ClaimsIdentity(): GetClaimsIdentity(sesionModel.Data.ToString());
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
       

        public async Task MarkUserAuthencaticated(ResponseApi model)
        {
            await localStorage.SetAsync("sesionState",model);
            var identity = GetClaimsIdentity(model.Data.ToString());
            var user = new ClaimsPrincipal(identity);
           
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
        private ClaimsIdentity GetClaimsIdentity(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }

        public async Task MarkUserLogout()
        {
            await localStorage.DeleteAsync("authToken");
            var identity =new  ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
