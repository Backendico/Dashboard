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

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element
{
    /// <summary>
    /// Interaction logic for ModelSupport.xaml
    /// </summary>
    public partial class ModelSupport : UserControl
    {
        public ModelSupport(BsonDocument Detail)
        {
            InitializeComponent();

            TextHeader.Text = Detail["Header"].ToString();
            if (Detail["IsOpen"].AsBoolean)
            {
                //init part
                switch (Detail["Part"].AsInt32)
                {
                    case 0:
                        {
                            TextPart.Text = "Other";
                        }
                        break;
                    case 1:
                        {
                            TextPart.Text = "Authentication";
                        }
                        break;
                    case 2:
                        {
                            TextPart.Text = "Dashboard";
                        }
                        break;
                    case 3:
                        {
                            TextPart.Text = "Payments";
                        }
                        break;



                }
                //init Priority
                switch (Detail["Priority"].AsInt32)
                {
                    case 0:
                        Priority.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                        break;
                    case 1:
                        Priority.BorderBrush = new SolidColorBrush(Colors.Orange);
                        break;
                    case 2:
                        Priority.BorderBrush = new SolidColorBrush(Colors.Tomato);
                        break;
                }
            }
            else
            {
                TextHeader.Text += "(Closed)";
                TextHeader.TextDecorations = TextDecorations.Strikethrough;
                TextTime.Visibility = Visibility.Collapsed;
                TextPart.Visibility = Visibility.Collapsed;
            }

            TextTime.Text = Detail["Created"].ToLocalTime().ToString();
        }
    }
}
