using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageEduction
{
    /// <summary>
    /// Interaction logic for SubPageEduction_.xaml
    /// </summary>
    public partial class SubPageEduction : UserControl
    {
        public SubPageEduction()
        {
            InitializeComponent();
        }

        public void Close(object Sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }
    }
}
