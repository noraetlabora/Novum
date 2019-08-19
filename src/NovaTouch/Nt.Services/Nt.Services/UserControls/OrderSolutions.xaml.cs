using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nt.Services.UserControls
{
    /// <summary>
    /// Interaction logic for OrderSolutions.xaml
    /// </summary>
    public partial class OrderSolutions : UserControl
    {
        private const string serviceName = "Novacom.OrderSolutions";

        public OrderSolutions()
        {
            InitializeComponent();
            serviceStatus_Tick(null, null);
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(serviceStatus_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void serviceStatus_Tick(object sender, EventArgs e)
        {            
            var serviceStatus = Service.GetStatus(serviceName);

            switch (serviceStatus)
            {
                case Service.Status.Uninstalled:
                    lblServiceStatus.Content = "Service uninstalled";
                    btnServiceInstall.IsEnabled = true;
                    btnServiceInstall.Content = "Install Service";
                    btnServiceRun.Content = "Run Service";
                    btnServiceRun.IsEnabled = false;
                    EnableSettings();
                    break;
                case Service.Status.Stopped:
                    lblServiceStatus.Content = "stopped";
                    btnServiceInstall.IsEnabled = true;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = true;
                    btnServiceRun.Content = "Run Service";
                    EnableSettings();
                    break;
                case Service.Status.StopPending:
                    lblServiceStatus.Content = "stop pending";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = false;
                    btnServiceRun.Content = "Stop Service";
                    DisableSettings();
                    break;
                case Service.Status.StartPending:
                    lblServiceStatus.Content = "start pending";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = false;
                    btnServiceRun.Content = "Run Service";
                    DisableSettings();
                    break;
                case Service.Status.Paused:
                    lblServiceStatus.Content = "paused";
                    btnServiceInstall.IsEnabled = true;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = true;
                    btnServiceRun.Content = "Run Service";
                    DisableSettings();
                    break;
                case Service.Status.Running:
                    lblServiceStatus.Content = "running";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = true;
                    btnServiceRun.Content = "Stop Service";
                    DisableSettings();
                    break;
                default:
                    lblServiceStatus.Content = "unknown";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = false;
                    btnServiceRun.Content = "Run Service";
                    EnableSettings();
                    break;
            }
        }

        private void BtnServiceRun_Click(object sender, RoutedEventArgs e)
        {
            var serviceStatus = Service.GetStatus(serviceName);
            if (serviceStatus.Equals(Service.Status.Stopped))
            {
                DisableSettings();
                var args = new string[] { "--dbIp", txtDbIp.Text, "--dbPrt", txtDbPort.Text, "--dbNs", txtDbNamespace.Text, "--dbUsr", txtDbUser.Text, "--dbPwd", txtDbPassword.Password, "--osIp", txtServerPort.Text, "--osSPrt", txtServerPort.Text, "--osCPrt", txtClientPort.Text };
                Service.Start(serviceName, args);
            }
            else
            {
                Service.Stop(serviceName);
            }
            serviceStatus_Tick(null, null);
        }

        private void BtnServiceInstall_Click(object sender, RoutedEventArgs e)
        {
            var serviceStatus = Service.GetStatus(serviceName);
            if (serviceStatus.Equals(Service.Status.Uninstalled))
                Service.Install(serviceName, @"C:\Users\nrastl\Documents\Novum\src\OrderSolution\Os.Server\bin\Debug\netcoreapp3.0\Os.Server.exe");
            else
                Service.Uninstall(serviceName);

        }

        private void EnableSettings()
        {
            txtDbIp.IsEnabled = true;
            txtDbPort.IsEnabled = true;
            txtDbNamespace.IsEnabled = true;
            txtDbUser.IsEnabled = true;
            txtDbPassword.IsEnabled = true;
            txtClientPort.IsEnabled = true;
            txtServerPort.IsEnabled = true;
        }

        private void DisableSettings()
        {
            txtDbIp.IsEnabled = false;
            txtDbPort.IsEnabled = false;
            txtDbNamespace.IsEnabled = false;
            txtDbUser.IsEnabled = false;
            txtDbPassword.IsEnabled = false;
            txtClientPort.IsEnabled = false;
            txtServerPort.IsEnabled = false;
        }
    }
}
