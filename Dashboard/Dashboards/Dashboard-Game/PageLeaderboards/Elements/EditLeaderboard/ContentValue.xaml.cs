using Dashboard.GlobalElement;
using MongoDB.Bson;
using RestSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for ContentValue.xaml
    /// </summary>
    public partial class ContentValue : UserControl
    {
        BsonDocument DetailValue;
        public ContentValue(BsonDocument DetailValue, Action RefreshList)
        {
            InitializeComponent();
            this.DetailValue = DetailValue;

            TextToken.Text = DetailValue["Token"].ToString();
            TextValue.Text = DetailValue["Value"].ToString();
            TextRank.Text = DetailValue["Rank"].ToString();
            this.RefreshList = RefreshList;
        }

        private void Remove(object Sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("The value for the user is deleted \n Are you sure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Remove(DetailValue["Token"].ToString(), DetailValue["NameLeaderboard"].ToString(),
                    () =>
                    {
                        MessageBox.Show("Deleted !");
                        RefreshList();
                    },
                    () =>
                    {
                        MessageBox.Show("Faild Delete :(");
                    });
            }
        }


        Action RefreshList;

        private void PointerEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gainsboro);
        }

        private void PointerExit(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.WhiteSmoke);
        }
    }
}
