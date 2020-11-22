using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop
{
    /// <summary>
    /// Interaction logic for EditShop.xaml
    /// </summary>
    public partial class EditShop : UserControl
    {
        public EditShop(BsonDocument Detail)
        {
            InitializeComponent();

            TextAvatar.Text = Detail["AvatarLink"].AsString;
            TextName.Text = Detail["Name"].AsString;
            TextDescription.Text = Detail["Description"].AsString;
            TextMarketLink.Text = Detail["MarketLink"].AsString;
            IsActive.IsChecked = Detail["IsActive"].AsBoolean;
            TextToken.Text = Detail["Token"].ToString();
            TextPurdocts.Text = Detail["Products"].AsBsonArray.Count.ToString();
            TextCreated.Text = Detail["Created"].ToLocalTime().ToString();

            BTNSaveSetting.MouseDown += (s, e) =>
            {
                try
                {
                    //valid links
                    if (TextAvatar.Text.Length >= 1)
                    {
                        var NewLink = new Uri(TextAvatar.Text);

                        if (TextAvatar.Text.Contains(".png"))
                        {
                            Detail["AvatarLink"] = TextAvatar.Text;
                        }
                        else if (TextAvatar.Text.Contains(".gif"))
                        {
                            Detail["AvatarLink"] = TextAvatar.Text;
                        }
                        else if (TextAvatar.Text.Contains(".jpg"))
                        {
                            Detail["AvatarLink"] = TextAvatar.Text;
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        Debug.WriteLine(Detail["AvatarLink"]);

                    }

                    //valid name
                    if (TextName.Text.Length>=1)
                    {
                        Detail["Name"] = TextName.Text;
                    }
                    else
                    {
                        throw new Exception("Name Short");
                    }

                    //valid market
                    if (TextMarketLink.Text.Length>=1)
                    {
                        new Uri(TextMarketLink.Text);

                        Detail["MarketLink"] = TextMarketLink.Text;
                    }
                    else
                    {
                        Detail["MarketLink"] = TextMarketLink.Text;
                    }

                    Detail["IsActive"] = IsActive.IsChecked.Value;

                    Debug.WriteLine(Detail);
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }
            };

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


        }

        public void Close(object S, RoutedEventArgs Event)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }

        internal void ChangePage(object s, RoutedEventArgs eventArgs)
        {

        }
    }


}
