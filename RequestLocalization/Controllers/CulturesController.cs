using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestLocalization.Controllers
{
    public class CulturesController:ControllerBase
    {
        public IActionResult Set(string uiCulture)
        {
            var requestCulture = new RequestCulture(uiCulture);
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, cookieValue);
            return Ok();
        }
    }
}
