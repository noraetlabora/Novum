using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const string novOsServiceName = "Novacom OrderSolution Server";
        private string osServerConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + @"\OrderSolution\Os.Server.config.json";
        private string osClientConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + @"\OrderSolution\Os.Client.config.json";

        public OrderSolutions()
        {
            InitializeComponent();
            serviceStatus_Tick(null, null);
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(serviceStatus_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        public override bool ShouldSerializeContent()
        {
            return base.ShouldSerializeContent();
        }

        private void serviceStatus_Tick(object sender, EventArgs e)
        {
            var serviceStatus = Service.GetStatus(novOsServiceName);

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
                var serviceStatus = Service.GetStatus(novOsServiceName);
                if (serviceStatus.Equals(Service.Status.Stopped))
                {
                    DisableArguments();
                    btnServiceInstall.IsEnabled = false;
                    btnServiceRun.IsEnabled = false;
                    SaveServerConfiguration();
                    SaveClientConfiguration();
                    Service.Start(novOsServiceName);
                    System.Threading.Thread.Sleep(10000);
                    NcrOsServer("start");
                }
                else
                {
                    Service.Stop(novOsServiceName);
                    NcrOsServer("stop");
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Service.Error(ex, "couldn't start/stop service");
            }
        }

        private void BtnServiceInstall_Click(object sender, RoutedEventArgs e)
        {
            var assemblyFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var novOsServer = assemblyFolder + @"\OrderSolution\Os.Server.exe";

            try
            {
                var serviceStatus = Service.GetStatus(novOsServiceName);
                if (serviceStatus.Equals(Service.Status.Uninstalled))
                {
                    Service.Install(novOsServiceName, novOsServer);
                    NcrOsServer("install");
                }
                else
                {
                    Service.Uninstall(novOsServiceName);
                    NcrOsServer("uninstall");
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Service.Error(ex, "couldn't (un)install service");
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
            cmbLocalization.IsEnabled = true;
            cmbPriceEntryMode.IsEnabled = true;
            cmbAuthenticationMode.IsEnabled = true;
            chbDisableSubTables.IsEnabled = true;
            chbMoveAllSubTables.IsEnabled = true;
            chbMoveSingleSubTable.IsEnabled = true;
            chbTip.IsEnabled = true;
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
            cmbLocalization.IsEnabled = false;
            cmbPriceEntryMode.IsEnabled = false;
            cmbAuthenticationMode.IsEnabled = false;
            chbDisableSubTables.IsEnabled = false;
            chbMoveAllSubTables.IsEnabled = false;
            chbMoveSingleSubTable.IsEnabled = false;
            chbTip.IsEnabled = false;
        }

        private void SaveServerConfiguration()
        {
            try
            {
                var osServerConfiguration = new Os.Server.ServerConfiguration();
                osServerConfiguration.DatabaseIp = txtDbIp.Text;
                osServerConfiguration.DatabasePort = uint.Parse(txtDbPort.Text);
                osServerConfiguration.DatabaseNamespace = txtDbNamespace.Text;
                osServerConfiguration.DatabaseUser = txtDbUser.Text;
                osServerConfiguration.DatabasePassword = Nt.Util.Encryption.EncryptString(txtDbPassword.Password);

                osServerConfiguration.OsServerPort = uint.Parse(txtOsServerPort.Text);
                osServerConfiguration.OsClientIp = txtOsClientIp.Text;
                osServerConfiguration.OsClientPort = uint.Parse(txtOsClientPort.Text);

                var jsonOption = new System.Text.Json.JsonSerializerOptions();
                jsonOption.WriteIndented = true;
                string json = System.Text.Json.JsonSerializer.Serialize(osServerConfiguration, jsonOption);
                System.IO.File.WriteAllText(osServerConfigurationFile, json);
            }
            catch (Exception ex)
            {
                Logging.Log.Service.Error(ex, "exception while saving server configuration");
            }
        }

        private void SaveClientConfiguration()
        {
            try
            {
                var osClientConfiguration = new Os.Server.ClientConfiguration();

                var jsonOption = new System.Text.Json.JsonSerializerOptions();
                jsonOption.WriteIndented = true;

                //CommaToSet
                if (cmbPriceEntryMode.Text.Equals("CommaToSet"))
                    osClientConfiguration.PriceEntryMode = "0";
                //FixedComma
                else
                    osClientConfiguration.PriceEntryMode = "1";

                if ((bool)chbDisableSubTables.IsChecked)
                    osClientConfiguration.DisableSubtables = true;
                else
                    osClientConfiguration.DisableSubtables = false;

                osClientConfiguration.Localization = cmbLocalization.Text;
                osClientConfiguration.AuthenthicationMode = cmbAuthenticationMode.Text;
                osClientConfiguration.FeatureMoveAllSubTables = (bool)chbMoveAllSubTables.IsChecked;
                osClientConfiguration.FeatureMoveSingleSubTable = (bool)chbMoveSingleSubTable.IsChecked;
                osClientConfiguration.FeatureTip = (bool)chbTip.IsChecked;
                osClientConfiguration.Coursing = (bool)chbCoursing.IsChecked;

                string json = System.Text.Json.JsonSerializer.Serialize(osClientConfiguration, jsonOption);
                System.IO.File.WriteAllText(osClientConfigurationFile, json);
            }
            catch (Exception ex)
            {
                Logging.Log.Service.Error(ex, "exception while saving client configuration");
            }
        }

        private void LoadClientConfiguration()
        {
            var osClientConfiguration = new Os.Server.ClientConfiguration();

            try
            {
                var json = System.IO.File.ReadAllText(osClientConfigurationFile);
                osClientConfiguration = System.Text.Json.JsonSerializer.Deserialize<Os.Server.ClientConfiguration>(json);

                //CommaToSet
                if (osClientConfiguration.PriceEntryMode.Equals("0"))
                    cmbPriceEntryMode.Text = "CommaToSet";
                else
                    cmbPriceEntryMode.Text = "FixedComma";

                if (osClientConfiguration.DisableSubtables)
                    chbDisableSubTables.IsChecked = true;
                else
                    chbDisableSubTables.IsChecked = false;

                cmbLocalization.Text = osClientConfiguration.Localization;
                cmbAuthenticationMode.Text = osClientConfiguration.AuthenthicationMode;
                chbMoveAllSubTables.IsChecked = osClientConfiguration.FeatureMoveAllSubTables;
                chbMoveSingleSubTable.IsChecked = osClientConfiguration.FeatureMoveSingleSubTable;
                chbTip.IsChecked = osClientConfiguration.FeatureTip;
                chbCoursing.IsChecked = osClientConfiguration.Coursing;
            }
            catch(Exception ex)
            {
                Logging.Log.Service.Error(ex, "exception while loading client configuration");
            }

        }

        private void LoadServerConfiguration()
        {
            var osServerConfiguration = new Os.Server.ServerConfiguration();

            try
            {
                var json = System.IO.File.ReadAllText(osServerConfigurationFile);
                osServerConfiguration = System.Text.Json.JsonSerializer.Deserialize<Os.Server.ServerConfiguration>(json);

                txtDbIp.Text = osServerConfiguration.DatabaseIp;
                txtDbPort.Text = osServerConfiguration.DatabasePort.ToString();
                txtDbNamespace.Text = osServerConfiguration.DatabaseNamespace;
                txtDbUser.Text = osServerConfiguration.DatabaseUser;
                txtDbPassword.Password = Nt.Util.Encryption.DecryptString(osServerConfiguration.DatabasePassword);
                txtOsServerPort.Text = osServerConfiguration.OsServerPort.ToString();
                txtOsClientIp.Text = osServerConfiguration.OsClientIp;
                txtOsClientPort.Text = osServerConfiguration.OsClientPort.ToString();
            }
            catch (Exception ex)
            {
                Logging.Log.Service.Error(ex, "exception while loading server configuration");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadServerConfiguration();
            LoadClientConfiguration();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveServerConfiguration();
            SaveClientConfiguration();
        }

        private void NcrOsServer(string argument)
        {
            var assemblyFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var ncrOsServer = System.IO.Path.Combine(assemblyFolder, @"OrderSolution\NCR\OsServer\OsServerRun.exe");
            Logging.Log.Service.Info("call NCR OrderSolution Server to " + argument);
            Logging.Log.Service.Info(ncrOsServer + " " + argument);

            using (Process process = new Process())
            {
                process.StartInfo.FileName = ncrOsServer;
                process.StartInfo.Arguments = argument;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();

                var message = process.StandardOutput.ReadToEnd();

                if (!string.IsNullOrEmpty(message))
                {
                    Logging.Log.Service.Info(message);
                }
            }
        }
    }
}
