using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelAchievements
{
    /// <summary>
    /// Interaction logic for ModelAchievements.xaml
    /// </summary>
    public partial class ModelAchievements : UserControl
    {
        public ModelAchievements(BsonDocument DetailAchievement, ObjectId TokenPlayer, Action Refreshlist)
        {
            InitializeComponent();

            //frist init
            TextToken.Text = DetailAchievement["Token"].ToString();
            TextName.Text = DetailAchievement["Name"].AsString;
            TextRecive.Text = DetailAchievement["Recive"].ToLocalTime().ToString();

            //action delete achievement
            BTNRemove.MouseDown += (s, e) =>
            {
                DetailAchievement.Remove("Recive");

                SDK.SDK_PageDashboards.DashboardGame.PageAchievements.RemoveAchievementsPlayer(TokenPlayer, DetailAchievement, result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Removed", Notifaction.StatusMessage.Ok);
                        Refreshlist();

                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Remove", Notifaction.StatusMessage.Error);
                    }

                });

            };

            //copy TOken
            TextToken.MouseDown += GlobalEvents.CopyText;

        }
    }
}
