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

        public ModelBackupAbstract(BsonDocument Detail)
        {
            InitializeComponent();
            this.Detail = Detail;

            TextVersion.Text = Detail["Settings"]["Token"].ToString();
            TextStart.Text = Detail["Settings"]["Start"].ToLocalTime().ToString();
            TextEnd.Text = Detail["Settings"]["End"].ToLocalTime().ToString();

            TextVersion.MouseDown += GlobalEvents.CopyText;

            BTNRemove.MouseDown += async (s, e) =>
            {
                if (await DashboardGame.DialogYesNo("Information can not be undone") == MessageBoxResult.Yes)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.RemoveBackup(Detail["Settings"]["Token"].AsObjectId,
                        (Result) =>
                        {
                            (Parent as StackPanel).Children.Remove(this);

                            if (Result)
                            {
                                DashboardGame.Notifaction("Deleted !", Notifaction.StatusMessage.Ok);

                            //log
                            SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Delete Backup", $"Backup  \" {Detail["Settings"]["Token"].AsObjectId}\" deleted", new BsonDocument { }, false, resultLog => { });
                            }
                            else
                            {
                                DashboardGame.Notifaction("Cant Delete !", Notifaction.StatusMessage.Error);
                            }
                        });
                }
                else
                {
                    DashboardGame.Notifaction("Reject Delete", Notifaction.StatusMessage.Error);
                }
            };

            BTNDownload.MouseDown += (s, e) => { 
            
            DashboardGame.Dashboard.Root.Children.Add(new ModelDownloadBackup(Detail));
            
            };
        }

    }


}
