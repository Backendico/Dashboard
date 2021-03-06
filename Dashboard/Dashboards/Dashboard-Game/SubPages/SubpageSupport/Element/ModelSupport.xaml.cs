﻿using MongoDB.Bson;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element
{
    /// <summary>
    /// Interaction logic for ModelSupport.xaml
    /// </summary>
    public partial class ModelSupport : UserControl
    {

        public ModelSupport(BsonDocument Detail, SubpageSupport ParentClass)
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


            if (Detail["IsReport"].AsBoolean)
            {
                TextPart.Text = "";
                TextPart.Inlines.Add(new TextBlock() { FontSize = 15, Foreground = new SolidColorBrush(Colors.Tomato), FontFamily = new FontFamily("Segoe MDL2 Assets"), Text = "\xEBE8" });
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
