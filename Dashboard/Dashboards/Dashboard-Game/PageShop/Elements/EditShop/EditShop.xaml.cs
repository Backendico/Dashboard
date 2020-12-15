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
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;
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
    public partial class EditShop : UserControl, IEditStore
    {
        IStoreSetting Settings;

        Button BTNCurent;
        Grid PageCurent;

        BsonDocument NewProduct = new BsonDocument
            {
                {"Name",""},
                {"Count",0},
                {"Amount",0 },
                {"Price",0 },
                {"Avatar","" },
                {"Market","" },
                {"Description" ,""},
                {"Tags",new BsonArray() },
                {"IsExpiration",false},
                {"Expiration", SettingUser.ServerTime.AddDays(3)},
                {"Token" ,""},
                {"Created",SettingUser.ServerTime },
            };

        public BsonDocument DetailStore { get; set; }


        public EditShop(IStoreSetting Settings)
        {
            #region Global

            InitializeComponent();

            this.Settings = Settings;
            DetailStore = this.Settings.DetailStore;

            PageCurent = PanelSetting;
            BTNCurent = BTNSetting;

            MouseMove += (s, e) =>
            {
                Focus();
            };
            #endregion


            #region Setting

            InitSetting();

            //controll and inject avatar
            TextAvatar.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinkImages(s) && TextAvatar.Text.Length >= 1)
                {
                    var Photo = new Image();

                    var ImageBit = new BitmapImage();

                    ImageBit.DownloadFailed += (s1, e1) =>
                    {
                        Settings.DetailStore["AvatarLink"] = "";
                        TextAvatar.Text = "";
                        DashboardGame.Notifaction("Image not found", Notifaction.StatusMessage.Error);

                        PlaceAvatar.Child = null;
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

                    };

                    ImageBit.DownloadCompleted += (s2, e2) =>
                    {
                        Settings.DetailStore["AvatarLink"] = TextAvatar.Text;
                        PlaceAvatar.Child = null;
                        PlaceAvatar.Child = Photo;
                    };

                    ImageBit.BeginInit();
                    ImageBit.UriSource = new Uri(TextAvatar.Text, UriKind.Absolute);
                    Photo.Source = ImageBit;

                    ImageBit.EndInit();

                }
                else
                {
                    if (TextAvatar.Text.Length == 0)
                    {
                        Settings.DetailStore["AvatarLink"] = TextAvatar.Text;
                    }
                }
            };
            //controll and inject name
            TextName.TextChanged += (s, e) =>
            {
                if (TextName.Text.Length >= 1)
                {
                    Settings.DetailStore["Name"] = TextName.Text;
                }
                else
                {
                    TextName.Text = Settings.DetailStore["Name"].AsString;
                    DashboardGame.Notifaction("Name is Short", Notifaction.StatusMessage.Error);
                }
            };
            //control and inject marketlink 
            TextMarketLink.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinks(s))
                {
                    Settings.DetailStore["MarketLink"] = TextMarketLink.Text;
                }
            };
            //change Description
            TextDescription.TextChanged += (sender, e) =>
            {
                Settings.DetailStore["Description"] = TextDescription.Text;
            };

            //Active Control 
            IsActive.Checked += (s, e) =>
            {
                this.Settings.DetailStore["IsActive"] = true;
            };
            IsActive.Unchecked += (s, e) =>
            {
                this.Settings.DetailStore["IsActive"] = false;
            };

            //acaton SaveSetting
            BTNSaveSetting.MouseDown += (s, e) =>
            {
                //control avatar
                if (GlobalEvents.ControllLinkImages(TextAvatar) && TextAvatar.Text.Length >= 1)
                {
                    Settings.DetailStore["AvatarLink"] = TextAvatar.Text;
                }
                else
                {
                    if (TextAvatar.Text.Length == 0)
                    {
                        Settings.DetailStore["AvatarLink"] = TextAvatar.Text;
                    }
                }


                //control link market
                if (GlobalEvents.ControllLinks(TextMarketLink))
                {
                    Settings.DetailStore["MarketLink"] = TextMarketLink.Text;
                }

                Settings.Save();
                InitSetting();
            };

            TextToken.MouseDown += GlobalEvents.CopyText;

            #endregion

            #region SubPage AddProducts

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

            //change name
            TextNameProduct_AddProduct.TextChanged += (s, e) =>
            {
                NewProduct["Name"] = TextNameProduct_AddProduct.Text;
            };

            //Change count int
            TextCount_AddProducts.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextCount_AddProducts))
                {
                    NewProduct["Count"] = TextCount_AddProducts.Text;
                }
                else
                {
                    NewProduct["Count"] = 0;
                }
            };

            //change Amount
            TextAmount_AddProducts.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextAmount_AddProducts))
                {
                    NewProduct["Amount"] = TextAmount_AddProducts.Text;
                }
                else
                {
                    NewProduct["Amount"] = 0;
                }
            };

            //Change Price
            TextPrice_AddProduct.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextPrice_AddProduct))
                {
                    NewProduct["Price"] = TextPrice_AddProduct.Text;
                }
                else
                {
                    NewProduct["Price"] = 0;
                }

            };

            //cheack Avatar
            TextAvatar_AddProduct.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinkImages(s))
                {
                    NewProduct["Avatar"] = TextAvatar_AddProduct.Text;
                }
            };

            //cheack market link
            TextMarketLink_AddProduct.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinks(s))
                {
                    NewProduct["Market"] = TextMarketLink_AddProduct.Text;
                }
            };

            //change description
            TextDescription_AddDescription.TextChanged += (s, e) =>
            {
                NewProduct["Description"] = TextDescription_AddDescription.Text;
            };


            // expire cheack
            IsExpiraton_AddProducts.Checked += (s, e) =>
            {
                Calendar_Expire_AddProducts.Visibility = Visibility.Visible;
                NewProduct["IsExpiration"] = true;
            };
            IsExpiraton_AddProducts.Unchecked += (s, e) =>
            {
                Calendar_Expire_AddProducts.Visibility = Visibility.Collapsed;
                NewProduct["IsExpiration"] = false;
            };

            //event open tag system
            TagSystem.MouseDown += (s, e) =>
            {
                new TagsSystem(NewProduct["Tags"].AsBsonArray, () =>
                {
                    TextTagsCount.Text = NewProduct["Tags"].AsBsonArray.Count.ToString();
                });
            };

            //cheack calender
            Calendar_Expire_AddProducts.SelectedDatesChanged += (s, e) =>
            {
                if (Calendar_Expire_AddProducts.SelectedDate > SettingUser.ServerTime)
                {

                    NewProduct["Expiration"] = Calendar_Expire_AddProducts.SelectedDate;
                }
                else
                {
                    NewProduct["Expiration"] = SettingUser.ServerTime.AddDays(3);
                }
            };

            //action btn add tag
            BTNAddProduct.MouseDown += (s, e) =>
            {
                NewProduct["Token"] = ObjectId.GenerateNewId();
                NewProduct["Created"] = SettingUser.ServerTime;


                BsonDocument NewProduct1 = new BsonDocument
            {
                {"Name",""},
                {"Count",0},
                {"Amount",0 },
                {"Price",0 },
                {"Avatar","" },
                {"Market","" },
                {"Description" ,""},
                {"Tags",new BsonArray() },
                {"IsExpiration",false},
                {"Expiration", SettingUser.ServerTime.AddDays(3)},
                {"Token" ,ObjectId.GenerateNewId()},
                {"Created",SettingUser.ServerTime }
            };

                this.Settings.DetailStore["Products"].AsBsonArray.Add(NewProduct);
                this.Settings.Save();
                ShowoffPanelAddProduct();
                InitSetting();
                ReciveProduct();
                NewProduct = NewProduct1;
            };
            #endregion

        }

        #region Setting
        public void InitSetting()
        {
            TextStoreName.Text = Settings.DetailStore["Name"].ToString();
            TextAvatar.Text = Settings.DetailStore["AvatarLink"].AsString;
            TextName.Text = Settings.DetailStore["Name"].AsString;
            TextDescription.Text = Settings.DetailStore["Description"].AsString;
            TextMarketLink.Text = Settings.DetailStore["MarketLink"].AsString;
            IsActive.IsChecked = Settings.DetailStore["IsActive"].AsBoolean;
            TextToken.Text = Settings.DetailStore["Token"].ToString();
            TextPurdocts.Text = Settings.DetailStore["Products"].AsBsonArray.Count.ToString();
            TextCreated.Text = Settings.DetailStore["Created"].ToLocalTime().ToString();
            TextTagCount_Setting.Text = Settings.DetailStore["Tags"].AsBsonArray.Count.ToString();
            Tag_Setting.MouseDown += (s, e) =>
            {
                new TagsSystem(Settings.DetailStore["Tags"].AsBsonArray, () =>
                {
                    TextTagCount_Setting.Text = Settings.DetailStore["Tags"].AsBsonArray.Count.ToString();
                });
            };

            if (Settings.DetailStore["IsActive"].AsBoolean)
            {
                IsActive.IsChecked = true;
            }
            else
            {
                IsActive.IsChecked = false;
            }

            if (Settings.DetailStore["AvatarLink"].AsString.Length >= 1)
            {
                var image = new Image();
                var fullFilePath = Settings.DetailStore["AvatarLink"].ToString();

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

        #endregion


        #region Product
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

            var Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
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

        //PageProduct
        public void ReciveProduct()
        {
            PlaceProducts.Children.Clear();
            if (Settings.DetailStore["Products"].AsBsonArray.Count >= 1)
            {
                foreach (var item in Settings.DetailStore["Products"].AsBsonArray)
                {
                    PlaceProducts.Children.Add(new ModelProduct.ModelProduct(item.AsBsonDocument, this));
                }
            }
            else
            {
                ShowPanelAddProduct();
            }
        }

        #endregion

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

        public void Close(object S, RoutedEventArgs Event)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }

        public void SaveSetting()
        {
            Settings.Save();
            InitSetting();
        }
    }

    public interface IEditStore
    {
        BsonDocument DetailStore { get; set; }
        void InitSetting();
        void SaveSetting();

    }
}
