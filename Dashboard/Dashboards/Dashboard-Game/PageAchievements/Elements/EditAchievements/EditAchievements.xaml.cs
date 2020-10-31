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

namespace Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements.EditAchievements
{
    /// <summary>
    /// Interaction logic for EditAchievements.xaml
    /// </summary>
    public partial class EditAchievements : UserControl
    {
        public EditAchievements(BsonDocument DetailAchievement,Action RefreshList)
        {
            InitializeComponent();

            TextAchievementsID.Text = DetailAchievement["Token"].ToString();
            TextValue.Text = DetailAchievement["Value"].ToString();
            TextName_Achievements.Text = DetailAchievement["Name"].AsString;
            TextCreated.Text = DetailAchievement["Created"].ToLocalTime().ToString();
            TextToken.Text = DetailAchievement["Token"].ToString();


            //actions
            //copy token
            TextToken.MouseDown += GlobalEvents.CopyText;

            //action btnsave
            BTNSaveSetting.MouseDown += (s, e) =>
            {

                try
                {
                    long.Parse(TextValue.Text);

                    DetailAchievement["Value"] = long.Parse(TextValue.Text);
                    
                    SDK.SDK_PageDashboards.DashboardGame.PageAchievements.EditAchievements(DetailAchievement["Token"].AsObjectId, DetailAchievement, result =>
                    {
                        if (result)
                        {
                            RefreshList();
                            DashboardGame.Notifaction("Updated", Notifaction.StatusMessage.Ok);
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Update", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }
            };

        }

        void ChangePage(object sender, RoutedEventArgs e)
        {

        }

        public void Close(object Sender, RoutedEventArgs E)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }


    }
}
