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
using System.Windows.Media.Animation;
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
            TextTagCount_Setting.Text = Detail["Tags"].AsBsonArray.Count.ToString();
            Tag_Setting.MouseDown += (s, e) =>
            {
                new TagsSystem(Detail["Tags"].AsBsonArray, () =>
                {
                    TextTagCount_Setting.Text = Detail["Tags"].AsBsonArray.Count.ToString();
                });
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


            //action show panel add product
            BTNShowPanelAdd.MouseDown += (s, e) =>
            {
                ShowPanelAddProduct();
            };

            PanelAddProduct.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPanelAddProduct();
                }
            };


            #region SubPage AddTag

            BsonDocument NewProduct = new BsonDocument
            {
                {"Name","" },
                {"Count",0 },
                {"Amount",0 },
                {"Price",0 },
                {"Avatar","" },
                {"Market","" },
                {"Description" ,""},
                {"Tags",new BsonArray() },
                {"IsExpiraton",false },
                {"Expiraton",DateTime.Now }
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
                new TagsSystem(NewProduct["Tags"].AsBsonArray, () =>
                {
                    TextTagsCount.Text = NewProduct["Tags"].AsBsonArray.Count.ToString();
                });
            };

            //action btn add tag
            BTNAddProduct.MouseDown += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageStore.AddProduct(Detail["Token"].AsObjectId, NewProduct, result =>
                 {
                     Debug.WriteLine(result);
                 });
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

        void ShowPanelAddProduct()
        {
            PanelAddProduct.Visibility = Visibility.Visible;

            var Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddProduct.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard Story = new Storyboard();
            Story.Children.Add(Anim);
            Story.Begin(this);
        }

        void ShowoffPanelAddProduct()
        {

            var Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddProduct.Visibility = Visibility.Collapsed;
            };

            Storyboard.SetTargetName(Anim, PanelAddProduct.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard Story = new Storyboard();
            Story.Children.Add(Anim);
            Story.Begin(this);
        }
        #region Product

        //PageProduct
        public void ReciveProduct()
        {
            foreach (var item in DetailStore["Products"].AsBsonArray)
            {
                PlaceProducts.Children.Add(new ModelProduct.ModelProduct(item.AsBsonDocument, this));
            }
        }

        #endregion

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
