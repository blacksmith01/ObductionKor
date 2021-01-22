using HtmlAgilityPack;
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
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
    }

    public static class JsonEx
    {
        public static async ValueTask<JObject> LoadJObject(string FilePath)
        {
            using (var sr = new StringReader(await File.ReadAllTextAsync(FilePath)))
            using (var reader = new JsonTextReader(sr))
            {
                reader.FloatParseHandling = FloatParseHandling.Decimal;
                return JObject.Load(reader);
            }
        }

        public static async Task WriteAsync(this JObject jobj, string outputPath)
        {
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            using (var writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;
                await jobj.WriteToAsync(writer);
            }

            await File.WriteAllTextAsync(outputPath, sb.ToString(), Encoding.Unicode);
        }
    }

    public static class HtmlEx
    {
        public static void ForeachChildren(this HtmlNode node, Action<HtmlNode> action)
        {
            var child = node.FirstChild;
            while (child != null)
            {
                action(child);
                child = child.NextSibling;
            }
        }
        public static HtmlNode FindChild(this HtmlNode node, Func<HtmlNode, bool> conditaion)
        {
            var child = node.FirstChild;
            while (child != null)
            {
                if(conditaion(child))
                    return child;
                child = child.NextSibling;
            }
            return null;
        }
        public static HtmlNode FindChildRecursive(this HtmlNode node, Func<HtmlNode, bool> conditaion)
        {
            var child = node.FirstChild;
            while (child != null)
            {
                if (conditaion(child))
                    return child;

                var child2 = child.FindChildRecursive(conditaion);
                if (child2 != null)
                    return child2;

                child = child.NextSibling;
            }
            return null;
        }
    }
}
