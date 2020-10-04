using Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements;
using Dashboard.GlobalElement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.PageLeaderboards
{
    /// <summary>
    /// Interaction logic for PageLeaderBoards.xaml
    /// </summary>
    public partial class PageLeaderBoards : UserControl
    {
        public PageLeaderBoards()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reciveleaderboards(
                resul =>
                {
                    PlaceLeaderboard.Children.Clear();
                    foreach (var item in resul)
                    {
                        PlaceLeaderboard.Children.Add(new ModelLeaderboardAbstract(item.Value.AsBsonDocument, Start));
                    }
                },
                () =>
                {
                });
        }


        private void CreatLeaderboard(object sender, MouseButtonEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Add(new CreatLeaderBoard(Start));

        }
    }
}
