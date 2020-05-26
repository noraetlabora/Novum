using System.Windows;

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