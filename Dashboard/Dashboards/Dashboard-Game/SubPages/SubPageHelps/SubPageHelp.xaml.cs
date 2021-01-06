using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageHelps
{
    /// <summary>
    /// Interaction logic for SubPageHelp.xaml
    /// </summary>
    public partial class SubPageHelp : UserControl
    {
        public SubPageHelp()
        {
            InitializeComponent();
        }

        void Close(object sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }
    }
}
