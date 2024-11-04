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
            var tokenModel = (await localStorage.GetAsync<string>("token")).Value;

            var identity = tokenModel == null ? new ClaimsIdentity(): GetClaimsIdentity(tokenModel);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
        public async Task MarkUserAuthencaticated(ResponseApi model)
        {
            var token = model.Data.ToString();
            await localStorage.SetAsync("token", token);
            var identity = GetClaimsIdentity(token);
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
            await localStorage.DeleteAsync("token");
            var identity =new  ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
