using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;

namespace Service
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        public ServiceInstaller()
        {
            InitializeComponent();

            this.serviceInstaller.DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            this.serviceInstaller.ServiceName = ConfigurationManager.AppSettings["ServiceName"];
            this.serviceInstaller.Description = ConfigurationManager.AppSettings["ServiceDescription"];
        }
    }
}
