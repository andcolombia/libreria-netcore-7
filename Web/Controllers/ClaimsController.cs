using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcCode.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IConfiguration _configuration;
        public ClaimsController(IConfiguration _config)
        {
            _configuration = _config;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var httpClient = new HttpClient();
            var userInfo = new UserInfoRequest();

            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            //You get the user's first and last name below:
            ViewBag.Name = userClaims?.FindFirst("audd")?.Value;

            // The 'preferred_username' claim can be used for showing the username
            ViewBag.Username = userClaims?.FindFirst("audd")?.Value;

            // The subject/ NameIdentifier claim can be used to uniquely identify the user across the web
            ViewBag.Subject = userClaims?.FindFirst("sub")?.Value;

            // TenantId is the unique Tenant Id - which represents an organization in Azure AD
            ViewBag.TenantId = userClaims?.FindFirst("isss")?.Value;
            string authority = _configuration.GetValue<string>(
               "ServerSettings:authority");
            userInfo.Address = authority + "/connect/userinfo";
            userInfo.Token = userClaims?.FindFirst("access_token")?.Value;

            var userInfoProfile = await httpClient.GetUserInfoAsync(userInfo);

            ViewBag.userClaims = userInfoProfile.Claims;

            return View();
        }
    }
}