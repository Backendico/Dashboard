using MongoDB.Bson;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelAchievements
{
    /// <summary>
    /// Interaction logic for ModelAchievementStudio.xaml
    /// </summary>
    public partial class ModelAchievementStudio : UserControl
    {
        public ModelAchievementStudio(BsonDocument Detail, IEditAchievements Editor)
        {
            InitializeComponent();
            TextAchievement.Text = Detail["Name"].ToString();
            TextValue.Text = Detail["Value"].ToString();


            BTNAddAchievement.MouseDown += (s, e) =>
            {
                Editor.AddAchievements(Detail);
            };
        }
    }
}
