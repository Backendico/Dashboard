using Dashboard.Dashboards.Dashboard_Game.Elements.PageDashboard;
using Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using RestSharp;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.PagePlayer
{
    /// <summary>
    /// Interaction logic for PagePlayers.xaml
    /// </summary>
    public partial class PagePlayers : UserControl
    {
        public PagePlayers()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 1:recive list players top 100
        /// 2: instant eachplayer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.ReciveListPlayer(
                result =>
                {
                    PlaceContentUser.Children.Clear();
                    foreach (var item in result["ListPlayers"].AsBsonArray)
                    {
                        PlaceContentUser.Children.Add(new ModelAbstractUser(item.AsBsonDocument, Start,Parent as Grid));
                    }

                    //init total player
                    TextTotalPlayer.Text = result["Players"].ToString() + "   total players";
                },
                () =>
                {
                    MessageBox.Show("Faild Recive Player :(");

                });

        }


        private void Add(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DashboardGame.MainRoot.Children.Add(new CreatNewPlayer(Start));

        }

        private void Search(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DashboardGame.MainRoot.Children.Add(new SearchUser());
        }

    }
}
