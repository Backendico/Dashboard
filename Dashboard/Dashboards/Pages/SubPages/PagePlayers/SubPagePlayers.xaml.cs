using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.AddPlayer;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.SubPages.PagePlayers
{
    
    public partial class SubPagePlayers : UserControl
    {
        public SubPagePlayers()
        {
            InitializeComponent();


            //action btn add player
            BTNaddPlayer.Work += () =>
            {
                PageAUT.Placeholder.Children.Add(new SubPageAddPlayer());
            };

            //action switch to grid view 
            BTNGridView.Worker += () =>
            {
                PanelGridView.Visibility = System.Windows.Visibility.Visible;
                PanelListView.Visibility = System.Windows.Visibility.Collapsed;
            };

            //action switch to list view
            BTNListView.Worker += () =>
            {
                PanelGridView.Visibility = System.Windows.Visibility.Collapsed;
                PanelListView.Visibility = System.Windows.Visibility.Visible;
            };

        }


    }


}
