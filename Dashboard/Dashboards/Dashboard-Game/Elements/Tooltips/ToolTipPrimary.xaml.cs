using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Tooltips
{
    /// <summary>
    /// Interaction logic for ToolTipPrimary.xaml
    /// </summary>
    public partial class ToolTipPrimary : UserControl
    {
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                ToolTip = value;
            }
        }


        public string Placeholder
        {
            get
            {
                return TextTooltip.Text;
            }
            set
            {
                TextTooltip.Text = value;
            }
        }

        string _text;

        public ToolTipPrimary()
        {
            InitializeComponent();

        }
    }
}
