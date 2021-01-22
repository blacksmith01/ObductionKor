using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Translator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var Settings = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build().Get<AppSettings>();

            while (true)
            {
                Console.WriteLine("Write Commands...");
                var input = Console.ReadLine();
                if (input == null)
                    return;

                var commands = input.Split(" ");
                if (commands == null || commands.Length <= 0)
                    return;

                var api = new GoogleApi(Settings.Google.ProjectId, Settings.Google.LocationId, await File.ReadAllTextAsync("GoogleKey.json"));

                switch (commands[0])
                {
                    case "glossaries":
                        {
                            Console.WriteLine("[Result]");
                            foreach (var s in api.ListGlossaries())
                                Console.WriteLine(s);
                        }
                        break;

                    case "glossarydel":
                        {
                            if (commands.Length < 2)
                                return;

                            Console.WriteLine(api.DeleteGlossary(commands[1]) ? "Success" : "Failed");
                        }
                        break;

                    case "glossarynew":
                        {
                            if (commands.Length < 3)
                                return;

                            Console.WriteLine(api.CreateGlossary(commands[1], commands[2]) ?? "Failed");
                        }
                        break;

                    case "translate":
                        {
                            if (commands.Length < 3)
                                return;

                            var result = await api.TranslateText(commands[1], commands[2]);
                            Console.WriteLine(result);
                        }
                        break;

                    case "translatebat":
                        {
                            if (commands.Length < 4)
                                return;

                            Console.WriteLine(api.BatchTranslateText(commands[1], commands[2], commands[3]) ? "Success" : "Failed");
                        }
                        break;

                    default:
                        {

                        }
                        break;
                }
            }

        }
    }
}
