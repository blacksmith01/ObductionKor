using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public class AppSettings
    {
        public class GoogleApi
        {
            public string ProjectId { get; set; }
            public string LocationId { get; set; }
            public string JsonKeyFilePath { get; set; }
        }

        public GoogleApi Google { get; set; }
    }
}
