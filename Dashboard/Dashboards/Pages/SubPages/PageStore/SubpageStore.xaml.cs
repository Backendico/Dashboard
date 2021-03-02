using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.SubPages.PageStore
{
    public partial class SubpageStore : UserControl
    {
        public SubpageStore()
        {
            InitializeComponent();

            //action btn add player
            BTNaddPlayer.Work += () =>
            {
            };

            //action switch to grid view 
            BTNGridView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Visible;
                PanelListView.Visibility = Visibility.Collapsed;
            };

            //action switch to list view
            BTNListView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Collapsed;
                PanelListView.Visibility = Visibility.Visible;
            };

        }

    }
}
