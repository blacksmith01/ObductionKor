using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public static class Globals
    {
        public static AppSettings Settings { get; private set; }
        public static Serilog.Core.Logger Logger { get; private set; }
        public static string ExeFilePath { get; private set; }
        public static bool Initialize()
        {
            try
            {
                ExeFilePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                Settings = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build().Get<AppSettings>();

                Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .CreateLogger();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
        public static async Task UpdateAppSettings()
        {
            await File.WriteAllTextAsync(Path.Combine(ExeFilePath, "appsettings.json"), JsonConvert.SerializeObject(Settings, Formatting.Indented));
        }
    }
}
