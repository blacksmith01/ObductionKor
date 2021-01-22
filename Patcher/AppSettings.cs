using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public class AppSettings
    {
        public string ResourceDirectory { get; set; }
        public string ImageSourceDirectory { get; set; }
        public string WorkingDirectory { get; set; }
        public string RenderFont { get; set; }
        public int RenderFontSize { get; set; }
    }
}
