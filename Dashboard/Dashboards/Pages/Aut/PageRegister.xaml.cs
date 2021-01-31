using Dashboard.Dashboards.Pages.Aut;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageAUT.Register
{
    /// <summary>
    /// Interaction logic for PageRegister.xaml
    /// </summary>
    public partial class PageRegister : UserControl
    {
        public PageRegister()
        {
            InitializeComponent();

            //action btn login
            BTNLogin.Worker += () =>
            {
                (Parent as Grid).Children.Add(new PageLogin());
                (Parent as Grid).Children.Remove(this);
            };

            //action btn register
            BTNRegister.Worker += () =>
            {

            };



        }
    }
}
