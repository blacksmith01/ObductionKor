using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class HtmlSerializer
    {
        public static string Serialize(Dictionary<string, Dictionary<int, string>> txtMap)
        {
            var sb = new StringBuilder();
            sb.Append("<html><body>");
            int ip = 0;
            foreach (var kv in txtMap)
            {
                sb.Append($"<p i=\"{ip++}\" name=\"{kv.Key}\" child=\"{kv.Value.Count}\">");
                int itext = 0;
                foreach (var kv2 in kv.Value)
                {
                    sb.Append($"<span i=\"{itext++}\">");
                    if (kv2.Value.EndsWith(".png"))
                    {
                        sb.Append($"<img src=\"{kv2.Value}\" />");
                    }
                    else
                    {
                        // Special case for ignore \r\n when translating
                        if (kv.Key == "Board")
                        {
                            var sps = kv2.Value.Split("\r\n");
                            for (int ichunk = 0; ichunk < sps.Length; ichunk++)
                                sb.Append($"<span j=\"{ichunk}\">{sps[ichunk]}</span>");
                        }
                        else
                        {
                            sb.Append(kv2.Value);
                        }
                    }
                    sb.Append("</span>");
                }
                sb.Append("</p>");
            }
            sb.Replace("\r\n", "<br />");
            sb.Replace("\t", "<pre> </pre>");
            sb.Append("</body></html>");
            return sb.ToString();
        }

        public static Dictionary<string, Dictionary<int, string>> Deserialize(string html)
        {
            var dic = new Dictionary<string, Dictionary<int, string>>();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var nodeBody = doc.DocumentNode.FindChildRecursive(x => string.Compare(x.Name, "body", true) == 0);
            if (nodeBody == null)
                return dic;

            var textSpanIds = new Dictionary<int, int>(50);
            var chunkSpanIds = new List<int>(20);
            nodeBody.ForeachChildren(nodeP =>
            {
                if (string.Compare(nodeP.Name, "p", true) != 0 || !nodeP.HasAttributes)
                    return;

                var filename = nodeP.Attributes["name"].Value;
                var texts = new Dictionary<int, string>();
                if (!dic.TryAdd(filename, texts))
                    return;

                textSpanIds.Clear();
                nodeP.ForeachChildren(nodeText =>
                {
                    if (string.Compare(nodeText.Name, "span", true) != 0)
                        return;

                    var textid = int.Parse(nodeText.Attributes["i"].Value);

                    var idx = texts.Count;
                    if (nodeText.FirstChild != null && string.Compare(nodeText.FirstChild.Name, "img", true) == 0)
                    {
                        texts.Add(idx, nodeText.FirstChild.Attributes["src"].Value);
                    }
                    else
                    {
                        var text = "";
                        // Special case for ignore \r\n when translating
                        if (filename == "Board")
                        {
                            chunkSpanIds.Clear();
                            nodeText.ForeachChildren(nodeChunk =>
                            {
                                if (string.Compare(nodeChunk.Name, "span", true) != 0)
                                    return;

                                var chunkid = int.Parse(nodeChunk.Attributes["j"].Value);
                                if (chunkSpanIds.Contains(chunkid))
                                {
                                    text += " " + nodeChunk.InnerHtml;
                                }
                                else
                                {
                                    text += (chunkSpanIds.Any() ? "\r\n" : "") + nodeChunk.InnerHtml;
                                    chunkSpanIds.Add(chunkid);
                                }
                            });
                        }
                        else
                        {
                            text = InnerHtmlFinalize(nodeText);
                        }

                        text = text.Replace("<br>", "\r\n");
                        text = text.Replace("<br />", "\r\n");
                        text = text.Replace("<pre> </pre>", "\t");

                        if (textSpanIds.ContainsKey(textid))
                        {
                            texts[idx - 1] = texts[idx - 1] + " " + text;
                        }
                        else
                        {
                            texts.Add(idx, text);
                            textSpanIds.Add(textid, idx);
                        }
                    }
                });
            });

            return dic;
        }


        public static string InnerHtmlFinalize(HtmlNode node)
        {
            if (node.ChildNodes.Any() && !IsLastElementNode(node))
            {
                var sr = new StringBuilder();
                RecursiveInnerText(node, sr);
                return sr.ToString();
            }
            else
            {
                return node.InnerHtml;
            }
        }

        public static void RecursiveInnerText(HtmlNode node, StringBuilder sb)
        {
            if (node.NodeType == HtmlNodeType.Text)
                sb.Append(node.InnerText);

            foreach (var n in node.ChildNodes)
            {
                if (n.NodeType == HtmlNodeType.Text)
                    sb.Append(" ");

                RecursiveInnerText(n, sb);
            }
        }

        public static bool IsLastElementNode(HtmlNode node)
        {
            if (node.NodeType != HtmlNodeType.Element)
                return false;

            foreach (var n in node.ChildNodes)
            {
                if (n.NodeType == HtmlNodeType.Element)
                    return false;
            }

            return true;
        }
    }

    public class TxtZipSerialier
    {
        public static byte[] Serialize(Dictionary<string, Dictionary<int, string>> txtMap)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var kv in txtMap)
                    {
                        var entry = archive.CreateEntry($"{kv.Key}.txt");

                        using (var entryStream = entry.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            foreach (var kv2 in kv.Value)
                            {
                                streamWriter.WriteLine(kv2.Value);
                            }
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }

        public static Dictionary<string, Dictionary<int, string>> Deserialize(byte[] bytes)
        {
            var dic = new Dictionary<string, Dictionary<int, string>>();

            using (var stream = new MemoryStream(bytes))
            using (var archive = new ZipArchive(stream))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        var texts = new Dictionary<int, string>();
                        if (!dic.TryAdd(entry.Name, texts))
                            continue;

                        using (var entryStream = entry.Open())
                        using (var reader = new StreamReader(entryStream))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                texts.Add(texts.Count, line);
                            }
                        }
                    }
                }
            }

            return dic;
        }
    }
}
