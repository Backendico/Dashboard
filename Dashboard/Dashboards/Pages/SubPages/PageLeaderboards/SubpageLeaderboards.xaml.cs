using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.SubPages.PageLeaderboards.Moduls.AddLeaderboard;
using Dashboard.GlobalElement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Dashboard.Dashboards.Pages.SubPages.PageLeaderboards
{
    /// <summary>
    /// Interaction logic for SubpageLeaderboards.xaml
    /// </summary>
    public partial class SubpageLeaderboards : UserControl, IPageLeaderboard
    {
        public SubpageLeaderboards()
        {
            InitializeComponent();
            
            Init();

            BTNaddLeaderboard.Work += () =>
            {
                PageAUT.Placeholder.Children.Add(new SubpageAddLeaderboards());
            };

            BTNListView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Collapsed;
                PanelListView.Visibility = Visibility.Visible;

            };
            BTNGridView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Visible;
                PanelListView.Visibility = Visibility.Collapsed;
            };


        }

        public void Init()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reciveleaderboards(Result =>
            {
                if (Result.ElementCount >= 1)
                {
                    Debug.WriteLine("find Leaderboard");
                }
                else
                {
                    Debug.WriteLine("No Leaderboard");
                    PageAUT.Placeholder.Children.Add(new SubpageAddLeaderboards());
                }

            });

        }
    }
    public interface IPageLeaderboard
    {
        void Init();
    }
}
