using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{

    public partial class GhostButton : UserControl
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
        public GhostButton()
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
        }


    }
}
