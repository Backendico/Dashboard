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
            #region Global

            InitializeComponent();

            DetailStore = Detail;

            TextAvatar.Text = DetailStore["AvatarLink"].AsString;
            TextName.Text = DetailStore["Name"].AsString;
            TextDescription.Text = DetailStore["Description"].AsString;
            TextMarketLink.Text = DetailStore["MarketLink"].AsString;
            IsActive.IsChecked = DetailStore["IsActive"].AsBoolean;
            TextToken.Text = DetailStore["Token"].ToString();
            TextPurdocts.Text = DetailStore["Products"].AsBsonArray.Count.ToString();
            TextCreated.Text = DetailStore["Created"].ToLocalTime().ToString();
            TextTagCount_Setting.Text = DetailStore["Tags"].AsBsonArray.Count.ToString();
            Tag_Setting.MouseDown += (s, e) =>
            {
                new TagsSystem(DetailStore["Tags"].AsBsonArray, () =>
                {
                    TextTagCount_Setting.Text = DetailStore["Tags"].AsBsonArray.Count.ToString();
                });
            };



            if (DetailStore["AvatarLink"].AsString.Length >= 1)
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

            MouseMove += (s, e) =>
            {
                Focus();
            };
            #endregion



            #region Setting
            //controll and inject avatar
            TextAvatar.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinkImages(s)&&TextAvatar.Text.Length>=1)
                {
                    DetailStore["AvatarLink"] = TextAvatar.Text;
                }
                else
                {
                    if (TextAvatar.Text.Length==0)
                    {
                        DetailStore["AvatarLink"] = TextAvatar.Text;
                    }
                }
            };
            //controll and inject name
            TextName.TextChanged += (s, e) =>
            {
                if (TextName.Text.Length>=1)
                {
                    DetailStore["Name"] = TextName.Text;
                }
                else
                {
                    TextName.Text = DetailStore["Name"].AsString;
                    DashboardGame.Notifaction("Name is Short", Notifaction.StatusMessage.Error);
                }
            };
            //control and inject marketlink 
            TextMarketLink.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinks(s))
                {
                    DetailStore["MarketLink"] = TextMarketLink.Text;
                }
            };
           //change Description
            TextDescription.TextChanged += (sender, e) =>
            {
                DetailStore["Description"] = TextDescription.Text;
            };
            //Active Control 
            IsActive.Checked += (s, e) =>
            {
                DetailStore["IsActive"] = IsActive.IsChecked.Value;
            };

            //acaton SaveSetting
            BTNSaveSetting.MouseDown += (s, e) =>
            {
                //control avatar
                if (GlobalEvents.ControllLinkImages(TextAvatar) && TextAvatar.Text.Length >= 1)
                {
                    DetailStore["AvatarLink"] = TextAvatar.Text;
                }
                else
                {
                    if (TextAvatar.Text.Length == 0)
                    {
                        DetailStore["AvatarLink"] = TextAvatar.Text;
                    }
                }


                //control link market
                if (GlobalEvents.ControllLinks(TextMarketLink))
                {
                    DetailStore["MarketLink"] = TextMarketLink.Text;
                }

                Save();
            };

            TextToken.MouseDown += GlobalEvents.CopyText;

            #endregion


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

            //action show panel add product
            BTNShowPanelAdd.MouseDown += (s, e) =>
            {
                ShowPanelAddProduct();
            };

            //showoff panel add product
            PanelAddProduct.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPanelAddProduct();
                }
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
                SDK.SDK_PageDashboards.DashboardGame.PageStore.AddProduct(DetailStore["Token"].AsObjectId, NewProduct, result =>
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



        public void Save()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStore.SaveStore(DetailStore["Token"].AsObjectId, DetailStore, result =>
            {
                if (result)
                {

                    DashboardGame.Notifaction("Saved", Notifaction.StatusMessage.Ok);
                }
                else
                {
                    DashboardGame.Notifaction("Not Save", Notifaction.StatusMessage.Error);
                }
            });
        }

        public void Delete(ObjectId Token)
        {

        }
    }

    public interface EditProducts
    {
        void Save();
        void Delete(ObjectId Token);
    }
}
