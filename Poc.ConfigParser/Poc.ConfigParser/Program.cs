using System;
using System.IO;

namespace Poc.ConfigParser
{
    internal class Program
    {
        private static void Main()
        {
            var parser = new ConfigParser();
            var testFilePAth = $"{Directory.GetCurrentDirectory()}\\App1.config";

            var configDocument = parser.GetConfigDocument(testFilePAth);
            var appSettings = parser.GetAppSettings(configDocument);
            var connectionStrings = parser.GetConnectionSettings(configDocument);

            Console.WriteLine("Appsettings -----------------------------------");
            foreach (var appSetting in appSettings)
            {
                Console.WriteLine($"{appSetting.Key} : {appSetting.Value}");
            }

            Console.WriteLine("Connectionsettings -----------------------------");
            foreach (var connecionSetting in connectionStrings)
            {
                Console.WriteLine($"{connecionSetting.Key} : {connecionSetting.Value}");
            }

            var specialNode = parser.GetConfigSecion(configDocument);
            Console.WriteLine("Custom Section ----------------------------------");
            Console.WriteLine($"{specialNode.OuterXml}");

            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
