using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.Player_ListView
{

    public partial class PlayerListView : UserControl, IPlayerListView
    {
        bool IsSeemoreOpen = false;
        BsonDocument Detail;

        public PlayerListView(BsonDocument Detail, IPagePlayer Editor)
        {
            InitializeComponent();
            this.Detail = Detail;

            Panel.SetZIndex(this, Detail["ZIndex"].AsInt32);
            Init();

            BTNSeemore.MouseDown += (s, e) =>
            {
                if (IsSeemoreOpen)
                {
                    CloseMore();
                }
                else
                {
                    OpenMore();
                }

            };


        }
        void OpenMore()
        {
            IsSeemoreOpen = true;
            PanelMore.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3f));
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void CloseMore()
        {
            IsSeemoreOpen = false;
            DoubleAnimation Anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3f));
            Anim.Completed += (s, e) =>
            {
                PanelMore.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        public void Init()
        {

            // Username
            if (Detail.GetElement("Username").Value.ToString().Length >= 1)
            {
                TextUsername.Text = Detail.GetElement("Username").Value.ToString();
            }
            else
            {
                TextUsername.Text = "N/A";
            }

            // Email
            if (Detail["Email"].ToString().Length >= 1)
            {
                TextEmail.Text = Detail["Email"].ToString();
            }
            else
            {
                TextEmail.Text = "N/A";
            }

            // last login
            if (Detail["LastLogin"].ToString().Length >= 1)
            {
                TextLastLogin.Text = Detail["LastLogin"].ToLocalTime().ToString();
            }
            else
            {
                TextLastLogin.Text = "N/A";
            }

            //Created
            if (Detail["Created"].ToString().Length >= 1)
            {
                TextCreated.Text = Detail["Created"].ToLocalTime().ToString();
            }
            else
            {
                TextCreated.Text = "N/A";
            }


            //Currencey
            if (Detail["Currencey"].ToString().Length >= 1)
            {
                TextCurrencey.Text = Detail["Currencey"].ToString();
            }
            else
            {
                TextCurrencey.Text = "N/A";
            }

            //Language
            if (Detail["Country"].ToString().Length >= 1)
            {
                TextLanguage.Text = Detail["Country"].ToString();
            }
            else
            {
                TextLanguage.Text = "N/A";
            }

            //Token
            TextToken.Text = Detail["Token"].ToString();
        }
    }

    public interface IPlayerListView
    {
        void Init();
    }

}
