using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;

namespace Service
{
    class ProgramBase
    {
        private Service Service;

        public ProgramBase(Service service)
        {
            Service = service;
        }

        public void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                string arg = args[0].ToLowerInvariant();
                switch (arg)
                {
                    case "/i":  // Install
                        if (args.Length == 3)
                        {
                            ServiceInstaller.Account = ServiceAccount.User;
                            ServiceInstaller.Username = args[1];
                            ServiceInstaller.Password = args[2];
                        }

                        InstallService();
                        break;
                    case "/u":  // Uninstall
                        UninstallService();
                        break;
                    case "/s":  // Start
                        StartService();
                        break;
                    case "/h": // Halt
                        HaltService();
                        break;
                    case "/e": // Execute
                        Service.MainProcess();
                        System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                        break;
                    default:  // Not valid
                        Console.WriteLine("Argument not recognized: {0}", args[0]);
                        DisplayUsage();
                        break;
                }
            }
            else
            {
                if (Environment.UserInteractive)
                {
                    DisplayUsage();
                    return;
                }

                ServiceBase[] servicesToRun;
                servicesToRun = new ServiceBase[] { Service };
                ServiceBase.Run(servicesToRun);
            }
        }

        /// <summary>
        /// Displays the usage of the program when used interactively from the console.
        /// </summary>
        private void DisplayUsage()
        {
            string executableName = Path.GetFileName(Service.GetType().Assembly.Location);
            Console.WriteLine("Usage:");
            Console.WriteLine("{0} <option>", executableName);
            Console.WriteLine("<option> : /i \t Installs the service");
            Console.WriteLine("           /u \t Uninstalls the service");
            Console.WriteLine("           /s \t Starts the service");
            Console.WriteLine("           /h \t Stops the service");
            Console.WriteLine("           /e \t Runs the service in foreground");
        }

        /// <summary>
        /// Installs the service on the local machine.
        /// </summary>
        private int InstallService()
        {
            try
            {
                // Install the service with the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new string[] { Service.GetType().Assembly.Location });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.GetType() == typeof(Win32Exception))
                {
                    Win32Exception wex = (Win32Exception)ex.InnerException;
                    Console.WriteLine("Error(0x{0:X}): Service already installed.", wex.ErrorCode);
                    return wex.ErrorCode;
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Uninstalls the service on the local machine.
        /// </summary>
        private void UninstallService()
        {
            try
            {
                // Uninstall the service from the Windows Service Control Manager (SCM)
                ManagedInstallerClass.InstallHelper(new string[] { "/u", Service.GetType().Assembly.Location });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.GetType() == typeof(Win32Exception))
                {
                    Win32Exception wex = (Win32Exception)ex.InnerException;
                    Console.WriteLine(ex);
                    Console.WriteLine("Error(0x{0:X}).", wex.ErrorCode);
                }
                else
                {
                    Console.WriteLine(ex);
                    Exception inner = ex.InnerException;
                    while (inner != null)
                    {
                        Console.WriteLine("Inner: " + ex);
                        inner = inner.InnerException;
                    }
                }
            }
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        private void StartService()
        {
            try
            {
                var sc = new System.ServiceProcess.ServiceController(Service.ServiceName);
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        private void HaltService()
        {
            try
            {
                var sc = new System.ServiceProcess.ServiceController(Service.ServiceName);
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
