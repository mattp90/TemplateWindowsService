using Microsoft.Extensions.Configuration;
using System.IO;

namespace ServiceDotnet5
{
    public class AppConfigurations
    {
        public string SFTPAdress { get; set; }
        public string SFTPPort { get; set; }
        public string SFTPUsername { get; set; }
        public string SFTPPassword { get; set; }
        public string InputDirectory { get; set; }
        public string ProcessDirectory { get; set; }
        public string OutgoingDirectory { get; set; }
        public string ErrorDirectory { get; set; }
        public string ServiceName { get; set; }
        public string Polling { get; set; }
        public string PriipsDataFileXsd { get; set; }
    }
}
