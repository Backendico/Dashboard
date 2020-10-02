using MongoDB.Bson;
using System.Diagnostics;
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
        Grid PageDetail;
        public ModelSupport(BsonDocument Detail,Grid PageDetail)
        {
            InitializeComponent();
            this.PageDetail = PageDetail;

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

        private void OpenDetail(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Parent as StackPanel).Visibility = Visibility.Collapsed;
            PageDetail.Visibility = Visibility.Visible;
            PageDetail.Width = 300;
        }
    }
}
