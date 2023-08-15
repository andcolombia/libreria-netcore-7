
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace MvcCode.Controllers
{
    public class LogoutController : Controller
    {
        private readonly IConfiguration _configuration;
        public LogoutController(IConfiguration _config)
        {
            _configuration = _config;
        }

        public async Task<IActionResult> FrontChannelLogout(string sid, string iss)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentSid = User.FindFirst("sid")?.Value ?? "";
                if (string.Equals(currentSid, sid, StringComparison.Ordinal))
                {
                    await this.HttpContext.SignOutAsync();
                }
            }

            return NoContent();
        }

        
    }
}
