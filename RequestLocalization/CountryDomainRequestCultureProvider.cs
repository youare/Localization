using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestLocalization
{
    public class CountryDomainRequestCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            ProviderCultureResult result = null;
            var map = new Dictionary<string, string>()
                    {
                        { "en","en"},
                         { "de","de"}
                    };
            string domain = httpContext.Request.Host.Host.Split('.').Last();
            if (map.ContainsKey(domain))
            {
                result = new ProviderCultureResult(map[domain]);
            }
            return Task.FromResult(result);
        }
    }
}
