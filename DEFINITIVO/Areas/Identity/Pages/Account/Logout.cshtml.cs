using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace DEFINITIVO.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IMemoryCache _memoryCache;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, IMemoryCache memoryCache)
        {
            _signInManager = signInManager;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            _memoryCache.Remove("ProductosForIndex2");
            _memoryCache.Remove("Categorias");
            if (returnUrl == "usuario-saliendo")
            {
                return LocalRedirect("/Identity/Account/Logout");
            }
            else
            {
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return LocalRedirect("/Productos/Index2");
                }
            }
        }
    }
}
