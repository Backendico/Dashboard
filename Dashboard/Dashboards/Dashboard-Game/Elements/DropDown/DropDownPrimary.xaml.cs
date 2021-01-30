using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.DropDown
{
    /// <summary>
    /// Interaction logic for DropDownPrimary.xaml
    /// </summary>
    public partial class DropDownPrimary : UserControl
    {
        public StackPanel Items
        {
            get
            {
                return Content1.Content as StackPanel;
            }
            set
            {
                Content1.Content = value;
            }
        }

        public bool IsOpenSuggests
        {
            get
            {
                return _IsOpenSuggest;
            }
            set
            {
                _IsOpenSuggest = value;

                if (value)
                {
                    OpenLists();
                }
                else
                {
                    CloseLists();
                }

            }
        }

        public string Text
        {
            get
            {
                return MainTextBox.Text;
            }
            set
            {
                MainTextBox.Text = value;
                if (value.Length >= 1)
                {

                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    CloseLists();
                }
            }
        }

        public string PlaceHolder
        {
            get
            {
                return _PlaceHolder;
            }
            set
            {
                _PlaceHolder = value;
                MainTextBox.Foreground = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");

                if (Text != value || Text.Length <= 0)
                {
                    MainTextBox.Text = value;
                }

            }

        }


        string _PlaceHolder = "Place Holder";
        bool _IsOpenSuggest = false;



        public DropDownPrimary()
        {
            InitializeComponent();

            GotFocus += (s, e) =>
            {

                Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
            };

            LostFocus += (s, e) =>
            {
                Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                CloseLists();
            };

            MouseDown += (s, e) =>
            {
                if (IsOpenSuggests)
                {
                    IsOpenSuggests = false;
                    Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    CloseLists();
                }
                else
                {
                    IsOpenSuggests = true;
                    Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                    OpenLists();
                }
            };
        }


        void OpenLists()
        {
            IconArrow.Text = "\xE96D";

            DoubleAnimation Anim = new DoubleAnimation(150, TimeSpan.FromSeconds(0.2));
            Storyboard.SetTargetName(Anim, PanelSuggest.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

        }

        void CloseLists()
        {

            IconArrow.Text = "\xE96E";

            DoubleAnimation Anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.2));
            Storyboard.SetTargetName(Anim, PanelSuggest.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        
    }
}
