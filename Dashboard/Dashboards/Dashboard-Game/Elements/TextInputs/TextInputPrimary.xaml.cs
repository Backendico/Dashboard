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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.TextInputs
{
    public partial class TextInputPrimary : UserControl
    {

        public string Text
        {
            get
            {
                return MainTextBox.Text;
            }
            set
            {
                if (value.Length >= 1)
                {

                    MainTextBox.Text = value;
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
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

        public TextInputPrimary()
        {
            InitializeComponent();

            GotFocus += (s, e) =>
            {
                if (Text == PlaceHolder)
                {
                    MainTextBox.Foreground = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    MainTextBox.Text = "";
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
                    MainTextBox.Text = PlaceHolder;
                }
                else
                {
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);

                }
            };


            MainTextBox.TextChanged += (s, e) =>
            {

                if (MainTextBox.Text.Length >= 1&&MainTextBox.Text!=PlaceHolder)
                {
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);

                }
            };

        }
    }
}
