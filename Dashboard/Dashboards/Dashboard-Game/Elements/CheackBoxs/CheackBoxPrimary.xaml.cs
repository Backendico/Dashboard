using System.Windows.Controls;
using System.Windows.Media;

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
