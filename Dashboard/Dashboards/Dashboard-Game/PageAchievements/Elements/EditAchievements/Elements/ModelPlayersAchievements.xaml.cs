using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements.EditAchievements.Elements
{
    /// <summary>
    /// Interaction logic for ModelPlayersAchievements.xaml
    /// </summary>
    public partial class ModelPlayersAchievements : UserControl
    {
        public ModelPlayersAchievements(BsonDocument DetailPlayer, BsonDocument DetailAchievements, Action RefreshList)
        {
            //frist init
            InitializeComponent();
            TextToken.Text = DetailPlayer["Token"].ToString();
            TextUsername.Text = DetailPlayer["Username"].ToString() == "" ? "Not Set" : DetailPlayer["Username"].ToString();
            TextRecive.Text = DetailPlayer["Recive"].ToLocalTime().ToString();

            //copy token
            TextToken.MouseDown += GlobalEvents.CopyText;

            //action remove
            BTNRemove.MouseDown += async (s, e) =>
             {
                 var SerilseDetailachievements = new BsonDocument
                 {
                    {"Token",DetailAchievements["Token"] },
                    {"Name",DetailAchievements["Name"] },
                     {"Recive",DetailPlayer["Recive"] }
                 };

                 if (await DashboardGame.DialogYesNo("All information about this player achievement will be deleted \n Are you sure ? ") == System.Windows.MessageBoxResult.Yes)
                 {
                     SDK.SDK_PageDashboards.DashboardGame.PageAchievements.RemoveAchievementsPlayer(DetailPlayer["Token"].AsObjectId, SerilseDetailachievements, result =>
                 {
                     if (result)
                     {
                         DashboardGame.Notifaction("Removed", Notifaction.StatusMessage.Ok);
                         RefreshList();

                         //add log
                         SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Remove player from achievement", $"Player \" {DetailPlayer["Token"]} \" was removed from the \" {DetailAchievements["Name"]} \" achievement", new BsonDocument { }, false, resultLog => { });
                     }
                     else
                     {
                         DashboardGame.Notifaction("Faild Remove", Notifaction.StatusMessage.Error);
                     }
                 });
                 }
                 else
                 {
                     DashboardGame.Notifaction("Reject", Notifaction.StatusMessage.Warrning);
                 }

             };
        }
    }
}
