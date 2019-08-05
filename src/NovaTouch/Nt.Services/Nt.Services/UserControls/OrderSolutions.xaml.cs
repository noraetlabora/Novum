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
        private const string serviceName = "novacomOS";
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
            switch(serviceStatus)
            {
                case Service.Status.Uninstalled:
                    //brdServiceStatus.Background = 
                    lblServiceStatus.Content = "uninstalled";
                    break;
                case Service.Status.Stopped:
                    lblServiceStatus.Content = "stopped";
                    break;
                case Service.Status.StopPending:
                    lblServiceStatus.Content = "stop pending";
                    break;
                case Service.Status.StartPending:
                    lblServiceStatus.Content = "start pending";
                    break;
                case Service.Status.Paused:
                    lblServiceStatus.Content = "paused";
                    break;
                case Service.Status.Running:
                    lblServiceStatus.Content = "running";
                    break;
                default:
                    lblServiceStatus.Content = "unknown";
                    break;
            }
        }
    }
}
