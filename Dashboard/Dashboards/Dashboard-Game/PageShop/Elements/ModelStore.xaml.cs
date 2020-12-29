using Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonEditor;
using Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonView;
using Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements
{
    public partial class ModelStore : UserControl, IStoreSetting
    {

        public ModelStore(BsonDocument Detail)
        {
            InitializeComponent();
            DetailStore = Detail;

            Update();

            //action btn edit 
            BTNEdit.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new EditShop.EditShop(this));
            };

            //action show tag view
            Tags.MouseDown += (s, e) =>
            {
                new TagsSystem(Detail["Tags"].AsBsonArray);
            };

            //action btn delete
            BTNDelete.MouseDown += async (s, e) =>
            {
                Debug.WriteLine(Parent.GetType());
                if (await DashboardGame.DialogYesNo("All information will be lost Are you sure ? ") == MessageBoxResult.Yes)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageStore.RemoveStore(Detail, Result =>
                    {
                        if (Result)
                        {
                            DashboardGame.Notifaction("Store Deleted", Notifaction.StatusMessage.Ok);
                            (Parent as WrapPanel).Children.Remove(this);

                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Delete", Notifaction.StatusMessage.Ok);
                        }

                    });
                }
                else
                {
                    DashboardGame.Notifaction("Canceled", Notifaction.StatusMessage.Warrning);
                }
            };
        }

        public BsonDocument DetailStore { get; set; }


        public void Save()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStore.SaveStore(DetailStore["Token"].AsObjectId, DetailStore, result =>
            {
                if (result)
                {
                    DashboardGame.Notifaction("Saved", Notifaction.StatusMessage.Ok);
                    Update();
                }
                else
                {
                    DashboardGame.Notifaction("Not Save", Notifaction.StatusMessage.Error);
                    Update();
                }
            });
        }

        public void Update()
        {

            TextName.Text = DetailStore["Name"].AsString;
            TextDescription.Text = DetailStore["Description"].AsString.Length >= 1 ? DetailStore["Description"].AsString : "No Description";
            TextProduct.Text = DetailStore["Products"].AsBsonArray.Count.ToString();
            TextCreated.Text = DetailStore["Created"].ToLocalTime().ToString();

            TextToken.Text = DetailStore["Token"].ToString();
            TextToken.MouseDown += GlobalEvents.CopyText;

            if (DetailStore["IsActive"].AsBoolean)
            {
                IsActive1.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                IsActive2.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                IsActive1.BorderBrush = new SolidColorBrush(Colors.Tomato);
                IsActive2.Background = new SolidColorBrush(Colors.Tomato);
            }

            if (DetailStore["AvatarLink"].AsString.Length >= 1)
            {
                var image = new Image();
                var fullFilePath = DetailStore["AvatarLink"].ToString();

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

            TagCount.Text = DetailStore["Tags"].AsBsonArray.Count.ToString();
        }

    }

    public interface IStoreSetting
    {
        void Save();
        void Update();
        BsonDocument DetailStore { get; set; }

    }
}
