using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for BTNPrimary.xaml
    /// </summary>
    public partial class BTNPrimary : UserControl
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
        public BTNPrimary()
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
