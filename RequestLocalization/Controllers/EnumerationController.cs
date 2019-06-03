using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RequestLocalization.Controllers
{
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class EnumerationController:ControllerBase
    {
        private readonly IStringLocalizer<GenderEnum> _genderLocalizer;

        public EnumerationController(IStringLocalizer<GenderEnum> genderLocalizer)
        {
            _genderLocalizer = genderLocalizer;
        }
        public IActionResult Genders()
        {
            Console.WriteLine($"Current Culture:{CultureInfo.CurrentCulture}");
            Console.WriteLine($"Current UI Culture:{CultureInfo.CurrentUICulture}");

            var selectList = new List<SelectItem>();
            var values = Enum.GetValues(typeof(GenderEnum));
            foreach(var value in values)
            {
                selectList.Add(new SelectItem
                {
                    Name = _genderLocalizer[value.ToString()],
                    Value = (int)value
                });
            }

            var feature = HttpContext.Features.Get<IRequestCultureFeature>();
            Console.WriteLine($"Culture:{feature.RequestCulture.Culture}");
            Console.WriteLine($"UI Culture:{feature.RequestCulture.UICulture}");
            Console.WriteLine($"Provider:{feature.Provider}");

            return Ok(selectList);
        }
    }
}
