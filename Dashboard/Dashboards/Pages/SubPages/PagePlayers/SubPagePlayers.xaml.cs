using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.AddPlayer;
using Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.Player_GridShow;
using Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.Player_ListView;
using Dashboard.GlobalElement;
using System.Diagnostics;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.SubPages.PagePlayers
{

    public partial class SubPagePlayers : UserControl, IPagePlayer
    {
        int CountRecieve = 100;

        public SubPagePlayers()
        {
            InitializeComponent();

            Init();
            //action btn add player
            BTNaddPlayer.Work += () =>
            {
                PageAUT.Placeholder.Children.Add(new SubPageAddPlayer());
            };

            //action switch to grid view 
            BTNGridView.Worker += () =>
            {
                PanelGridView.Visibility = System.Windows.Visibility.Visible;
                PanelListView.Visibility = System.Windows.Visibility.Collapsed;
            };

            //action switch to list view
            BTNListView.Worker += () =>
            {
                PanelGridView.Visibility = System.Windows.Visibility.Collapsed;
                PanelListView.Visibility = System.Windows.Visibility.Visible;
            };

        }


        /// <summary>
        /// init page player 
        /// </summary>
        public void Init()
        {
            ContentPlaceHolderGridviewe.Children.Clear();
            ContentPlaceHolderListView.Children.Clear();
            
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.RecieveListPlayer(CountRecieve, result =>
            {
                if (result.ElementCount >= 1)
                {
                    if (result["ListPlayers"].AsBsonDocument["Players"].AsBsonArray.Count >= 1)
                    {
                        
                        foreach (var item in result["ListPlayers"].AsBsonDocument["Players"].AsBsonArray)
                        {
                            ContentPlaceHolderGridviewe.Children.Add(new Player_GridShow(item.AsBsonDocument,this));
                            ContentPlaceHolderListView.Children.Add(new PlayerListView(item.AsBsonDocument,this));

                        }
                    }
                    else
                    {
                        PageAUT.Placeholder.Children.Add(new SubPageAddPlayer());
                    }
                }
                else
                {
                    Debug.WriteLine("Error recieve player list");
                }
            });
        }

    }


    public interface IPagePlayer
    {

        void Init();
    }

}
