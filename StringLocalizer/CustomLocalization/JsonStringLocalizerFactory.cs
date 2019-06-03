using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StringLocalizer.CustomLocalization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourceRelativePath;
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> options)
        {
            _resourceRelativePath = options.Value?.ResourcesPath ?? string.Empty;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            var typeInfo = resourceSource.GetTypeInfo();
            var assemblyName = new AssemblyName(typeInfo.Assembly.FullName);
            var baseNamespace = assemblyName.Name;
            var typeRelativeNamespace = typeInfo.FullName.Substring(baseNamespace.Length);
            return new JsonStringLocalizer(_resourceRelativePath, typeRelativeNamespace, CultureInfo.CurrentUICulture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
