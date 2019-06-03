using Resource.Resources;
using System;
using System.Globalization;

namespace Resource
{
    class Program
    {
        static void Main(string[] args)
        {
       
            Console.WriteLine(GenderEnum.Female.ToLocalizedString());
            CultureInfo.CurrentUICulture = new CultureInfo("de-DE");
            Console.WriteLine(GenderEnum.Female.ToLocalizedString());
        }
    }
}
