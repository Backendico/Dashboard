using MongoDB.Bson;
using System;
using System.Collections.Generic;
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

namespace Dashboard.Dashboards.Pages.SubPages.PageAchievements.Components.Achievements_ListView
{
    /// <summary>
    /// Interaction logic for AchievementListView.xaml
    /// </summary>
    public partial class AchievementListView : UserControl, IAchievementListView
    {

        BsonDocument Detail;
        bool IsSeemoreOpen = false;
        public AchievementListView(BsonDocument Detail)
        {
            InitializeComponent();
            this.Detail = Detail;
            Panel.SetZIndex(this, Detail["Zindex"].ToInt32());

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
            TextName.Text = Detail["Name"].ToString();
            TextValue.Text = Detail["Value"].ToString();
            TextCreated.Text = Detail["Created"].ToString();
            TextPlayers.Text = Detail["Players"].ToString();
            TextToken.Text = Detail["Token"].ToString();


        }
    }

    internal interface IAchievementListView
    {
        void Init();


    }
}
