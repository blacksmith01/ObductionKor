using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class Constants
    {
        public const string JsonEnName = "Translate.en.json";
        public const string JsonKoName = "Translate.ko.json";

        public const string HtmlEnName = "Translate.en.html";
        public const string HtmlKoName = "Translate.ko.html";

        public const string TxtZipEnName = "Translate.en.txt.zip";
        public const string TxtZipKoName = "Translate.ko.txt.zip";

        public static string EnLocalizationPath(string resBasePath)
        {
            return Path.Combine(resBasePath, "Obduction", "Content", "Localization", "en");
        }
    }
}
