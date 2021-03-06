﻿using Dashboard.GlobalElement;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.PageDashboard
{
    /// <summary>
    /// Interaction logic for PageDashboard.xaml
    /// </summary>
    public partial class PageDashboard : UserControl
    {
        public PageDashboard()
        {
            InitializeComponent();

            //recive Notifactions
            DashboardGame.Dashboard.ReciveNotifactions();

            DashboardGame.ReciveManuallNotifaction();

            Loaded += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageDashboard.ReciveDetail(
                    result =>
                    {
                        //players
                        TextPlayer_24Hours.Text = result["Players"]["24Hours"].ToString();
                        TextPlayer_1Day.Text = result["Players"]["1Days"].ToString();
                        TextPlayer_7Day.Text = result["Players"]["7Days"].ToString();
                        TextPlayer_30Day.Text = result["Players"]["30Days"].ToString();

                        //Logs
                        TextLogin_24hours.Text = result["Logins"]["24Hours"].ToString();
                        TextLogin_1Day.Text = result["Logins"]["1Days"].ToString();
                        TextLogin_7Day.Text = result["Logins"]["7Days"].ToString();
                        TextLogin_30Day.Text = result["Logins"]["30Days"].ToString();

                        //leaderboards
                        TextCount_Leaderboards.Text = result["Leaderboards"]["Count"].ToString();
                        TextCountTotal_Leaderbords.Text = result["Leaderboards"]["Totall"].ToString();

                        //Players
                        TextCount_Players.Text = result["PlayersMonetiz"]["Count"].ToString();
                        TextCountTotal_Players.Text = result["PlayersMonetiz"]["Totall"].ToString();

                        //Logs
                        TextCount_Logs.Text = result["Logs"]["Count"].ToString();
                        TextTotalCount_Logs.Text = result["Logs"]["Totall"].ToString();

                        //Apis
                        TextCount_APIs.Text = result["APIs"]["Count"].ToString();
                        TextTotalCount_APIs.Text = result["APIs"]["Totall"].ToString();

                        //achievements
                        TextTotalCount_Achievements.Text = result["Achievements"]["Totall"].ToString();
                        TextCount_Achievements.Text = result["Achievements"]["Count"].ToString();

                        //Store
                        TextTotalCount_Store.Text = result["Store"]["Count"].ToString();
                        TextCount_Store.Text = result["Store"]["Totall"].ToString();
                    },
                    () =>
                    {
                    });

            };
        }

    }
}
