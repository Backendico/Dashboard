using Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem;
using Dashboard.Dashboards.Dashboard_Game.PageShop.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PageShop
{
    public partial class PageShop : UserControl
    {
        BsonDocument Detail = new BsonDocument
        {
            {"Name","" },
            {"Description", ""},
            {"MarketLink","" },
            {"AvatarLink","" },
            {"Tags",new BsonArray() },
            {"Products" ,new BsonArray()},
            {"IsActive",true },
            {"Payments",new BsonArray() }

        };


        public PageShop()
        {
            InitializeComponent();

            ReciveStores();

            //acaton add Store
            BTNaddStore.MouseDown += (s, e) =>
            {
                try
                {

                    if (TextboxName.Text.Length <= 5)
                        throw new Exception("Store name Short");

                    if (TextBoxMarketLink.Text.Length >= 1)
                    {
                        try
                        {
                            new Uri(TextBoxMarketLink.Text);

                        }
                        catch (Exception)
                        {
                            DashboardGame.Notifaction("The \" Market link \" is incorrect", Notifaction.StatusMessage.Error);
                        }

                    }


                    if (TextBoxAvatar.Text.Length >= 1)
                    {
                        try
                        {
                            new Uri(TextBoxAvatar.Text);

                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction("The \" Avatar link \" is incorrect", Notifaction.StatusMessage.Error);
                        }

                    }


                    Detail["Name"] = TextboxName.Text;
                    Detail["Description"] = TextBoxDescription.Text;
                    Detail["MarketLink"] = TextBoxMarketLink.Text;
                    Detail["AvatarLink"] = TextBoxAvatar.Text;
                    Detail["IsActive"] = CheackboxActivity.IsChecked.Value;

                    SDK.SDK_PageDashboards.DashboardGame.PageStore.AddStore(Detail, result =>
                    {
                        if (result)
                        {
                            DashboardGame.Notifaction("Store Added", Notifaction.StatusMessage.Ok);
                            ReciveStores();
                            ShowOffSubpageAddStore();
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Add", Notifaction.StatusMessage.Error);
                        }
                    });

                }
                catch (Exception ex)
                {

                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }

            };

            //action close
            PanelAddStore.MouseDown += (s, e) =>
            {

                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffSubpageAddStore();
                }
            };


            #region AddStore

            //btn show panel add store
            BTNShowPanelStore.MouseDown += (s, e) =>
            {
                ShowSubpageAddStore();

            };

            //action btn show tag system
            Tags.MouseDown += (s, e) => {
                new TagsSystem(Detail["Tags"].AsBsonArray, () => {
                    TextTagCount.Text = Detail["Tags"].AsBsonArray.Count.ToString();
                });
            }; 
            #endregion
        
        }

        void ReciveStores()
        {
            PlaceStore.Children.Clear();

            SDK.SDK_PageDashboards.DashboardGame.PageStore.ReciveStore(result =>
            {
                try
                {

                    if (result["Store"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in result["Store"].AsBsonArray)
                        {
                            PlaceStore.Children.Add(new ModelStore(item.AsBsonDocument));
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                        ShowSubpageAddStore();
                    }
                }
                catch (Exception)
                {
                    DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                    ShowSubpageAddStore();
                }
            });
        }


        /// <summary>
        /// show subpage Add store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowSubpageAddStore()
        {
            PanelAddStore.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddStore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        /// <summary>
        /// showof Panel AddLeaderboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowOffSubpageAddStore()
        {

            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                PanelAddStore.Visibility = Visibility.Collapsed;
                TextboxName.Text = "";
                TextBoxDescription.Text = "";
                TextBoxMarketLink.Text = "";
                TextBoxAvatar.Text = "";
                TextTagCount.Text = "0";
                CheackboxActivity.IsChecked = false;

                Detail = new BsonDocument
                {
                    {"Name","" },
                    {"Description", ""},
                    {"MarketLink","" },
                    {"AvatarLink","" },
                    {"Tags",new BsonArray() },
                    {"Products" ,new BsonArray()},
                    {"IsActive",true }
                };

            };
            Storyboard.SetTargetName(Anim, PanelAddStore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


    }
    
}
