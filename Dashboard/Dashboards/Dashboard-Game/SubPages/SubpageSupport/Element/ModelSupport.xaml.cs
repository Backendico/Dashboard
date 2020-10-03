using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element
{
    /// <summary>
    /// Interaction logic for ModelSupport.xaml
    /// </summary>
    public partial class ModelSupport : UserControl
    {

        public ModelSupport(BsonDocument Detail, Grid PageDetail, SubpageSupport ParentClass)
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

            //init mouse down 
            MouseDown += (s, e) =>
            {
                ParentClass.CurentSupport = Detail;
                ParentClass.OpenEachSupport();
            };
        }
    }
}
