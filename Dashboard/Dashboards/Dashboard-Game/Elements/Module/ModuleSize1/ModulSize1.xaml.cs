using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Module.ModuleSize1
{

    public partial class ModulSize1 : UserControl
    {
        public string TabName
        {
            get
            {
                return NameTab.Text;
            }
            set
            {
                NameTab.Text = value;
            }
        }


        public Grid PageContent
        {
            get
            {
                return PlaceHolder.Content as Grid;
            }
            set
            {
                PlaceHolder.Content = value;
            }
        }
    
        public ModulSize1()
        {
            InitializeComponent();
            ShowPage(1);

            //action btn close
            BTNClose.MouseDown += (s, e) =>
            {
                ShowPage(0, () =>
                {
                    Visibility = Visibility.Collapsed;
                });
            };

        }


        void ShowPage(int TO, Action Act = null)
        {
            var Current = 0;

            if (TO != 0)
            {
                Current = 0;
            }
            else
            {
                Current = 1;
            }

            DoubleAnimation Anim = new DoubleAnimation(Current, TO, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                if (Act != null)
                {
                    Act();
                }
            };
            Storyboard.SetTargetName(Anim, Root.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard story = new Storyboard();
            story.Children.Add(Anim);
            story.Begin(this);
        }
    }
}
