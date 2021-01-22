using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class BackgroundProcess
    {
        public static async ValueTask<bool> Initialize()
        {
            var enLocalResPath = Constants.EnLocalizationPath(Globals.Settings.ResourceDirectory);

            var ResEn = new GameResource();
            await ResEn.Load(enLocalResPath);

            var WorkFileKoPath = Path.Combine(Globals.Settings.WorkingDirectory, Constants.JsonKoName);
            await Task.Run(() =>
            {
                var jsonEnFilePath = Path.Combine(Globals.Settings.WorkingDirectory, Constants.JsonEnName);
                if (!File.Exists(jsonEnFilePath))
                {
                    File.WriteAllText(jsonEnFilePath, JsonConvert.SerializeObject(ResEn.TextMap, Formatting.Indented));
                }
                if (!File.Exists(WorkFileKoPath))
                {
                    var Translated = ResEn.TextMap.ToDictionary(x => x.Key, x => 
                    {
                        if (string.Compare(x.Key, "Manifest") == 0)
                            return new(x.Value);
                        else
                            return x.Value.ToDictionary(x => x.Key, x => x.Value.EndsWith(".png") ? x.Value : "");
                    });

                    File.WriteAllTextAsync(WorkFileKoPath, JsonConvert.SerializeObject(Translated, Formatting.Indented));
                }

                var htmlEnFilePath = Path.Combine(Globals.Settings.WorkingDirectory, Constants.HtmlEnName);
                if (!File.Exists(htmlEnFilePath))
                {
                    File.WriteAllText(htmlEnFilePath, HtmlSerializer.Serialize(ResEn.TextMap));
                }

                var txtZipEnFilePath = Path.Combine(Globals.Settings.WorkingDirectory, Constants.TxtZipEnName);
                if (!File.Exists(txtZipEnFilePath))
                {
                    File.WriteAllBytes(txtZipEnFilePath, TxtZipSerialier.Serialize(ResEn.TextMap));
                }
            });

            await Globals.UpdateAppSettings();

            return true;
        }

        public static async ValueTask<bool> PatchTextJson()
        {
            var enLocalResPath = Constants.EnLocalizationPath(Globals.Settings.ResourceDirectory);
            var WorkFileKoPath = Path.Combine(Globals.Settings.WorkingDirectory, Constants.JsonKoName);
            if (!File.Exists(WorkFileKoPath))
                throw new Exception($"Not Found Translated File.({Constants.JsonKoName})");

            var GRes = new GameResource();
            await GRes.Load(enLocalResPath);

            var translateMap = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, string>>>(File.ReadAllText(WorkFileKoPath));
            foreach (var kvFile in translateMap)
            {
                var fileName = kvFile.Key;

                var resource = GRes.Resources.Where(r => Path.GetFileNameWithoutExtension(r.FilePath) == fileName).FirstOrDefault();
                if (resource == null)
                    continue;

                foreach (var kvLine in kvFile.Value)
                {
                    resource.UpdateText(kvLine.Key, kvLine.Value);
                }
            }

            var krResDirPath = Path.Combine(Globals.Settings.WorkingDirectory, "PatchedText");
            if (!Directory.Exists(krResDirPath))
                Directory.CreateDirectory(krResDirPath);

            foreach (var pair in GRes.Resources)
            {
                await pair.Export(Path.Combine(krResDirPath, Path.GetFileName(pair.FilePath)));
            }

            await PatchImage(GRes);

            await Globals.UpdateAppSettings();

            return true;
        }

        public static async ValueTask<bool> PatchTextHtml()
        {
            var enLocalResPath = Constants.EnLocalizationPath(Globals.Settings.ResourceDirectory);
            var WorkFileKoPath = Path.Combine(Globals.Settings.WorkingDirectory, Constants.HtmlKoName);
            if (!File.Exists(WorkFileKoPath))
                throw new Exception($"Not Found Translated File.({Constants.HtmlKoName})");

            var GRes = new GameResource();
            await GRes.Load(enLocalResPath);

            var translateMap = HtmlSerializer.Deserialize(File.ReadAllText(WorkFileKoPath));
            foreach (var kvFile in translateMap)
            {
                var fileName = kvFile.Key;

                var resource = GRes.Resources.Where(r => Path.GetFileNameWithoutExtension(r.FilePath) == fileName).FirstOrDefault();
                if (resource == null)
                    continue;

                foreach (var kvLine in kvFile.Value)
                {
                    resource.UpdateText(kvLine.Key, kvLine.Value);
                }
            }

            var krResDirPath = Path.Combine(Globals.Settings.WorkingDirectory, "PatchedText");
            if (!Directory.Exists(krResDirPath))
                Directory.CreateDirectory(krResDirPath);

            foreach (var pair in GRes.Resources)
            {
                await pair.Export(Path.Combine(krResDirPath, Path.GetFileName(pair.FilePath)));
            }

            await PatchImage(GRes);

            await Globals.UpdateAppSettings();

            return true;
        }

        public static async ValueTask<bool> PatchTextTxtZip()
        {
            var enLocalResPath = Constants.EnLocalizationPath(Globals.Settings.ResourceDirectory);
            var WorkFileKoPath = Path.Combine(Globals.Settings.WorkingDirectory, Constants.TxtZipKoName);
            if (!File.Exists(WorkFileKoPath))
                throw new Exception($"Not Found Translated File.({Constants.TxtZipKoName})");

            var GRes = new GameResource();
            await GRes.Load(enLocalResPath);

            var translateMap = TxtZipSerialier.Deserialize(File.ReadAllBytes(WorkFileKoPath));
            foreach (var kvFile in translateMap)
            {
                var fileName = kvFile.Key;

                var resource = GRes.Resources.Where(r => Path.GetFileNameWithoutExtension(r.FilePath) == fileName).FirstOrDefault();
                if (resource == null)
                    continue;

                foreach (var kvLine in kvFile.Value)
                {
                    resource.UpdateText(kvLine.Key, kvLine.Value);
                }
            }

            var krResDirPath = Path.Combine(Globals.Settings.WorkingDirectory, "PatchedText");
            if (!Directory.Exists(krResDirPath))
                Directory.CreateDirectory(krResDirPath);

            foreach (var pair in GRes.Resources)
            {
                await pair.Export(Path.Combine(krResDirPath, Path.GetFileName(pair.FilePath)));
            }

            await Globals.UpdateAppSettings();

            return true;
        }

        public static async ValueTask<bool> PatchImage(GameResource localdataKr)
        {
            var ignoreFileNames = new string[] {
                "Menu",
                "Manifest",
                "WMDWarning1",
                "wmdwarning2",
            };

            var krResDirPath = Path.Combine(Globals.Settings.WorkingDirectory, "PatchedImage");
            if (!Directory.Exists(krResDirPath))
                Directory.CreateDirectory(krResDirPath);

            using (var bgBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
            using (var arialFont = new Font("Noto Sans KR Medium", 16))
            using (var strformat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near })
            {
                foreach (var res in localdataKr.Resources.Where(n => n is GameResource.Description && !ignoreFileNames.Contains(n.FileName)).OfType<GameResource.Description>())
                {
                    var JsonFileName = res.FileName;
                    var isBook = JsonFileName != "Board";

                    for (int ipage = 0; ipage < res.Pages.Count; ipage++)
                    {
                        var page = res.Pages[ipage];
                        string imgFileName = JsonFileName + "-" + (isBook ? (ipage - (ipage % 2)) : ipage).ToString();
                        string srcImgFilePath = Path.Combine(Globals.Settings.ImageSourceDirectory, imgFileName + ".tga");
                        string dstImgFilePath = Path.Combine(krResDirPath, imgFileName + ".tga");
                        if (!File.Exists(srcImgFilePath))
                            continue;

                        string currText = "";
                        foreach (var pe in page.PageElements)
                        {
                            var firstValue = pe.Text.FirstOrDefault();
                            if (firstValue == '/')
                            {
                                continue;
                            }
                            if (firstValue == '*' || firstValue == '-')
                            {
                                if (currText.Length > 0 && currText.Last() != '\n')
                                {
                                    currText += "\r\n";
                                }
                            }
                            currText += pe.Text;
                        }

                        var tga = new TGASharpLib.TGA((!isBook || (ipage % 2) == 0) ? srcImgFilePath : dstImgFilePath);
                        using (var bitmap = (Bitmap)tga)
                        using (var graphics = Graphics.FromImage(bitmap))
                        {
                            var rect = new RectangleF(0, 0, bitmap.Width, bitmap.Height);
                            if (!isBook || (ipage % 2) == 0)
                            {
                                graphics.FillRectangle(bgBrush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                            }
                            if (isBook)
                            {
                                rect.Width = bitmap.Width / 2;
                                if ((ipage % 2) == 1)
                                {
                                    rect.X = rect.Width;
                                }
                            }
                            rect.X += 5;
                            rect.Y += 25;
                            rect.Width -= 5;
                            rect.Height -= 5;

                            graphics.DrawString(currText, arialFont, Brushes.White, rect, strformat);
                            graphics.Flush();

                            ((TGASharpLib.TGA)bitmap).Save(dstImgFilePath);
                        }
                    }
                }
            }

            return true;
        }
    }
}
