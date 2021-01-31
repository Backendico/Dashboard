using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for SecondaryButton.xaml
    /// </summary>
    public partial class SecondaryButton : UserControl
    {
        public string TextButton
        {
            get { return _Textbutton; }
            set
            {
                NameButton.Text = value;
                _Textbutton = value;
            }
        }

        string _Textbutton;
        public SecondaryButton()
        {
            InitializeComponent();

            MouseDown += (s, e) =>
            {
                try
                {
                    Worker();
                }
                catch (Exception)
                {
                    Debug.WriteLine("no worker");
                }
            };


            GotFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(2);
            };

            LostFocus += (s, e) =>
            {

                BorderFocuse.BorderThickness = new Thickness(0);
            };
        }


        public event Action Worker;
    }
}
