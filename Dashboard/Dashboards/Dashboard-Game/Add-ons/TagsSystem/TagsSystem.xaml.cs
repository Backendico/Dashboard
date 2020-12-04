using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using RestSharp;
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

namespace Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem
{
    /// <summary>
    /// Interaction logic for TagsSystem.xaml
    /// </summary>
    public partial class TagsSystem : UserControl
    {
        public TagsSystem(BsonArray ListTag)
        {
            DashboardGame.Dashboard.Root.Children.Add(this);

            InitializeComponent();

            Root.MouseDown += (s, e) =>
            {
                Close();
            };


        }

        void Close()
        {
            try
            {
                DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
                Anim.Completed += (s, e) =>
                {
                    DashboardGame.Dashboard.Root.Children.Remove(this);
                };

                Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
                Storyboard.SetTargetName(Anim, Content.Name);
                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(Anim);
                storyboard.Begin(this);

            }
            catch (Exception)
            {
                DashboardGame.Dashboard.Root.Children.Remove(this);
            }
        }
    }
}
