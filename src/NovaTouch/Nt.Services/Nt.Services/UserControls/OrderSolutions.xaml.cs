﻿using System;
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
        private string configurationFile = AppDomain.CurrentDomain.BaseDirectory + @"\OrderSolution\Os.Server.config.json";

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
                    SaveArguments();
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
            catch(Exception ex)
            {
                LogEvent(ex);
            }
        }

        private static void LogEvent(Exception ex)
        {
            var eventLog = new EventLog();
            eventLog.Source = novOsServiceName;

            var builder = new StringBuilder();
            builder.Append(ex.Message);
            if (ex.InnerException != null)
            {
                builder.Append(System.Environment.NewLine);
                builder.Append(ex.InnerException.Message);
            }

            eventLog.WriteEntry(builder.ToString());
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
                LogEvent(ex);
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
            var osServerConfiguration = new Os.Server.OsServerConfiguration();
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
            System.IO.File.WriteAllText(configurationFile, json);
        }

        private void LoadArguments()
        {
            var osServerConfiguration = new Os.Server.OsServerConfiguration();

            try
            {
                var json = System.IO.File.ReadAllText(configurationFile);
                osServerConfiguration = System.Text.Json.JsonSerializer.Deserialize<Os.Server.OsServerConfiguration>(json);
            }
            catch
            {
            }

            txtDbIp.Text = osServerConfiguration.DatabaseIp;
            txtDbPort.Text = osServerConfiguration.DatabasePort.ToString();
            txtDbNamespace.Text = osServerConfiguration.DatabaseNamespace;
            txtDbUser.Text = osServerConfiguration.DatabaseUser;
            txtDbPassword.Password = Nt.Util.Encryption.DecryptString(osServerConfiguration.DatabasePassword);
            txtOsServerPort.Text = osServerConfiguration.OsServerPort.ToString();
            txtOsClientIp.Text = osServerConfiguration.OsClientIp;
            txtOsClientPort.Text = osServerConfiguration.OsClientPort.ToString(); 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadArguments();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveArguments();
        }

        private void NcrOsServer(string argument)
        {
            var assemblyFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var ncrOsServer = assemblyFolder + "\\OrderSolution\\NRC\\OsServer\\OsServerRun.exe";

            EventLog eventLog = new EventLog();
            eventLog.Source = "Novacom Service";
            eventLog.WriteEntry(argument + " service NCR OrderSolution Server" + System.Environment.NewLine + ncrOsServer);

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
                    eventLog = new EventLog();
                    eventLog.Source = "Novacom Service";
                    eventLog.WriteEntry(ncrOsServer + argument + System.Environment.NewLine + message);
                }
            }
        }
    }
}
