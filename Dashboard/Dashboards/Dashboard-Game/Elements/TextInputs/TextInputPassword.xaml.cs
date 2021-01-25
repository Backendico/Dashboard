using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.TextInputs
{
    /// <summary>
    /// Interaction logic for TextInputPassword.xaml
    /// </summary>
    public partial class TextInputPassword : UserControl
    {

        public string Text
        {
            get
            {
                try
                {

                    return MainTextBox.Password;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            set
            {
                try
                {

                    if (value.Length >= 1)
                    {

                        MainTextBox.Password = value;
                        MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                catch
                {

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
                    MainTextBox.Password = value;
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

                    MainTextBox.BorderBrush = TextblockError.Foreground;
                    MainTextBox.BorderThickness = new Thickness(2, 2, 2, 2);
                }
                else
                {
                    TextblockError.Visibility = Visibility.Collapsed;
                    IconErr.Visibility = Visibility.Collapsed;
                    MainTextBox.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    MainTextBox.BorderThickness = new Thickness(0, 0, 0, 2);
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

        bool err;
        string _PlaceHolder;

        public TextInputPassword()
        {
            InitializeComponent();

            GotFocus += (s, e) =>
            {
                if (Text == PlaceHolder)
                {
                    MainTextBox.Foreground = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    MainTextBox.Password = "";
                }
                else
                {
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
                }
            };

            LostFocus += (s, e) =>
            {
                if (Text.Length <= 0)
                {
                    MainTextBox.Foreground = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    MainTextBox.Password = PlaceHolder;
                }
                else
                {
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);

                }
            };


            MainTextBox.PasswordChanged += (s, e) =>
            {
                if (MainTextBox.Password.Length >= 1 && MainTextBox.Password != PlaceHolder)
                {
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
                }

            };
        }
    }
}
