using System.Configuration;

namespace Service
{
    partial class ServiceInstaller
    {
        public static System.ServiceProcess.ServiceAccount Account = System.ServiceProcess.ServiceAccount.LocalSystem;
        public static string Username = null;
        public static string Password = null;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Account = Account;
            this.serviceProcessInstaller.Password = Password;
            this.serviceProcessInstaller.Username = Username;
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            this.serviceInstaller.ServiceName = ConfigurationManager.AppSettings["ServiceName"];
            this.serviceInstaller.Description = ConfigurationManager.AppSettings["DescriptionService"];
            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.serviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
    }
}