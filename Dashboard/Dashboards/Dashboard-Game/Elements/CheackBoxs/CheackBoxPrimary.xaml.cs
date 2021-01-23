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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.CheackBoxs
{
    /// <summary>
    /// Interaction logic for CheackBoxPrimary.xaml
    /// </summary>
    public partial class CheackBoxPrimary : UserControl
    {
        public bool IsCheacked
        {
            set
            {
                if (value && Cheacker.IsEnabled)
                {
                    Cheack = value;
                    Cheacker.Background = new SolidColorBrush(Colors.Black);
                    TextCheack.Text = "\xF78C";
                }
                else if (Cheacker.IsEnabled)
                {
                    Cheack = value;
                    Cheacker.Background = new SolidColorBrush(Colors.Transparent);
                    TextCheack.Text = "";
                }
            }
            get { return Cheack; }
        }

        bool Cheack = false;

        public CheackBoxPrimary()
        {
            InitializeComponent();

            MouseDown += (s, e) =>
            {
                if (Cheack)
                {
                    IsCheacked = false;
                }
                else
                {
                    IsCheacked = true;
                }
            };
        }
    }
}
