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

namespace Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements
{
    /// <summary>
    /// Interaction logic for ModelAchivments.xaml
    /// </summary>
    public partial class ModelAchivments : UserControl
    {
        public ModelAchivments(BsonDocument DetailAchievement)
        {
            InitializeComponent();
            TextName.Text = DetailAchievement["Name"].AsString;
            TextToken.Text = DetailAchievement["Token"].ToString();
            TextCreated.Text = DetailAchievement["Created"].ToLocalTime().ToString();
            TextValue.Text = DetailAchievement["Value"].ToString();

        }
    }
}
