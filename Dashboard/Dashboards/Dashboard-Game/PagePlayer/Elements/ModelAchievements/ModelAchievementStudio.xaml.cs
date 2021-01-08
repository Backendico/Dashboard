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

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelAchievements
{
    /// <summary>
    /// Interaction logic for ModelAchievementStudio.xaml
    /// </summary>
    public partial class ModelAchievementStudio : UserControl
    {
        public ModelAchievementStudio(BsonDocument Detail,IEditAchievements Editor)
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
