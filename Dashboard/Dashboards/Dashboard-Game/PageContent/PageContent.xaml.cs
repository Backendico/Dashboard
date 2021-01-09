using Dashboard.Dashboards.Dashboard_Game.PageContent.Elements.ModelContent;
using Dashboard.GlobalElement;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageContent
{
    /// <summary>
    /// Interaction logic for PageContent.xaml
    /// </summary>
    public partial class PageContent : UserControl
    {
        int Count = 100;

        public PageContent()
        {
            InitializeComponent();

            InitPageContent();

            BTNAddContent.MouseDown += (s, e) =>
            {
                if (TextBoxName.Text.Length >= 4)
                {

                    SDK.SDK_PageDashboards.DashboardGame.PageContent.AddContent(TextBoxName.Text, result =>
                    {

                        Debug.WriteLine(result);
                    });
                }
                else
                {
                    DashboardGame.Notifaction("Name Short", Notifaction.StatusMessage.Warrning);
                }

            };
        }

        void InitPageContent()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageContent.RecieveContents(Count, result =>
            {
                if (result.ElementCount>=1)
                {
                    Debug.WriteLine(result);
                    foreach (var item in result["Content"].AsBsonArray)
                    {
                        PlaceContent.Children.Add(new ModelContent(item.AsBsonDocument));
                    }
                }
                else
                {
                    DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                }

            });

        }
    }
}
