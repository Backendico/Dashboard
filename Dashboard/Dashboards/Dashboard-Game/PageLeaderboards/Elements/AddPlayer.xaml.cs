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
                    MessageBox.Show("Added ! ");
                    Refreshlist();
                },
                () =>
                {
                    MessageBox.Show("Faild  to add :(");
                });
        }


        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }


        Action Refreshlist;

    }
}
