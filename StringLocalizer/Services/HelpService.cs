using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StringLocalizer.Services
{
    public interface IHelpService
    {
        string GetHelpFor(string serviceName);
    }
    public class HelpService: IHelpService
    {
        private readonly IStringLocalizerFactory _factory;

        public HelpService(IStringLocalizerFactory factory)
        {
            _factory = factory;
        }
        public string GetHelpFor(string serviceName)
        {
            var serviceClassName = $"{serviceName}Service";
            var type = Assembly.GetExecutingAssembly()
                .ExportedTypes
                .Where(x => x.Name.Equals(serviceClassName, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
            if(type == null)
            {
                return $"Help is not available for {serviceName}.";
            }
            var localizer = _factory.Create(type);
            var resources = localizer.GetAllStrings().Select(x => x.Name);
            return $"Available keys: {string.Join(',', resources)}";
        }
    }
}
