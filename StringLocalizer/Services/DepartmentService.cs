using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringLocalizer.Services
{
    public interface IDepartmentService {
        string GetInfo(string name);
    }
    public class DepartmentService: IDepartmentService
    {
        private readonly IStringLocalizer _localizer;

        public DepartmentService(IStringLocalizer<DepartmentService> localizer)
        {
            _localizer = localizer;
        }

        public string GetInfo(string name)
        {
            var value = _localizer[name];
            if (value.ResourceNotFound)
            {
                return _localizer["help"];
            }
            return value;
        }
    }
}
