using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringLocalizer.CustomLocalization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly string _resourcesRelativePath;
        private readonly string _typeRelativeNamespace;
        private readonly CultureInfo _uiCulture;
        JObject _resourceCache;

        public JsonStringLocalizer(string resourcesRelativePath, string typeRelativeNamespace, CultureInfo uiCulture)
        {
            _resourcesRelativePath = resourcesRelativePath;
            _typeRelativeNamespace = typeRelativeNamespace;
            _uiCulture = uiCulture;
        }
        private JObject GetResource()
        {
            if (_resourceCache == null)
            {
                string tag = _uiCulture.Name;
                var typeRelativePath = _typeRelativeNamespace.Replace(".", "/");
                string filePath = $"{_resourcesRelativePath}{typeRelativePath}/{tag}.json";
                var json = File.Exists(filePath) ? File.ReadAllText(filePath) : "{}";
                _resourceCache = JObject.Parse(json);
            }
            return _resourceCache;
        }
        public LocalizedString this[string name] => this[name, null];

        public LocalizedString this[string name, params object[] arguments]
        {
            get {
                var resources = GetResource();
                var value = resources.Value<string>(name);
                var resourceNotFound = string.IsNullOrWhiteSpace(value);
                if (resourceNotFound)
                {
                    value = name;
                }
                else
                {
                    if(arguments != null)
                    {
                        value = string.Format(value, arguments);
                    }
                }
                return new LocalizedString(name, value, resourceNotFound);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var resources = GetResource();
            foreach(var pair in resources)
            {
                yield return new LocalizedString(pair.Key, pair.Value.Value<string>());
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonStringLocalizer(_resourcesRelativePath, _typeRelativeNamespace, culture);
        }
    }
}
