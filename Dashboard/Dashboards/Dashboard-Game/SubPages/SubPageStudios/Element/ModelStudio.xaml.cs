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

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios.Element
{
    /// <summary>
    /// Interaction logic for ModelStudio.xaml
    /// </summary>
    public partial class ModelStudio : UserControl
    {
        public ModelStudio(BsonDocument DetailStudio)
        {
            InitializeComponent();
            TextToken.Text = DetailStudio["Token"].ToString();

            TextName.Text = DetailStudio["Name"].ToString();
        }
    }
}
