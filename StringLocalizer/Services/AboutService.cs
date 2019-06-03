using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringLocalizer.Services
{
    public interface IAboutService
    {
        string Reply(string searchTerm);
    }
    public class AboutService: IAboutService
    {
        IStringLocalizer _localizer;
        public AboutService(IStringLocalizer<AboutService> localizer)
        {
            _localizer = localizer;
        }

        public string Reply(string searchTerm)
        {
            var resource = _localizer[searchTerm];
            if (resource.ResourceNotFound)
            {
                return _localizer["help"];
            }
            return resource;
        }
    }
}
