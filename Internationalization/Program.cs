using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Internationalization
{
    class Program
    {
        static void Main(string[] args)
        {
            //This comes from the operating system...
            DisplayCurrentCulture();
            DateTimeDisplayDemo();
            DateTimeParseDemo();
            NumberParsingDemo();
            Console.WriteLine();
            CultureInfo de = new CultureInfo("de-DE");
            CultureInfo.CurrentCulture = de;
            DisplayCurrentCulture();
            DateTimeDisplayDemo();
            DateTimeParseDemo();
            NumberParsingDemo();
            Console.WriteLine();

       
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            TaskFactory tf = new TaskFactory();
            tf.StartNew(BackgroundTask);
            Task.Delay(3000).Wait();
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            ThreadInfo();
            Task.Delay(3000).Wait();
            Console.WriteLine();
            Console.WriteLine();



            //specific culture => neutral culture => Invariant culture
            CultureInfo.CurrentCulture = new CultureInfo("zh-Hant-HK");
            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine($"Current culture:{CultureInfo.CurrentCulture}");
                Console.WriteLine($"Is neutral:{CultureInfo.CurrentCulture.IsNeutralCulture}");
                Console.WriteLine($"Parent culture:{CultureInfo.CurrentCulture.Parent}");
                Console.WriteLine();
                CultureInfo.CurrentCulture = CultureInfo.CurrentCulture.Parent;
            }

        }
        static void DisplayCurrentCulture()
        {
            Console.WriteLine(CultureInfo.CurrentCulture.Name);
            Console.WriteLine(CultureInfo.CurrentCulture.DisplayName);
        }
        static void DateTimeDisplayDemo()
        {
            Console.WriteLine(DateTime.Now);
        }
        static void DateTimeParseDemo()
        {
            string dateString = "2.12.2016";
            DateTime date = DateTime.Parse(dateString);
            Console.WriteLine(date.ToString("D"));
            // cultureinfo implements IFormatProvider
            DateTime date1 = DateTime.Parse(dateString, new CultureInfo("de-DE"));
            Console.WriteLine(date1.ToString("D"));
        }
        static void NumberParsingDemo()
        {
            string numberAsString = "1.500";
            decimal number = decimal.Parse(numberAsString) + 1;
            Console.WriteLine(number);
            var numberAsString2 = "1,500";
            decimal number2 = decimal.Parse(numberAsString2) + 1;
            Console.WriteLine(number2);
        }

        static void ThreadInfo()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} : {CultureInfo.CurrentCulture.DisplayName}");
        }

        static void BackgroundTask()
        {
            while (true)
            {
                ThreadInfo();
                Task.Delay(1000).Wait();
            }
        }
    }
}
