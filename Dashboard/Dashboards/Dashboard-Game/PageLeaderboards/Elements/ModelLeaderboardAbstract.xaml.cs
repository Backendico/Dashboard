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
    /// Interaction logic for ModelLeaderboardAbstract.xaml
    /// </summary>
    public partial class ModelLeaderboardAbstract : UserControl, IEditorLeaderboard
    {

        public ModelLeaderboardAbstract(BsonDocument Detail, Action RefreshList)
        {
            InitializeComponent();
            DetailLeaderboard = Detail;

            Init();

            BTNEdit.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new EditLeaderboard(this));
            };

            TextToken.MouseDown += GlobalEvents.CopyText;
        }

        public BsonDocument DetailLeaderboard { get; set; }


        void Init()
        {

            TextName.Text = DetailLeaderboard["Settings"]["Name"].AsString;
            TextToken.Text = DetailLeaderboard["Settings"]["Token"].AsObjectId.ToString();

            switch (DetailLeaderboard["Settings"]["Reset"].ToInt32())
            {
                case 0:
                    {
                        TextReset.Text = "Manually";
                    }
                    break;
                case 1:
                    {
                        TextReset.Text = "Hourly";
                    }
                    break;
                case 2:
                    {
                        TextReset.Text = "Daily";
                    }
                    break;
                case 3:
                    {
                        TextReset.Text = "Weekly";
                    }
                    break;
                case 4:
                    {
                        TextReset.Text = "monthly";
                    }
                    break;
                default:
                    TextReset.Text = "NotCategory";
                    break;
            }

            switch (DetailLeaderboard["Settings"]["Sort"].ToInt32())
            {
                case 0:
                    {
                        TextSort.Text = "Last";
                    }
                    break;
                case 1:
                    {
                        TextSort.Text = "Minimum";
                    }
                    break;
                case 2:
                    {
                        TextSort.Text = "Maximum";
                    }
                    break;
                case 3:
                    {
                        TextSort.Text = "Sum";
                    }
                    break;

                default:
                    TextReset.Text = "NotCategory";
                    break;
            }


            //TextPlayers.Text = Detail["Count"].ToString();
            Debug.WriteLine("Count Player add to " + GetType().Name);
        }

        public void Delete()
        {

        }


        public void Save()
        {


            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.EditLeaderboard(DetailLeaderboard["Settings"].AsBsonDocument, result =>
            {
                if (result)
                {
                    DashboardGame.Notifaction("Saved !", Notifaction.StatusMessage.Ok);
                    Init();

                    //log
                    SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Edit Leaderboard", $"You have changed the \" {DetailLeaderboard["Settings"]["Name"]} \" leaderboard settings", DetailLeaderboard["Settings"].AsBsonDocument, false, resultlog => { });
                }
                else
                {

                    DashboardGame.Notifaction("Not Change !", Notifaction.StatusMessage.Error);
                }

            });

        }
    }

    public interface IEditorLeaderboard
    {
        void Save();
        void Delete();
        BsonDocument DetailLeaderboard { get; set; }
    }
}
