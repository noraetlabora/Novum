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
        private const string ntServiceName = "Novacom.OrderSolutions";
        private const string osServiceName = "Orderman.OrderSolutions";
        private const string logFileName = "Nt.Services.OrderSolutions.log";

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
            var serviceStatus = Service.GetStatus(ntServiceName);

            switch (serviceStatus)
            {
                case Service.Status.Uninstalled:
                    lblServiceStatus.Content = "Service uninstalled";
                    btnServiceInstall.IsEnabled = true;
                    btnServiceInstall.Content = "Install Service";
                    btnServiceRun.Content = "Run Service";
                    btnServiceRun.IsEnabled = false;
                    EnableArguments();
                    break;
                case Service.Status.Stopped:
                    lblServiceStatus.Content = "stopped";
                    btnServiceInstall.IsEnabled = true;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = true;
                    btnServiceRun.Content = "Run Service";
                    EnableArguments();
                    break;
                case Service.Status.StopPending:
                    lblServiceStatus.Content = "stop pending";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = false;
                    btnServiceRun.Content = "Stop Service";
                    DisableArguments();
                    break;
                case Service.Status.StartPending:
                    lblServiceStatus.Content = "start pending";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = false;
                    btnServiceRun.Content = "Run Service";
                    DisableArguments();
                    break;
                case Service.Status.Paused:
                    lblServiceStatus.Content = "paused";
                    btnServiceInstall.IsEnabled = true;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = true;
                    btnServiceRun.Content = "Run Service";
                    DisableArguments();
                    break;
                case Service.Status.Running:
                    lblServiceStatus.Content = "running";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = true;
                    btnServiceRun.Content = "Stop Service";
                    DisableArguments();
                    break;
                default:
                    lblServiceStatus.Content = "unknown";
                    btnServiceInstall.IsEnabled = false;
                    btnServiceInstall.Content = "Uninstall Service";
                    btnServiceRun.IsEnabled = false;
                    btnServiceRun.Content = "Run Service";
                    EnableArguments();
                    break;
            }
        }

        private void BtnServiceRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var serviceStatus = Service.GetStatus(ntServiceName);
                if (serviceStatus.Equals(Service.Status.Stopped))
                {
                    DisableArguments();
                    SaveArguments();
                    var args = new string[] { "--dbIp", txtDbIp.Text, "--dbPrt", txtDbPort.Text, "--dbNs", txtDbNamespace.Text, "--dbUsr", txtDbUser.Text, "--dbPwd", txtDbPassword.Password, "--osSPrt", txtOsServerPort.Text, "--osCIp", txtOsClientIp.Text, "--osCPrt", txtOsClientPort.Text };
                    Service.Start(ntServiceName, args);
                    System.Threading.Thread.Sleep(10000);
                    Service.Start(osServiceName, Array.Empty<string>());
                }
                else
                {
                    Service.Stop(ntServiceName);
                    Service.Stop(osServiceName);
                }
            }
            catch(Exception ex)
            {
                using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(logFileName, true))
                {
                    outputFile.WriteLine(ex.Message);
                    outputFile.WriteLine(ex.StackTrace);
                }
            }
        }

        private void BtnServiceInstall_Click(object sender, RoutedEventArgs e)
        {
            var assemblyFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var ntOsServer = assemblyFolder + "\\Os.Server.exe";
            var osServer = assemblyFolder + "\\OsServer\\OsServerRun.exe";

            try
            {
                var serviceStatus = Service.GetStatus(ntServiceName);
                if (serviceStatus.Equals(Service.Status.Uninstalled))
                {
                    Service.Install(ntServiceName, ntOsServer);
                    Service.Install(osServiceName, osServer);
                }
                else
                {
                    Service.Uninstall(osServiceName);
                    Service.Uninstall(ntServiceName);
                }
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(logFileName, true))
                {
                    outputFile.WriteLine(ex.Message);
                    outputFile.WriteLine(ex.StackTrace);
                }
            }
        }

        private void EnableArguments()
        {
            txtDbIp.IsEnabled = true;
            txtDbPort.IsEnabled = true;
            txtDbNamespace.IsEnabled = true;
            txtDbUser.IsEnabled = true;
            txtDbPassword.IsEnabled = true;
            txtOsServerPort.IsEnabled = true;
            txtOsClientIp.IsEnabled = true;
            txtOsClientPort.IsEnabled = true;
        }

        private void DisableArguments()
        {
            txtDbIp.IsEnabled = false;
            txtDbPort.IsEnabled = false;
            txtDbNamespace.IsEnabled = false;
            txtDbUser.IsEnabled = false;
            txtDbPassword.IsEnabled = false;
            txtOsServerPort.IsEnabled = false;
            txtOsClientIp.IsEnabled = false;
            txtOsClientPort.IsEnabled = false;
        }

        private void SaveArguments()
        {
            var osArguments = new Arguments.OsArguments();
            osArguments.DatabaseIp = txtDbIp.Text;
            osArguments.DatabasePort = uint.Parse(txtDbPort.Text);
            osArguments.DatabaseNamespace = txtDbNamespace.Text;
            osArguments.DatabaseUser = txtDbUser.Text;
            osArguments.DatabasePassword = Util.Encryption.EncryptString(txtDbPassword.Password);
            osArguments.OsServerPort = uint.Parse(txtOsServerPort.Text);
            osArguments.OsClientIp = txtOsClientIp.Text;
            osArguments.OsClientPort = uint.Parse(txtOsClientPort.Text);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(osArguments);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Os.Server.json", json);
        }

        private void LoadArguments()
        {
            var osArguments = new Arguments.OsArguments();

            try
            {
                var json = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Os.Server.json");
                osArguments = Newtonsoft.Json.JsonConvert.DeserializeObject<Arguments.OsArguments>(json);
            }
            catch
            {
            }

            txtDbIp.Text = osArguments.DatabaseIp;
            txtDbPort.Text = osArguments.DatabasePort.ToString();
            txtDbNamespace.Text = osArguments.DatabaseNamespace;
            txtDbUser.Text = osArguments.DatabaseUser;
            txtDbPassword.Password = Util.Encryption.DecryptString(osArguments.DatabasePassword);
            txtOsServerPort.Text = osArguments.OsServerPort.ToString();
            txtOsClientIp.Text = osArguments.OsClientIp;
            txtOsClientPort.Text = osArguments.OsClientPort.ToString(); 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadArguments();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveArguments();
        }
    }
}
