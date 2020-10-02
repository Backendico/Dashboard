using Dashboard.GlobalElement;
using System;
using System.Windows;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Window
    {
        string NameLeaderboard;
        public AddPlayer(string NameLeaderboard, Action Refreshlist)
        {
            InitializeComponent();
            this.NameLeaderboard = NameLeaderboard;
            this.Refreshlist = Refreshlist;
        }


        private void Add(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.AddPlayer(NameLeaderboard, TextboxTokenPlayer.Text, int.Parse(TextboxValue.Text),
                () =>
                {
                    Close();
                    DashboardGame.Notifaction("Added ! ", Notifaction.StatusMessage.Ok);
                    Refreshlist();

                    //Addlog
                    var detaillog = new MongoDB.Bson.BsonDocument { { "NameLeaderboard", NameLeaderboard }, { "TokenPlayer", TextboxTokenPlayer.Text }, { "Value", TextboxValue.Text } };
                    SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Add player to leaderboard", $"You have added player \" {TextboxTokenPlayer.Text} \" to the \" {NameLeaderboard} \" leaderboard", detaillog, false, resultlog => { });
                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Add ! ", Notifaction.StatusMessage.Error);
                });
        }


        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }


        Action Refreshlist;

    }
}
