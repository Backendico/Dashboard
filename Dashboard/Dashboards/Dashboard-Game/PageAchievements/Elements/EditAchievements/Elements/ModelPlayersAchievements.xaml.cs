using Dashboard.GlobalElement;
using MongoDB.Bson;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements.EditAchievements.Elements
{
    /// <summary>
    /// Interaction logic for ModelPlayersAchievements.xaml
    /// </summary>
    public partial class ModelPlayersAchievements : UserControl
    {
        public ModelPlayersAchievements(BsonDocument DetailPlayer)
        {
            InitializeComponent();
            TextToken.Text = DetailPlayer["Token"].ToString();
            TextUsername.Text = DetailPlayer["Username"].ToString() == "" ? "Not Set" : DetailPlayer["Username"].ToString();

            TextToken.MouseDown += GlobalEvents.CopyText;

            BTNRemove.MouseDown += (s, e) =>
            {

            };
        }
    }
}
