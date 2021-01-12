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

namespace Dashboard.Dashboards.Dashboard_Game.PageKeyValue.Elements
{
    /// <summary>
    /// Interaction logic for ModelKeyValue.xaml
    /// </summary>
    public partial class ModelKeyValue : UserControl
    {
        public ModelKeyValue(BsonDocument Detail)
        {
            InitializeComponent();

        }
    }
}
