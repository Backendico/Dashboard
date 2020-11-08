using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{


    public partial class ModelLeaderboardPlayer_New : System.Windows.Controls.UserControl
    {
        BsonDocument PlayerLeaderboards;


        public ModelLeaderboardPlayer_New(BsonDocument TotalLeaderboard, BsonDocument PlayerLeaderboards)
        {
            InitializeComponent();

            this.PlayerLeaderboards = PlayerLeaderboards;


            //ini combobox items
            foreach (var T in TotalLeaderboard)
            {
                ComboBoxName.Items.Add(new ComboBoxItem { Content = T.Name });
            }



            //combobox item selected method add
            foreach (ComboBoxItem item in ComboBoxName.Items)
            {
                item.Selected += (obj, e) =>
                {
                    try
                    {
                        PlayerLeaderboards.Add(item.Content.ToString(), new BsonInt64(0));
                        ComboBoxName.IsEnabled = false;
                        TextValue.IsEnabled = true;

                    }
                    catch (Exception ex)
                    {

                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                    }

                };
            }

            //fill value
            TextValue.TextChanged += (obj, e) =>
            {
                if (long.TryParse(TextValue.Text, out _) && ComboBoxName.Text != "")
                {
                    PlayerLeaderboards[ComboBoxName.Text] = new BsonInt64(long.Parse(TextValue.Text));
                }
                else
                {
                    TextValue.Text = 0.ToString();
                }
            };

            //action btn delete
            BTNDelete.MouseDown += (s, e) =>
            {
                if (TextValue.IsEnabled)
                    PlayerLeaderboards.Remove(ComboBoxName.Text);
                (Parent as StackPanel).Children.Remove(this);
            };
        }

    }

}
