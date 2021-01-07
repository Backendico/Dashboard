using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    public partial class ModelAbstractUser : UserControl, IEditPlayer
    {

        public BsonDocument DetailPlayer { get; set; }

        public ModelAbstractUser(BsonDocument DetailPlayer, Action Refreshlist)
        {
            InitializeComponent();

            this.DetailPlayer = DetailPlayer;
            this.Refreshlist = Refreshlist;


            Update();

            TextToken.MouseDown += GlobalEvents.CopyText;


            BTNEdit.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new EditPlayer(this));
            };

        }


        public void Save()
        {
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.Save(DetailPlayer["Account"]["Token"].ToString(), DetailPlayer, result =>
            {
                if (result)
                {
                    DashboardGame.Notifaction("Saved", Notifaction.StatusMessage.Ok);

                    // add log
                    SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Edit Player", $"You have Edit player \" {DetailPlayer["Account"]["Username"]} \" ", DetailPlayer, false, resul => { });
                }
                else
                {
                    DashboardGame.Notifaction("Not Change", Notifaction.StatusMessage.Warrning);
                }

            });

            Update();
        }

        public void RefreshMainList()
        {
            Refreshlist();
        }

        public void Update()
        {
            TextToken.Text = DetailPlayer["Account"]["Token"].AsObjectId.ToString();
            TextLastLogin.Text = DetailPlayer["Account"]["LastLogin"].ToUniversalTime().ToString();
            TextCreated.Text = DetailPlayer["Account"]["Created"].ToUniversalTime().ToString();
            TextCountry.Text = DetailPlayer["Account"]["Country"].AsString;
            TextUsername.Text = DetailPlayer["Account"]["Username"].AsString;

            if (DetailPlayer["Account"]["IsBan"].AsBoolean)
            {
                StatusBan.BorderBrush = new SolidColorBrush(Colors.Tomato);
            }
            else
            {
                StatusBan.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
        }

        public async void Delete(UserControl Editor)
        {
            if (await DashboardGame.DialogYesNo("All user information is lost \n Are you sure") == System.Windows.MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.Delete(DetailPlayer["Account"]["Token"].ToString(), result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Deleted !", Notifaction.StatusMessage.Ok);

                        RefreshMainList();
                        DashboardGame.Dashboard.Root.Children.Remove(this);
                        DashboardGame.Dashboard.Root.Children.Remove(Editor);


                        //add log
                        SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Delete Player", $"You have deleted player \" {DetailPlayer["Account"]["Username"]} \"", DetailPlayer, false, resul => { });
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Delete !", Notifaction.StatusMessage.Ok);
                    }
                });

            }
            else
            {
                DashboardGame.Notifaction("Delete Reject", Notifaction.StatusMessage.Ok);
            }

        }

        public void AddLeaderboard(BsonDocument DetailLeaderboard)
        {
        }

        Action Refreshlist;
    }

    public interface IEditPlayer
    {
        void AddLeaderboard(BsonDocument DetailLeaderboard);
        void Save();
        void RefreshMainList();
        void Update();
        void Delete(UserControl Editor);
        BsonDocument DetailPlayer { get; set; }
    }
}
