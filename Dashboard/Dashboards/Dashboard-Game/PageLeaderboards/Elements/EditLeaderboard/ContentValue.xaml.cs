using Dashboard.GlobalElement;
using MongoDB.Bson;
using RestSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private async void Remove(object Sender, MouseButtonEventArgs e)
        {
            if (await DashboardGame.DialogYesNo("The value for the user is deleted \n Are you sure?")==MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Remove(DetailValue["Token"].ToString(), DetailValue["NameLeaderboard"].ToString(),
                    () =>
                    {
                        DashboardGame.Notifaction("Deleted", Notifaction.StatusMessage.Ok);
                        RefreshList();
                    },
                    () =>
                    {
                        DashboardGame.Notifaction("Faild Delete",Notifaction.StatusMessage.Error);
                    });
            }
            else
            {
                DashboardGame.Notifaction("Delete reject", Notifaction.StatusMessage.Error);
            }
        }


        Action RefreshList;

    }
}
