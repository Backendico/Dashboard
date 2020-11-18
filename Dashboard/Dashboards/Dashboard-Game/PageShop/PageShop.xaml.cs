using Dashboard.GlobalElement;
using MongoDB.Bson;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        };


        public PageShop()
        {
            InitializeComponent();


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


                    SDK.SDK_PageDashboards.DashboardGame.PageStore.AddStore(Detail, result =>
                    {
                        if (result)
                        {
                            DashboardGame.Notifaction("Store Added", Notifaction.StatusMessage.Ok);
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

            //action add tag
            BTNAddTag.MouseDown += (s, e) =>
            {
                if (TextBoxTag.Text.Length >= 1)
                {
                    //cheack Dublicate
                    if (Detail["Tags"].AsBsonArray.Count >= 1)
                    {

                        int Count = 0;
                        for (int i = 0; i < Detail["Tags"].AsBsonArray.Count; i++)
                        {

                            if (Detail["Tags"].AsBsonArray[i].AsString != TextBoxTag.Text)
                            {
                                Count = 0;
                            }
                            else
                            {
                                Count = 1;
                            }
                        }
                        if (Count == 1)
                        {
                            DashboardGame.Notifaction("Tags available", Notifaction.StatusMessage.Error);
                        }
                        else
                        {
                            Detail["Tags"].AsBsonArray.Add(TextBoxTag.Text);
                            RefreshTags();
                            TextBoxTag.Text = "";
                            DashboardGame.Notifaction("Tag add", Notifaction.StatusMessage.Ok);
                        }
                    }
                    else
                    {
                        Detail["Tags"].AsBsonArray.Add(TextBoxTag.Text);
                        RefreshTags();
                        TextBoxTag.Text = "";
                        DashboardGame.Notifaction("Tag add", Notifaction.StatusMessage.Ok);
                    }
                }
                else
                {
                    DashboardGame.Notifaction("Name Tag Short", Notifaction.StatusMessage.Error);
                }
            };

            //action close
            PanelAddStore.MouseDown += (s, e) =>
            {

                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffSubpageAddLeaderboard();
                }
            };

            //btn show panel add store
            BTNShowPanelStore.MouseDown += (s, e) =>
            {
                ShowSubpageAddLeaderboard();

            };
        }


        /// <summary>
        /// show subpage Add store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowSubpageAddLeaderboard()
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
        void ShowOffSubpageAddLeaderboard()
        {

            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                PanelAddStore.Visibility = Visibility.Collapsed;
                TextboxName.Text = "";
                TextBoxDescription.Text = "";
                TextBoxMarketLink.Text = "";
                TextBoxAvatar.Text = "";
                TextBoxTag.Text = "";
                Detail = new BsonDocument
                {
                    {"Name","" },
                    {"Description", ""},
                    {"MarketLink","" },
                    {"AvatarLink","" },
                    {"Tags",new BsonArray() },
                    {"Products" ,new BsonArray()},
                };

                PlaceTags.Children.Clear();
            };
            Storyboard.SetTargetName(Anim, PanelAddStore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        void RefreshTags()
        {
            PlaceTags.Children.Clear();

            if (Detail["Tags"].AsBsonArray.Count == 1)
            {
                PlaceTags.Children.Add(new ModelTag(Detail["Tags"][0].AsString, Detail["Tags"].AsBsonArray, RefreshTags, PlaceTags));
            }
            else
            {
                for (int i = 0; i < Detail["Tags"].AsBsonArray.Count; i++)
                {
                    if (i + 1 == Detail["Tags"].AsBsonArray.Count)
                    {
                        PlaceTags.Children.Add(new ModelTag(Detail["Tags"][i].AsString, Detail["Tags"].AsBsonArray, RefreshTags, PlaceTags));
                    }
                    else
                    {

                        PlaceTags.Children.Add(new ModelTag(Detail["Tags"][i].AsString, Detail["Tags"].AsBsonArray, RefreshTags, PlaceTags));
                        PlaceTags.Children.Add(new TextBlock()
                        {
                            Text = " | ",
                            Foreground = new SolidColorBrush(Colors.Black)

                        });
                    }


                }

            }

        }


        class ModelTag : TextBlock
        {
            public ModelTag(string NameTag, BsonArray TagList, Action RefreshTags, WrapPanel PlaceTags)
            {
                Text = NameTag;
                Foreground = new SolidColorBrush(Colors.Black);
                FontWeight = FontWeights.Bold;
                Cursor = Cursors.Hand;
                MouseEnter += (s, e) =>
                {
                    Foreground = new SolidColorBrush(Colors.Tomato);
                };

                MouseLeave += (s, e) =>
                {
                    Foreground = new SolidColorBrush(Colors.Black);
                };

                MouseDown += (s, e) =>
                {
                    TagList.Remove(NameTag);
                    RefreshTags();
                };
            }

        }
    }
}
