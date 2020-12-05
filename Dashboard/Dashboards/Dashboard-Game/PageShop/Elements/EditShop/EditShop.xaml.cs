using Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem;
using Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop.ModelProduct;
using Dashboard.Dashboards.Dashboard_Game.SubPages;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop
{
    /// <summary>
    /// Interaction logic for EditShop.xaml
    /// </summary>
    public partial class EditShop : UserControl, EditProducts
    {
        BsonDocument DetailStore;

        Button BTNCurent;
        Grid PageCurent;




        public EditShop(BsonDocument Detail)
        {
            InitializeComponent();

            DetailStore = Detail;

            TextAvatar.Text = Detail["AvatarLink"].AsString;
            TextName.Text = Detail["Name"].AsString;
            TextDescription.Text = Detail["Description"].AsString;
            TextMarketLink.Text = Detail["MarketLink"].AsString;
            IsActive.IsChecked = Detail["IsActive"].AsBoolean;
            TextToken.Text = Detail["Token"].ToString();
            TextPurdocts.Text = Detail["Products"].AsBsonArray.Count.ToString();
            TextCreated.Text = Detail["Created"].ToLocalTime().ToString();

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

            PageCurent = PanelSetting;
            BTNCurent = BTNSetting;



            //acaton SaveSetting
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
                    if (TextName.Text.Length >= 1)
                    {
                        Detail["Name"] = TextName.Text;
                    }
                    else
                    {
                        throw new Exception("Name Short");
                    }

                    //valid market
                    if (TextMarketLink.Text.Length >= 1)
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

            TextToken.MouseDown += GlobalEvents.CopyText;

            #region SubPage

            BsonDocument NewProduct = new BsonDocument
            {
                {"Name","" },
                {"Tags",new BsonArray() }

            };

            // expire cheack
            IsExpiraton.Checked += (s, e) =>
            {
                Calendar_Expire_AddProducts.Visibility = Visibility.Visible;
            };
            IsExpiraton.Unchecked += (s, e) =>
            {
                Calendar_Expire_AddProducts.Visibility = Visibility.Collapsed;
            };

            //cheack count int
            TextCount_AddProducts.LostFocus += (s, e) =>
            {
                GlobalEvents.ControllNumberFilde(s as TextBox);
            };
            TextAmount_AddProducts.LostFocus += (s, e) =>
            {
                GlobalEvents.ControllNumberFilde(s as TextBox);
            };
            TextPrice_AddProduct.LostFocus += (s, e) =>
            {
                GlobalEvents.ControllNumberFilde(s as TextBox);
            };
            //cheack Image
            TextAvatar_AddProduct.LostFocus += (s, e) =>
            {
                GlobalEvents.ControllLinkImages(s);
            };
            TextMarketLink_AddProduct.LostFocus += (s, e) =>
            {
                GlobalEvents.ControllLinkImages(s);
            };

            //event open tag system
            TagSystem.MouseDown += (s, e) =>
            {
                new TagsSystem(NewProduct["Tags"].AsBsonArray);
            };

            //action btn add tag
            BTNAddProduct.MouseDown += (s, e) =>
            {
                Debug.WriteLine(NewProduct);
            };
            #endregion

        }

        public void Close(object S, RoutedEventArgs Event)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }

        internal void ChangePage(object s, RoutedEventArgs eventArgs)
        {
            var SelectedBTN = s as Button;

            BTNCurent.BorderBrush = new SolidColorBrush(Colors.Transparent);
            PageCurent.Visibility = Visibility.Collapsed;

            if (SelectedBTN.Name == BTNSetting.Name)
            {
                BTNCurent = BTNSetting;
                PageCurent = PanelSetting;
            }
            else if (SelectedBTN.Name == BTNProduct.Name)
            {
                BTNCurent = BTNProduct;
                PageCurent = PanelProduct;
                ReciveProduct();
            }
            else if (SelectedBTN.Name == BTNPayments.Name)
            {
                BTNCurent = BTNPayments;
                PageCurent = PanelPayments;
            }

            PageCurent.Visibility = Visibility.Visible;
            BTNCurent.BorderBrush = new SolidColorBrush(Colors.Orange);


        }


        //PageProduct
        public void ReciveProduct()
        {
            Debug.WriteLine(DetailStore["Products"]);
            foreach (var item in DetailStore["Products"].AsBsonArray)
            {
                PlaceProducts.Children.Add(new ModelProduct.ModelProduct(item.AsBsonDocument, this));
            }
        }


        public void Save(BsonDocument Detail)
        {

        }

        public void Delete(ObjectId Token)
        {

        }
    }

    public interface EditProducts
    {
        void Save(BsonDocument Detail);
        void Delete(ObjectId Token);
    }
}
