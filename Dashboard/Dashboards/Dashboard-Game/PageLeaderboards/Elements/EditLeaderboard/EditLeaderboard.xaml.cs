using Dashboard.GlobalElement;
using RestSharp;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MongoDB.Bson;
using System;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Security.Cryptography.X509Certificates;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    public partial class EditLeaderboard : UserControl
    {
        public EditLeaderboard(IEditorLeaderboard Editor)
        {
            InitializeComponent();

            #region Page Setting

            TextToken.Text = Editor.DetailLeaderboard["Settings"]["Token"].AsObjectId.ToString();
            TextStart.Text = DateTime.Parse(Editor.DetailLeaderboard["Settings"]["Start"].ToString()).ToString();
            TextLeaderboardName.Text = Editor.DetailLeaderboard["Settings"]["Name"].AsString;
            TextName_Setting.Text = Editor.DetailLeaderboard["Settings"]["Name"].AsString;
            TextMinValue.Text = Editor.DetailLeaderboard["Settings"]["Min"].ToString();
            TextMaxValue.Text = Editor.DetailLeaderboard["Settings"]["Max"].ToString();

            ComboboxReset.SelectedIndex = Editor.DetailLeaderboard["Settings"]["Reset"].ToInt32();
            TextAmount.Text = Editor.DetailLeaderboard["Settings"]["Amount"].ToString();

            ComboboxSort.SelectedIndex = Editor.DetailLeaderboard["Settings"]["Sort"].ToInt32();

            PanelAmount.Loaded += (s, e) =>
            {

                if (Editor.DetailLeaderboard["Settings"]["Sort"].ToInt32() == 0)
                {
                    PanelAmount.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelAmount.Visibility = Visibility.Visible;
                }
            };


            //copy token
            TextToken.MouseDown += GlobalEvents.CopyText;

            //Control and Deploy amount
            TextAmount.LostFocus += (s, e) =>
            {
                try
                {
                    Editor.DetailLeaderboard["Settings"]["Amount"] = int.Parse(TextAmount.Text);
                    Editor.Save();
                }
                catch (Exception ex)
                {
                    Editor.DetailLeaderboard["Settings"]["Amount"] = 1;
                    TextAmount.Text = "1";
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                    Editor.Save();
                }
            };

            //change Minvalue
            TextMinValue.LostFocus += (s, e) =>
            {
                try
                {
                    Editor.DetailLeaderboard["Settings"]["Min"] = Int64.Parse(TextMinValue.Text);
                    Editor.Save();
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                    TextMinValue.Text = Editor.DetailLeaderboard["Settings"]["Min"].ToString();
                }
            };

            //change Max value
            TextMaxValue.LostFocus += (s, e) =>
            {
                try
                {
                    Editor.DetailLeaderboard["Settings"]["Max"] = Int64.Parse(TextMaxValue.Text);
                    Editor.Save();
                }
                catch (Exception ex)
                {

                    TextMaxValue.Text = Editor.DetailLeaderboard["Settings"]["Max"].ToString();
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }

            };


            //change amount
            TextAmount.LostFocus += (s, e) =>
            {
                try
                {
                    Editor.DetailLeaderboard["Settings"]["Amount"] = Int64.Parse(TextMinValue.Text);
                    Editor.Save();
                }
                catch (Exception ex)
                {
                    TextAmount.Text = Editor.DetailLeaderboard["Settings"]["Amount"].ToString();
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }
            };


            //change reset
            ComboboxReset.SelectionChanged += (s, e) =>
            {
                Editor.DetailLeaderboard["Settings"]["Reset"] = ComboboxReset.SelectedIndex;

                if (Editor.DetailLeaderboard["Settings"]["Reset"].ToInt32() == 0)
                {
                    PanelAmount.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelAmount.Visibility = Visibility.Visible;
                }
                Editor.Save();
            };

            //change sort
            ComboboxSort.SelectionChanged += (s, e) =>
            {
                Editor.DetailLeaderboard["Settings"]["Sort"] = ComboboxSort.SelectedIndex;
                Editor.Save();
            };

            #endregion

            #region Page Leaderboard


            #endregion
        }

        //global method
        private void ChangePage(object sender, RoutedEventArgs e)
        {
            var BTN = sender as Button;

            switch (BTN.Content)
            {
                case "Setting":
                    {
                        ContentLeaderboard.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Collapsed;
                        ContentSetting.Visibility = Visibility.Visible;


                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    }
                    break;
                case "Leaderboard":
                    {
                        ContentLeaderboard.Visibility = Visibility.Visible;
                        ContentSetting.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Collapsed;

                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        
                        ReciveLeaderboard();
                    }
                    break;
                case "Backups":
                    {
                        ContentLeaderboard.Visibility = Visibility.Collapsed;
                        ContentSetting.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Visible;


                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    }
                    break;

                default:
                    Debug.WriteLine("not set");
                    break;
            }
            ShowoffPaneladdPlayer();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }

        void ReciveLeaderboard()
        {

            Debug.WriteLine("hi");
        }


        private void ShowPanelAddPlayer()
        {
            PanelAddPlayer.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        private void ShowoffPaneladdPlayer()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                TextboxTokenPlayer.Text = "";
                TextboxValue.Text = "";

                PanelAddPlayer.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

    }
}
