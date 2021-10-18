using Microsoft.Extensions.Logging;
using System;

namespace ServiceDotnet5
{
    public class Service
    {
        private readonly ILogger<Service> Logger;

        protected AppConfigurations Configurations { get; set; }
        
        public Service()
        { }

        public Service(ILogger<Service> logger, AppConfigurations configurations)
        {
            Logger = logger;
            Configurations = configurations;
        }

        public void Main()
        {
            // Get all zip files from sftp folder 
            try
            {
                MainMethod();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"\tError in Main Method");
            }
        }

        public void MainMethod()
        {
            // To implement
        }
    }
}
