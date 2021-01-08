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
        public ModelAchievements(BsonDocument DetailAchievement, IEditAchievements Editor)
        {
            InitializeComponent();

            //frist init
            TextToken.Text = DetailAchievement["Token"].ToString();
            TextName.Text = DetailAchievement["Name"].AsString;
            TextRecive.Text = DetailAchievement["Recive"].ToLocalTime().ToString();

            //action delete achievement
            BTNRemove.MouseDown += async (s, e) =>
             {
                 if (await DashboardGame.DialogYesNo("All Value in this section will be lost.  are you sure? ") == System.Windows.MessageBoxResult.Yes)
                 {
                     DetailAchievement.Remove("Recive");

                     Editor.RemoveAchievements(DetailAchievement);

                 }
                 else
                 {
                     DashboardGame.Notifaction("Canceled", Notifaction.StatusMessage.Warrning);
                 }
             };


            //copy TOken
            TextToken.MouseDown += GlobalEvents.CopyText;

        }
    }
}
