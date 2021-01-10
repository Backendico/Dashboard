using Dashboard.GlobalElement;
using MongoDB.Bson;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for ContentValue.xaml
    /// </summary>
    public partial class ContentValue : UserControl
    {
        public ContentValue(BsonDocument DetailValue, IEditorLeaderboard Editor)
        {
            InitializeComponent();

            TextToken.Text = DetailValue["Leaderboards"]["Token"].ToString();
            TextUsername.Text = DetailValue["Leaderboards"]["Username"].ToString();
            TextValue.Text = DetailValue["Leaderboards"]["Score"].ToString();
            TextRank.Text = DetailValue["Rank"].ToString();

            TextToken.MouseDown += GlobalEvents.CopyText;

            BTNRemove.MouseDown += async (s, e) =>
            {
                if (await DashboardGame.DialogYesNo("The value for the user is deleted \n Are you sure?") == MessageBoxResult.Yes)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Remove(DetailValue["Leaderboards"]["Token"].ToString(), DetailValue["Leaderboards"]["Leaderboard"].ToString(),
                        () =>
                        {
                            Visibility = Visibility.Collapsed;
                            DashboardGame.Notifaction("Deleted", Notifaction.StatusMessage.Ok);

                            Editor.DetailLeaderboard["Settings"]["Count"] = (Editor.DetailLeaderboard["Settings"]["Count"].ToInt32() - 1);

                            Editor.Save();
                        },
                        () =>
                        {
                            DashboardGame.Notifaction("Faild Delete", Notifaction.StatusMessage.Error);
                        });
                }
                else
                {
                    DashboardGame.Notifaction("Delete reject", Notifaction.StatusMessage.Error);
                }
            };
        }


    }
}
