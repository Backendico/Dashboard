using Dashboard.GlobalElement;
using MongoDB.Bson;
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
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageDashboard.ReciveDetail(
                result =>
                {
                    TextPlayer_24Hours.Text = result["Players"]["24Hours"].ToString();
                    TextPlayer_1Day.Text = result["Players"]["1Days"].ToString();
                    TextPlayer_7Day.Text = result["Players"]["7Days"].ToString();
                    TextPlayer_30Day.Text = result["Players"]["30Days"].ToString();

                    TextLogin_24hours.Text = result["Logins"]["24Hours"].ToString();
                    TextLogin_1Day.Text = result["Logins"]["1Days"].ToString();
                    TextLogin_7Day.Text = result["Logins"]["7Days"].ToString();
                    TextLogin_30Day.Text = result["Logins"]["30Days"].ToString();

                    TextCount_Leaderboards.Text = result["Counts"]["Leaderboards"].ToString();
                    TextCount_Players.Text = result["Counts"]["Players"].ToString();

                },
                () =>
                {
                });

        }
    }
}
