using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.TextInputs
{
    public partial class TextInputPrimary : UserControl
    {

        public InputType InputMode
        {

            get
            {
                try
                {
                    return Type;

                }
                catch (System.Exception)
                {
                    return InputType.All;
                }
            }
            set
            {
                Type = value;
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
                    if (MainTextBox.IsFocused)
                    {

                        TextblockError.Visibility = Visibility.Collapsed;
                        IconErr.Visibility = Visibility.Collapsed;
                        MainTextBox.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                        MainTextBox.BorderThickness = new Thickness(2);

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
        InputType Type;


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
                    MainTextBox.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                    MainTextBox.BorderThickness = new Thickness(2);
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
                    MainTextBox.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8D8D8D");
                    MainTextBox.BorderThickness = new Thickness(0, 0, 0, 2);
                }
            };


            MainTextBox.TextChanged += (s, e) =>
            {
                if (MainTextBox.Text.Length >= 1 && MainTextBox.Text != PlaceHolder)
                {
                    MainTextBox.Foreground = new SolidColorBrush(Colors.Black);
                }


                switch (InputMode)
                {
                    case InputType.Email:
                        {
                            try
                            {
                                if (Text.Length >= 1 && Text != PlaceHolder)
                                {
                                    new MailAddress(MainTextBox.Text);
                                    IsError = false;
                                }
                                else
                                {
                                    IsError = false;
                                }

                            }
                            catch (System.Exception)
                            {

                                IsError = true;
                            }
                        }
                        break;
                    case InputType.All:
                        {
                            IsError = false;
                        }
                        break;
                    case InputType.Number:
                        break;
                    default:
                        break;
                }
            };

        }


        public enum InputType
        {
            Email, All, Number
        }
    }
}
