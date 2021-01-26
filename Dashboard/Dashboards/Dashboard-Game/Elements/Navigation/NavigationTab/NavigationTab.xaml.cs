using Dashboard.Dashboards.Dashboard_Game.Elements.Icons;
using System;
using System.CodeDom;
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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Navigation.NavigationTab
{

    public partial class NavigationTab : UserControl
    {
        internal IconType IconType
        {
            set
            {
                Icon.Text = new Icons.Icons()[value];
            }
        }
        internal string NameTab
        {
            set
            {
                try
                {
                    if (value.Length >= 1)
                    {
                        TabName.Text = value;
                    }
                    else
                    {
                        TabName.Text = "Not Set";
                    }

                }
                catch (Exception)
                {
                    TabName.Text = "Not Set";
                }
            }
        }

        internal bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (value)
                {


                    foreach (var item in (Parent as StackPanel).Children)
                    {
                        if (item.GetType() == typeof(NavigationTab))
                        {
                            if ((item as NavigationTab).IsSelected && (item as NavigationTab) != this)
                            {
                                (item as NavigationTab).IsSelected = false;

                            }
                        }

                    }


                    Select();
                }
                else
                {
                    DeSelect();
                }

                _IsSelected = value;

            }
        }

        bool _IsSelected;
        public NavigationTab()
        {
            InitializeComponent();

            MouseEnter += (s, e) =>
            {
                Hover();
            };

            MouseLeave += (s, e) =>
            {
                HoverOff();
            };


            MouseDown += (s, e) =>
            {
                IsSelected = true;
            };




        }

        void Select()
        {

            TabName.FontWeight = FontWeights.Bold;


            DoubleAnimation Anim3 = new DoubleAnimation(11, 13, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim3, TabName.Name);
            Storyboard.SetTargetProperty(Anim3, new PropertyPath("FontSize"));


            DoubleAnimation Anim2 = new DoubleAnimation(16, 19, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim2, Icon.Name);
            Storyboard.SetTargetProperty(Anim2, new PropertyPath("FontSize"));



            DoubleAnimation Anim1 = new DoubleAnimation(0, 0.3, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim1, BackgroudnBorder.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(Anim1);
            storyboard.Children.Add(Anim2);
            storyboard.Children.Add(Anim3);
            storyboard.Begin(this);
        }

        void DeSelect()
        {
            TabName.FontWeight = FontWeights.Normal;


            DoubleAnimation Anim3 = new DoubleAnimation(13, 11, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim3, TabName.Name);
            Storyboard.SetTargetProperty(Anim3, new PropertyPath("FontSize"));


            DoubleAnimation Anim2 = new DoubleAnimation(19, 16, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim2, Icon.Name);
            Storyboard.SetTargetProperty(Anim2, new PropertyPath("FontSize"));

            DoubleAnimation Anim1 = new DoubleAnimation(0.3, 0, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim1, BackgroudnBorder.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(Anim1);
            storyboard.Children.Add(Anim2);
            storyboard.Children.Add(Anim3);
            storyboard.Begin(this);
        }

        void Hover()
        {
            if (!IsSelected)
            {
                DoubleAnimation Anim1 = new DoubleAnimation(0, 0.3, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim1, BackgroudnBorder.Name);
                Storyboard.SetTargetProperty(Anim1, new PropertyPath("Opacity"));
                Storyboard storyboard = new Storyboard();

                storyboard.Children.Add(Anim1);
                storyboard.Begin(this);
            }

        }

        void HoverOff()
        {
            if (!IsSelected)
            {
                DoubleAnimation Anim1 = new DoubleAnimation(0.3, 0, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim1, BackgroudnBorder.Name);
                Storyboard.SetTargetProperty(Anim1, new PropertyPath("Opacity"));
                Storyboard storyboard = new Storyboard();

                storyboard.Children.Add(Anim1);
                storyboard.Begin(this);
            }
        }

    }
}
