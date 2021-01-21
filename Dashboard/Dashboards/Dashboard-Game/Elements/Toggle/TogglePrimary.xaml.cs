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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Toggle
{
    /// <summary>
    /// Interaction logic for TogglePrimary.xaml
    /// </summary>
    public partial class TogglePrimary : UserControl
    {

        public bool IsOn
        {
            get { return _IsOn; }
            set
            {
                if (IsEnabled)
                {

                    if (value)
                    {
                        Root.Background = (Brush)new BrushConverter().ConvertFromString("#42be65");
                        TextOnOff.Text = "On";
                        AnimationON();
                    }
                    else
                    {
                        Root.Background = (Brush)new BrushConverter().ConvertFromString("#8D8D8D");
                        TextOnOff.Text = "Off";
                        AnimationOff();
                    }

                    _IsOn = value;
                }
            }
        }

        public string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                _Label = value;
                TextLable.Text = value;

            }
        }

        string _Label;
        bool _IsOn;

        public TogglePrimary()
        {
            InitializeComponent();

            MouseDown += (s, e) =>
            {
                if (IsEnabled)
                {
                    if (!_IsOn)
                    {
                        IsOn = true;
                    }
                    else
                    {
                        IsOn = false;
                    }
                }
            };

            IsEnabledChanged += (s, e) =>
            {
                if (IsEnabled)
                {
                    if (IsOn)
                    {
                        Root.Background = (Brush)new BrushConverter().ConvertFromString("#42be65");

                    }
                    else
                    {
                        Root.Background = (Brush)new BrushConverter().ConvertFromString("#8D8D8D");
                    }

                }
                else
                {

                    Root.Background = (Brush)new BrushConverter().ConvertFromString("#F4F4F4");
                }
            };
        }


        void AnimationON()
        {
            ThicknessAnimation Anim = new ThicknessAnimation(new Thickness(3, 0, 0, 0), new Thickness(23, 0, 0, 0), TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, TogglePointer.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Margin"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

        }

        void AnimationOff()
        {
            ThicknessAnimation Anim = new ThicknessAnimation(new Thickness(22, 0, 0, 0), new Thickness(3, 0, 0, 0), TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, TogglePointer.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Margin"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

    }
}
