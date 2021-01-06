using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageInternalNotifaction.Elements;
using Dashboard.GlobalElement;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageInternalNotifaction
{
    /// <summary>
    /// Interaction logic for InternalNotifaction.xaml
    /// </summary>
    public partial class InternalNotifaction : UserControl
    {

        ModelInternalNotifaction[] Notifactions;
        int CountNotifaction = 10;

        public InternalNotifaction()
        {
            InitializeComponent();

            ReciveLogs();

            //reload list
            BTNReload.MouseDown += (s, o) =>
            {
                ReciveLogs();
            };

            //remove all Notifaction in seces
            BTNDeleteAllView.MouseDown += (o, s) =>
            {
                foreach (var item in Notifactions)
                {

                    SDK.SDK_PageDashboards.DashboardGame.PageLog.DeleteLog(item.Detail, resultDelete =>
                    {
                        if (resultDelete)
                        {
                            PlaceContentNotifacction.Children.Remove(item);
                        }
                    });

                    ReciveLogs();
                };

            };

            //++ countnotifaction
            BTNReciveMore.MouseDown += (o, s) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveMonetize(result =>
                {
                    if (CountNotifaction + 10 <= result["Logs"].ToInt32())
                    {
                        CountNotifaction += 10;
                        ReciveLogs();
                    }
                    else
                    {
                        TextHistoryCount.Text += $" \\ {result["Logs"]}";
                        DashboardGame.Notifaction($"The value \"{result["Logs"]}\" can be read from history. To see more of Payments Buy Now!", Notifaction.StatusMessage.Warrning);
                    }

                }, () => { });

            };

            CloseArea.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    DoubleAnimation Opacity = new DoubleAnimation();
                    Opacity.From = 1;
                    Opacity.To = 0;
                    Opacity.Duration = new Duration(TimeSpan.FromSeconds(0.3));
                    Opacity.Completed += (o, ss) =>
                    {
                        Close(null, null);
                    };

                    Storyboard.SetTargetName(Opacity, Root.Name);
                    Storyboard.SetTargetProperty(Opacity, new PropertyPath("Opacity"));

                    Storyboard MyStory = new Storyboard();

                    MyStory.Children.Add(Opacity);
                    MyStory.Begin(this);

                }
            };

            //mark Notifaction as read
            SDK.SDK_PageDashboards.DashboardGame.PageLog.MarkNotifactionasRead(result =>
            {
                if (result)
                {
                    ReciveLogs();
                }
                else
                {

                }
            });
        }


        /// <summary>
        /// 1: recive logs 
        /// 2: instance Model notifaction
        /// 3 btn delete all init
        /// </summary>
        void ReciveLogs()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLog.ReciveLog(CountNotifaction,
                result =>
                {
                    TextHistoryCount.Text = result[1].AsBsonArray.Count.ToString();
                    Notifactions = new ModelInternalNotifaction[result[1].AsBsonArray.Count];

                    PlaceContentNotifacction.Children.Clear();

                    var Count = 0;
                    foreach (var item in result[1].AsBsonArray)
                    {
                        Notifactions[Count] = new ModelInternalNotifaction(item.AsBsonDocument, ReciveLogs);
                        PlaceContentNotifacction.Children.Add(Notifactions[Count]);
                        Count++;
                    }
                },
                () =>
                {

                }
                );

        }

        private void Close(object sender, EventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }
    }
}
