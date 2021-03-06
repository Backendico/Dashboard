using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.TextInputs
{

    public partial class InputSearch : UserControl
    {
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
                    OpenSuggests();
                }
                else
                {
                    CloseSuggests();
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
                    CloseSuggests();
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


        public bool IsError
        {
            get
            {
                return err;
            }
            set
            {

                err = value;
                if (err)
                {
                    TextblockError.Visibility = Visibility.Visible;
                    IconErr.Visibility = Visibility.Visible;
                    Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#DA1E28");

                }
                else
                {
                    TextblockError.Visibility = Visibility.Collapsed;
                    IconErr.Visibility = Visibility.Collapsed;

                    if (MainTextBox.IsFocused)
                    {
                        Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                        Root.BorderThickness = new Thickness(2);

                    }
                    else
                    {
                        Root.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        Root.BorderThickness = new Thickness(2);
                    }
                }
            }
        }

        public string TextError
        {
            get
            {
                return TextblockError.Text;
            }
            set
            {
                if (TextblockError.Text.Length >= 1)
                {

                    TextblockError.Text = value;
                }
                else
                {
                    TextblockError.Text = "Error";
                }
            }
        }

        bool err = false;
        string _PlaceHolder = "Place Holder";
        bool _IsOpenSuggest = false;

        public InputSearch()
        {
            InitializeComponent();

            GotFocus += (s, e) =>
            {
                if (Text == PlaceHolder)
                {
                    MainTextBox.Foreground = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                    MainTextBox.Text = "";
                }
                else
                {
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
                    Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                    Root.BorderThickness = new Thickness(2);
                }
            };

            LostFocus += (s, e) =>
            {
                if (Text.Length <= 0)
                {
                    MainTextBox.Foreground = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    Root.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    MainTextBox.Text = PlaceHolder;
                }
                else
                {

                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
                    Root.BorderBrush = new SolidColorBrush(Colors.Transparent);
                }

                CloseSuggests();
            };

            //Action change Text
            MainTextBox.TextChanged += (s, e) =>
            {
                if (IsLoaded)
                {

                    if (Text.Length >= 1)
                    {
                        OpenSuggests();
                    }
                    else
                    {
                        CloseSuggests();
                    }
                }
            };

            //action btnClearText
            BTNClear.MouseDown += (s, e) =>
            {
                if (Text != PlaceHolder)
                {
                    Text = "";
                    PlaceHolder = _PlaceHolder;
                    CloseSuggests();
                }
            };
        }

        void OpenSuggests()
        {
            PanelSuggest.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(150, TimeSpan.FromSeconds(0.2));
            Storyboard.SetTargetName(Anim, PanelSuggest.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

        }

        void CloseSuggests()
        {
            DoubleAnimation Anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.2));
            Anim.Completed += (s, e) =>
            {
                PanelSuggest.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetName(Anim, PanelSuggest.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


    }
}
