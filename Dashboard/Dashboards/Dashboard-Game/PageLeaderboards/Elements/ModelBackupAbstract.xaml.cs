using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for ModelBackupAbstract.xaml
    /// </summary>
    public partial class ModelBackupAbstract : UserControl
    {
        BsonDocument Detail;

        public ModelBackupAbstract(BsonDocument Detail, Action RefreshList)
        {
            InitializeComponent();
            this.Detail = Detail;
            this.RefreshList = RefreshList;

            TextVersion.Text = Detail["Name"].AsString;
            TextStart.Text = Detail["Start"].ToUniversalTime().ToString();
            TextEnd.Text = Detail["End"].ToUniversalTime().ToString();


        }

        private async void RemoveBackup(object sender,MouseButtonEventArgs e)
        {
            if (await DashboardGame.DialogYesNo("Information can not be undone") == MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.RemoveBackup(Detail["NameLeaderboard"].AsString, Detail["Name"].AsString,
                    () =>
                    {
                        DashboardGame.Notifaction("Deleted !", Notifaction.StatusMessage.Ok);
                        RefreshList();

                        //log
                        SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Delete Backup", $"Backup  \" {Detail["NameLeaderboard"]}\" deleted", new BsonDocument { }, false, resultLog => { });
                    },
                    () =>
                    {
                        DashboardGame.Notifaction("Fail Delete", Notifaction.StatusMessage.Error);
                    });
            }
            else
            {
                DashboardGame.Notifaction("Reject Delete", Notifaction.StatusMessage.Error);
            }
        }

        private void Download(object sender, MouseButtonEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Add(new ModelDownloadBackup(Detail["NameLeaderboard"].ToString(), Detail["Name"].ToString()));
        }


        Action RefreshList;


    }


}
