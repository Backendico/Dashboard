using MongoDB.Bson;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLog
{
    /// <summary>
    /// Interaction logic for ModelLogPlayer.xaml
    /// </summary>
    public partial class ModelLogPlayer : UserControl
    {
        public ModelLogPlayer(BsonDocument LogDetail)
        {
            InitializeComponent();

            TextHeader.Text = LogDetail["Header"].AsString;
            TextDescription.Text = LogDetail["Description"].AsString;
            TextTime.Text = LogDetail["Time"].ToLocalTime().ToString();
        }
    }
}
