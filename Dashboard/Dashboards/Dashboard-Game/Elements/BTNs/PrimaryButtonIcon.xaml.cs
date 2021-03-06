using Dashboard.Dashboards.Dashboard_Game.Elements.Icons;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for PrimaryButtonIcon.xaml
    /// </summary>
    public partial class PrimaryButtonIcon : UserControl
    {
        internal IconType Icon
        {
            set
            {
                TextIcon.Text = new Icons.Icons()[value];
            }
        }
        internal string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                TextButton.Text = value;
                _Text = value;
            }
        }

        string _Text = "";

        public PrimaryButtonIcon()
        {
            InitializeComponent();

            GotFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(2);
            };

            LostFocus += (s, e) =>
            {

                BorderFocuse.BorderThickness = new Thickness(0);
            };


            //action work
            MouseDown += (s, e) =>
            {
                if (Work != null)
                {
                    Work();
                }
            };
        }

        internal event Action Work;
    }
}
