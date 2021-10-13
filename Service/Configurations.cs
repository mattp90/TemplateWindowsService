using System.Configuration;

namespace Service
{
    public class Configurations
    {
        public readonly int Polling = int.Parse(ConfigurationManager.AppSettings["Polling"]);
        public readonly string ServiceName = ConfigurationManager.AppSettings["ServiceName"];
        public readonly string DisplayName = ConfigurationManager.AppSettings["DisplayName"];
        public readonly string ServiceDescription = ConfigurationManager.AppSettings["ServiceDescription"];
    }
}
