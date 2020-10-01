using Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using Newtonsoft.Json;
using RestSharp;
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
            DashboardGame.MainRoot.Children.Add(new CreatLeaderBoard(Start));

        }
    }
}
