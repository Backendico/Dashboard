using Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements;
using Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using RestSharp;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements
{
    /// <summary>
    /// Interaction logic for ModelStore.xaml
    /// </summary>
    public partial class ModelStore : UserControl
    {
        public ModelStore(BsonDocument Detail)
        {
            InitializeComponent();
            TextName.Text = Detail["Name"].AsString;
            TextDescription.Text = Detail["Description"].AsString.Length >= 1 ? Detail["Description"].AsString : "No Description";
            TextProduct.Text = Detail["Products"].AsBsonArray.Count.ToString();
            TextCreated.Text = Detail["Created"].ToLocalTime().ToString();

            TextToken.Text = Detail["Token"].ToString();
            TextToken.MouseDown += GlobalEvents.CopyText;

            if (Detail["IsActive"].AsBoolean)
            {
                IsActive1.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                IsActive2.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                IsActive1.BorderBrush = new SolidColorBrush(Colors.Tomato);
                IsActive2.Background = new SolidColorBrush(Colors.Tomato);
            }

            if (Detail["AvatarLink"].AsString.Length >= 1)
            {
                var image = new Image();
                var fullFilePath = Detail["AvatarLink"].ToString();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                image.Source = bitmap;
                PlaceAvatar.Child = image;
            }
            else
            {
                PlaceAvatar.Child = new TextBlock()
                {
                    Text = "\xEB9F",
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    FontSize = 30,
                    ToolTip = "No Avatar"
                };
            }

            //action btn edit 
            BTNEdit.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new EditShop.EditShop());
            };

            RefreshTags();

            void RefreshTags()
            {
                PlaceTags.Children.Clear();

                if (Detail["Tags"].AsBsonArray.Count == 0)
                {
                    PlaceTags.Children.Add(new TextBlock() { Text = "No Tag", Foreground = new SolidColorBrush(Colors.Black) });
                }
                if (Detail["Tags"].AsBsonArray.Count == 1)
                {
                    PlaceTags.Children.Add(new ModelTag(Detail["Tags"][0].AsString));
                }
                else
                {
                    for (int i = 0; i < Detail["Tags"].AsBsonArray.Count; i++)
                    {
                        if (i + 1 == Detail["Tags"].AsBsonArray.Count)
                        {
                            PlaceTags.Children.Add(new ModelTag(Detail["Tags"][i].AsString));
                        }
                        else
                        {

                            PlaceTags.Children.Add(new ModelTag(Detail["Tags"][i].AsString));
                            PlaceTags.Children.Add(new TextBlock()
                            {
                                Text = " | ",
                                Foreground = new SolidColorBrush(Colors.Black)

                            });
                        }


                    }

                }

            }

        }
    }
}
