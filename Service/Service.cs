using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace Service
{
    public class Service : ServiceBase
    {
        protected Thread MainThread { get; private set; }

        protected System.Threading.Timer timer;

        Configurations Configurations;

        public Service(Configurations configurations)
        {
            Configurations = configurations;

            // Set the current directory (certain services needs to be have the active directory
            // set to the executable directory. A service in LOCALSYSTEM runs from System32)
            Directory.SetCurrentDirectory(Path.GetDirectoryName(this.GetType().Assembly.Location));

            ServiceName = Configurations.ServiceName;
            CanStop = true;
        }

        public Service(string serviceName)
        {
            Configurations = new Configurations();
            
            // Set the current directory (certain services needs to be have the active directory
            // set to the executable directory. A service in LOCALSYSTEM runs from System32)
            Directory.SetCurrentDirectory(Path.GetDirectoryName(this.GetType().Assembly.Location));

            ServiceName = serviceName;
            CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(Callback, null, Configurations.Polling * 1000, Timeout.Infinite);
        }

        protected override void OnStop()
        {
            if (MainThread != null)
            {
                MainThread.Join();
                MainThread = null;
            }
        }

        public void Callback(object state)
        {
            // Long running operation
            MainThread = new Thread(new ThreadStart(MainProcess));
            MainThread.Start();
        }

        public void MainProcess()
        {
            // do something 

            using (StreamWriter sw = new StreamWriter("file.txt", true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")} - 1");
            }

            timer.Change(Configurations.Polling * 1000, Timeout.Infinite);
        }

    }
}