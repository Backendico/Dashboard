using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements.EditAchievements;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements
{
    /// <summary>
    /// Interaction logic for ModelAchivments.xaml
    /// </summary>
    public partial class ModelAchivments : UserControl
    {
        public ModelAchivments(BsonDocument DetailAchievement, Action RefreshList)
        {
            InitializeComponent();
            TextName.Text = DetailAchievement["Name"].AsString;
            TextToken.Text = DetailAchievement["Token"].ToString();
            TextCreated.Text = DetailAchievement["Created"].ToLocalTime().ToString();
            TextValue.Text = DetailAchievement["Value"].ToString();
            try
            {

                TextPlayers.Text = DetailAchievement["Players"]["Achievements"].ToString();
            }
            catch (Exception)
            {

                TextPlayers.Text = "(0) No player received ";
            }

            BTNEdit.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new EditAchievements.EditAchievements(DetailAchievement, RefreshList));
            };

            BTNDelete.MouseDown += async (s, e) =>
             {
                 if (await DashboardGame.DialogYesNo("All settings and achievement players are removed \n are you sure ? ") == MessageBoxResult.Yes)
                 {
                     SDK.SDK_PageDashboards.DashboardGame.PageAchievements.RemoveAchievements(DetailAchievement,
                        result =>
                        {
                            if (result)
                            {
                                DashboardGame.Notifaction("Deleted", StatusMessage.Ok);
                                RefreshList();
                            }
                            else
                            {
                                DashboardGame.Notifaction("Faild Delete", StatusMessage.Error);
                            }

                        });
                 }
                 else
                 {
                     DashboardGame.Notifaction("Reject", StatusMessage.Warrning);
                 }
             };

        }
    }
}
