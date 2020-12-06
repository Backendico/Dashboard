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

namespace Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem.Elements
{
    /// <summary>
    /// Interaction logic for ModelTag.xaml
    /// </summary>
    public partial class ModelTag : UserControl
    {
        /// <summary>
        /// for view
        /// </summary>
        /// <param name="Detail"></param>
        public ModelTag(BsonDocument Detail)
        {
            InitializeComponent();

            TextNameTag.Text = Detail["Name"].ToString();
            Color1.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(Detail["Color"].AsString);
            Color2.Background = Color1.Background;
        }

        /// <summary>
        /// for editor
        /// </summary>
        /// <param name="Detail"></param>
        /// <param name="Controler"></param>
        public ModelTag(BsonDocument Detail, IControlTag Controler)
        {
            InitializeComponent();

            TextNameTag.Text = Detail["Name"].ToString();
            Color1.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(Detail["Color"].AsString);
                Color2.Background = Color1.Background;
            MouseDown += (s, e) =>
            {
                Controler.Delete(Detail, this);
            };
        }
    }
}
