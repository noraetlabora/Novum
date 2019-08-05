using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nt.Services
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OrderSolutions_Selected(null, null);
        }

        private void OrderSolutions_Selected(object sender, RoutedEventArgs e)
        {
            UserControlsGrid.Children.Clear();
            UserControlsGrid.Children.Add(new UserControls.OrderSolutions());
        }

        private void Logs_Selected(object sender, RoutedEventArgs e)
        {
            UserControlsGrid.Children.Clear();
            UserControlsGrid.Children.Add(new UserControls.Logs());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}