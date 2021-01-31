using Dashboard.Dashboards.Pages.Aut;
using System.Diagnostics;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageAUT.Recovery
{

    public partial class PageAccountRecovery : UserControl
    {
        public PageAccountRecovery()
        {
            InitializeComponent();

            BTNLogin.Worker += () =>
            {
                (Parent as Grid).Children.Add(new PageLogin());
                (Parent as Grid).Children.Remove(this);
            };
            BTNRecovery.Worker += () =>
            {
                Debug.WriteLine("Recovery Here");
            };
        }
    }
}
