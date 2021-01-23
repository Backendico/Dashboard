using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.TextRichPrimary
{
    /// <summary>
    /// Interaction logic for TextRichPrimary.xaml
    /// </summary>
    public partial class TextRichPrimary : UserControl
    {

        public string TextLabelHeader
        {

            set
            {
                try
                {
                    TextLabel.Text = value.Length >= 1 ? value : "Enter Text:";
                }
                catch (Exception)
                {
                    TextLabel.Text = "Enter Text:";
                }

            }
        }

        public int TextLength
        {
            get { return _TextLength; }
            set
            {
                MaxTextLenght.Text = value.ToString();
                _TextLength = value;
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

                    Textbox.BorderBrush = TextblockError.Foreground;
                    Textbox.BorderThickness = new Thickness(2, 2, 2, 2);
                }
                else
                {
                    TextblockError.Visibility = Visibility.Collapsed;
                    IconErr.Visibility = Visibility.Collapsed;
                    Textbox.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#8d8d8d");
                    Textbox.BorderThickness = new Thickness(0, 0, 0, 2);
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
        int _TextLength;
        public string SafeText;


        public TextRichPrimary()
        {
            InitializeComponent();
            Textbox.TextChanged += (s, e) =>
            {
                TextCount.Text = Textbox.Text.Length.ToString();

                if (Textbox.Text.Length > _TextLength)
                {
                    IsError = true;
                    TextblockError.Text = "The text length is too long";
                }
                else
                {
                    SafeText = Textbox.Text;
                    TextblockError.Text = "Error";
                    IsError = false;
                }
            };
        }
    }
}
