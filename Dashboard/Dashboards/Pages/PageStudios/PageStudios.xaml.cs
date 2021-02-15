using Dashboard.Dashboards.Pages.PageStudios.Moduls.Studios_GridView;
using Dashboard.Dashboards.Pages.PageStudios.Moduls.SubPageAddNewStudio;
using Dashboard.GlobalElement;
using System.Diagnostics;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.Aut
{
    /// <summary>
    /// Interaction logic for PageStudios.xaml
    /// </summary>
    public partial class PageStudios : UserControl, IPageStudio
    {
        public PageStudios()
        {
            InitializeComponent();

            BTNNewStudio.Work += () =>
            {
                PageAUT.Placeholder.Children.Add(new SubPageAddNewStudio(this));
            };

            InitStudios();
        }

        public void InitStudios()
        {
            ContentPlaceholderStudio.Children.Clear();
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveStudios(Result =>
            {

                if (Result.ElementCount >= 1)
                {
                    if (Result["Studios"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in Result["Studios"].AsBsonArray)
                        {
                            ContentPlaceholderStudio.Children.Add(new StudioGridView(item["Setting"].AsBsonDocument));
                        }
                    }
                    else
                    {
                        PageAUT.Placeholder.Children.Add(new SubPageAddNewStudio(this));

                        Debug.WriteLine("No Studio");
                    }

                }
                else
                {
                    Debug.WriteLine("Err Recive Studio");
                }
            });
        }

    }

    public interface IPageStudio
    {

        void InitStudios();
    }
}
