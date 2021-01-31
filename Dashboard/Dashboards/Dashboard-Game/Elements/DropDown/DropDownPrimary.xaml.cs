using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.DropDown
{

    public partial class DropDownPrimary : UserControl
    {

        internal int DefualtItem
        {
            set
            {
                _SelectionIndex = value;
            }
        }
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


        public string PlaceHolder
        {
            get
            {
                return _PlaceHolder;
            }
            set
            {
                _PlaceHolder = value;
                TextPlaceHolder.Text = value;
            }

        }


        string _PlaceHolder = "Place Holder";
        bool _IsOpenSuggest = false;
        int _SelectionIndex = 0;


        public DropDownPrimary()
        {
            InitializeComponent();

            Loaded += (S, e) =>
            {
                SelectItem(Items.Children[_SelectionIndex] as DropDownItem);
            };
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

        internal void SelectItem(DropDownItem Item)
        {
            _SelectionIndex = Items.Children.IndexOf(Item);
            TextSelected.Text = (Items.Children[Items.Children.IndexOf(Item)] as DropDownItem).Text;
        }

    }
}
