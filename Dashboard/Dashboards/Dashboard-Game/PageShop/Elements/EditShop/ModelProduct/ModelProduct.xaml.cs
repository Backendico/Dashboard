using Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop.ModelProduct
{
    /// <summary>
    /// Interaction logic for ModelProduct.xaml
    /// </summary>
    public partial class ModelProduct : UserControl
    {
        IStoreSetting MainSetting;
        BsonDocument DetailProduct;

        public ModelProduct(BsonDocument DetailProduct, IStoreSetting setting)
        {
            InitializeComponent();

            MainSetting = setting;
            this.DetailProduct = DetailProduct;

            var image = new Image();
            var fullFilePath = this.DetailProduct["Avatar"].ToString();

            if (this.DetailProduct["Avatar"].ToString().Length >= 1)
            {

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


            TextCreated.Text = this.DetailProduct["Created"].ToLocalTime().ToString();

            TextToken.Text = this.DetailProduct["Token"].ToString();
            TextToken.MouseDown += GlobalEvents.CopyText;

            TextName.Text = this.DetailProduct["Name"].ToString();
            TextAvatarLink.Text = this.DetailProduct["Avatar"].ToString();
            TextDescription.Text = this.DetailProduct["Description"].ToString();
            TextCount.Text = this.DetailProduct["Count"].ToString();
            TextAmount.Text = this.DetailProduct["Amount"].ToString();
            TextPrice.Text = this.DetailProduct["Price"].ToString();
            TextTags.Text = this.DetailProduct["Tags"].AsBsonArray.Count.ToString();
            if (this.DetailProduct["IsExpiration"].AsBoolean)
            {
                IsExpiration.IsChecked = true;
                Calender.Visibility = Visibility.Visible;
            }
            else
            {
                IsExpiration.IsChecked = false;
                Calender.Visibility = Visibility.Collapsed;
            }

            TextExpiration.Text = this.DetailProduct["Expiration"].ToUniversalTime().Date.ToString();
            Calender.SelectedDate = DetailProduct["Expiration"].ToUniversalTime();



            //action expiration
            IsExpiration.Checked += (s, e) =>
            {
                Calender.Visibility = Visibility.Visible;
                this.DetailProduct["IsExpiration"] = true;
                UpdateProduct();
            };
            IsExpiration.Unchecked += (s, e) =>
            {
                Calender.Visibility = Visibility.Collapsed;
                this.DetailProduct["IsExpiration"] = false;
                UpdateProduct();
            };
            //actionTags
            Tags.MouseDown += (s, e) =>
            {
                new TagsSystem(this.DetailProduct["Tags"].AsBsonArray, () =>
                {
                    UpdateProduct();
                    TextTags.Text = this.DetailProduct["Tags"].AsBsonArray.Count.ToString();
                });
            };


            //change name
            TextName.TextChanged += (s, e) =>
            {
                if (TextName.Text.Length >= 1)
                {
                    this.DetailProduct["Name"] = TextName.Text;
                    UpdateProduct();
                }
                else
                {
                    DashboardGame.Notifaction("Name Is Short", Notifaction.StatusMessage.Error);
                }
            };
            //change avatar
            TextAvatarLink.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinkImages(s))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.DownloadFailed += (s1, e1) =>
                    {
                      this.DetailProduct["Avatar"] = "";
                        UpdateLayout();

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

                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(TextAvatarLink.Text, UriKind.Absolute);
                    bitmap.EndInit();
                    image.Source = bitmap;
                    PlaceAvatar.Child = image;

                    this.DetailProduct["Avatar"] = TextAvatarLink.Text;
                    UpdateLayout();
                }

                UpdateProduct();
            };
            //change Description
            TextDescription.TextChanged += (s, e) =>
            {
                this.DetailProduct["Description"] = TextDescription.Text;
                UpdateProduct();
            };
            //change Count
            TextCount.TextChanged += (s, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextCount))
                {
                    this.DetailProduct["Count"] = int.Parse(TextCount.Text);
                }

                UpdateProduct();
            };
            //change amount
            TextAmount.TextChanged += (s, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextAmount))
                {
                    this.DetailProduct["Amount"] = int.Parse(TextAmount.Text);
                }
                UpdateProduct();
            };
            //change Price
            TextPrice.TextChanged += (s, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextPrice))
                {
                    this.DetailProduct["Price"] = int.Parse(TextPrice.Text);
                }
                UpdateProduct();
            };
            //cahge calender
            Calender.SelectedDatesChanged += (s, e) =>
             {
                 if (Calender.SelectedDate > SettingUser.ServerTime)
                 {
                     this.DetailProduct["Expiration"] = Calender.SelectedDate;
                     TextExpiration.Text = this.DetailProduct["Expiration"].ToUniversalTime().Date.ToString();
                 }
                 else
                 {
                     DashboardGame.Notifaction("Time must be in the future", Notifaction.StatusMessage.Error);
                 }
             };

            //action save Setting
            BTNSave.MouseDown += (s, e) =>
            {
                setting.Save();
            };
        }

        void UpdateProduct()
        {

            for (int i = 0; i < MainSetting.DetailStore["Products"].AsBsonArray.Count; i++)
            {
                if (MainSetting.DetailStore["Products"].AsBsonArray[i].AsBsonDocument["Token"].AsObjectId == DetailProduct["Token"])
                {
                    MainSetting.DetailStore["Products"].AsBsonArray[i] = DetailProduct;
                }
            }
        }
    }
}
