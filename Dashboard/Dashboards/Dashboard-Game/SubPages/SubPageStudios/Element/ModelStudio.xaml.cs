using MongoDB.Bson;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios.Element
{
    /// <summary>
    /// Interaction logic for ModelStudio.xaml
    /// </summary>
    public partial class ModelStudio : UserControl
    {
        public ModelStudio(BsonDocument DetailStudio, SubPageStudios Root)
        {
            InitializeComponent();
            TextToken.Text = DetailStudio["Token"].ToString();

            TextName.Text = DetailStudio["Name"].ToString();

            MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.ChangeStudio(DetailStudio, true);

                DashboardGame.Dashboard.Root.Children.Remove(Root);

            };
        }
    }
}
