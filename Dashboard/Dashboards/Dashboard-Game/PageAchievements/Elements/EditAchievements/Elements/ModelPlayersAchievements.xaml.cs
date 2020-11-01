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
            InitializeComponent();
            TextToken.Text = DetailPlayer["Token"].ToString();
            TextUsername.Text = DetailPlayer["Username"].ToString() == "" ? "Not Set" : DetailPlayer["Username"].ToString();

            TextToken.MouseDown += GlobalEvents.CopyText;

            BTNRemove.MouseDown += (s, e) =>
            {
                var SerilseDetailachievements = new BsonDocument
                {
                    {"Token",DetailAchievements["Token"] },
                    {"Name",DetailAchievements["Name"] }
                };

                SDK.SDK_PageDashboards.DashboardGame.PageAchievements.RemoveAchievementsPlayer(DetailPlayer["Token"], SerilseDetailachievements, result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Removed", Notifaction.StatusMessage.Ok);
                        RefreshList();
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Remove", Notifaction.StatusMessage.Error);
                    }
                });
            };
        }
    }
}
