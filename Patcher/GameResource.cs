using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class GameResource
    {
        public static readonly string[] DescriptionFileNames = new string[]{
                "AmbassadorSeed",
                "ArrivalInventory",
                "BarnacleProject",
                "BatteryLog",
                "Bleeder",
                "Bleeder_b",
                "Blob",
                "blob2",
                "Board",
                "CarolineLetter",
                "Communication",
                "CWJournal",
                "Drink1",
                "Drink2",
                "Drink3",
                "EECodeTest",
                "FarleyVaultJournal",
                "GuestBook",
                "HunrathHistoryScrap1",
                "HunrathHistoryScrap1b",
                "Lockdown",
                "Manifest",
                "MayorImager",
                "MayorsLog",
                "Menu",
                "MofangDisabler",
                "MofangProjector",
                "RacesHistory",
                "RailMap",
                "SalvagePartsLog",
                "SeedInformation",
                "TheGauntlet",
                "ThePlan",
                "VaultClue",
                "WMDWarning1",
                "WMDWarning1_b",
                "wmdwarning2",
            };
        public static readonly string[] SubtitleFileNames = new string[]{
                "CW_180A",
                "CW_180B",
                "CW_180C",
                "CW_180D",
                "CW_180E",
                "CW_BleederA",
                "CW_BleederB",
                "CW_BleederC",
                "CW_Connect1A",
                "CW_Connect1B",
                "CW_Connect1C",
                "CW_Connect2A",
                "CW_Connect2B",
                "CW_Connect2C",
                "CW_Final1A",
                "CW_Final1B",
                "CW_FinalDia",
                "CW_FinalEar",
                "CW_PowerA",
                "CW_PowerB",
                "CW_PowerC",
                "CW_PowerD",
                "FY_VilleinMessage",
                "FarleyImagerTest_Mono",
                "FarleyTapeIntro",
                "Farley_Audio_Journal",
                "HR_Town_New",
                "HR_Tree_New",
                "HR_Water_New",
                "HR_Welcome_New",
                "KioskFarley",
                "IM_EX3",
                "IM_HU1",
                "IM_HU2",
                "IM_HU3",
                "IM_HU4",
                "IM_Intro",
                "Intro1_A",
                "Intro1_B",
                "Intro1_C",
                "Intro2_A",
                "Intro2_B",
                "Intro2_C",
                "Intro2_D",
                "Intro2_E",
                "Intro2_F",
                "Intro2_G",
                "Intro2_H",
                "Intro2_I",
                "Intro3_A",
                "Intro3_B",
                "Intro3_C",
                "Intro4_A",
                "Intro4_B",
                "Intro4_C",
            };
        public abstract class ResourceBase
        {
            public string FilePath { get; set; }
            public string FileName { get { return Path.GetFileNameWithoutExtension(FilePath); } }
            public abstract Dictionary<int, string> GetTextMap();
            public abstract void UpdateText(int index, string text);
            public abstract Task Export(string outputPath);
        }

        public class Description : ResourceBase
        {
            public class Page
            {
                public List<PageElement> PageElements { get; set; } = new List<PageElement>();
            }
            public class PageElement
            {
                public string Text { get; set; }
            }

            public List<Page> Pages { get; set; } = new List<Page>();

            public async static Task<ResourceBase> Create(string filePath)
            {
                var res = new Description { FilePath = filePath };
                JObject jobj = await JsonEx.LoadJObject(filePath);
                IList<JToken> jtokens = jobj["Pages"].Children().ToList();
                res.Pages.AddRange(jtokens.Select(token => token.ToObject<Description.Page>()));
                return res;
            }
            public override Dictionary<int, string> GetTextMap()
            {
                Dictionary<int, string> map = new();
                int index = 0;
                foreach (var p in Pages)
                {
                    foreach (var pe in p.PageElements)
                    {
                        map[index] = pe.Text;
                        index += 1;
                    }
                }
                return map;
            }

            public override void UpdateText(int index, string text)
            {
                int i = 0;
                foreach (var p in Pages)
                {
                    foreach (var pe in p.PageElements)
                    {
                        if (i == index)
                        {
                            pe.Text = text;
                            return;
                        }
                        i += 1;
                    }
                }
            }

            public async override Task Export(string outputPath)
            {
                JObject jobj = await JsonEx.LoadJObject(FilePath);

                int pcount = 0;
                foreach (var ptoken in jobj["Pages"].Children())
                {
                    int pecount = 0;
                    foreach (var petoken in ptoken["PageElements"].Children())
                    {
                        petoken["Text"] = Pages[pcount].PageElements[pecount].Text;
                        pecount++;
                    }
                    pcount++;
                }
                await jobj.WriteAsync(outputPath);
            }
        }

        public class Subtitle : ResourceBase
        {
            public class Line
            {
                public string Text { get; set; }
                public string Time { get; set; }
            }

            public List<Line> Lines { get; set; } = new List<Line>();

            public async static Task<ResourceBase> Create(string filePath)
            {
                var res = new Subtitle { FilePath = filePath };
                JObject jobj = await JsonEx.LoadJObject(filePath);
                IList<JToken> jtokens = jobj["Lines"].Children().ToList();
                res.Lines.AddRange(jtokens.Select(token => token.ToObject<Subtitle.Line>()));
                return res;
            }
            public override Dictionary<int, string> GetTextMap()
            {
                Dictionary<int, string> map = new();
                int index = 0;
                foreach (var line in Lines)
                {
                    map[index] = line.Text;
                    index += 1;
                }
                return map;
            }

            public override void UpdateText(int index, string text)
            {
                Lines[index].Text = text;
            }

            public async override Task Export(string outputPath)
            {
                JObject jobj = await JsonEx.LoadJObject(FilePath);
                IList<JToken> jtokens = jobj["Lines"].Children().ToList();
                int count = jtokens.Count;
                for (int i = 0; i < count; i++)
                {
                    jtokens[i]["Text"] = Lines[i].Text;
                }
                await jobj.WriteAsync(outputPath);
            }
        }

        public List<ResourceBase> Resources { get; set; } = new List<ResourceBase>();
        public Dictionary<string, Dictionary<int, string>> TextMap = new();

        public async Task<bool> Load(string dirPath)
        {
            int add_count = 0;
            foreach (var file in Directory.GetFiles(dirPath, "*.json"))
            {
                var filename = Path.GetFileNameWithoutExtension(file);

                if (Resources.Find(x => x.FilePath == file) != null)
                    continue;

                ResourceBase res = null;
                if (SubtitleFileNames.Contains(filename))
                {
                    res = await Subtitle.Create(file);
                }
                else if (DescriptionFileNames.Contains(filename))
                {
                    res = await Description.Create(file);
                }
                else
                {
                    continue;
                }

                Resources.Add(res);
                TextMap[Path.GetFileNameWithoutExtension(res.FilePath)] = res.GetTextMap();
                add_count++;
            }

            return add_count > 0;
        }

        public static string CreateValue(string key, string value)
        {
            return (value.FirstOrDefault() == '/' || (key.Length > 8 && string.Compare(key, 0, "Manifest", 0, 8) == 0)) ? value : "";
        }
    }
}
