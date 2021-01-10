using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageDocuments
{
    /// <summary>
    /// Interaction logic for SubpageDocuments.xaml
    /// </summary>
    public partial class SubpageDocuments : UserControl
    {
        public SubpageDocuments()
        {
            InitializeComponent();
        }

        void Close(object Sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }
    }
}
