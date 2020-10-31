using MongoDB.Bson;
using System;
using System.Collections.Generic;
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

        }
    }
}
