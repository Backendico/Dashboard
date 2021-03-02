using Dashboard.GlobalElement;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.SubPages.PageContent.Moduls.AddContent
{
    /// <summary>
    /// Interaction logic for AddContent.xaml
    /// </summary>
    public partial class AddContent : UserControl
    {
        public AddContent(IPageContent editor)
        {
            InitializeComponent();
            ShowPage(1);


            //action btn close
            BTNClose.MouseDown += (s, e) =>
            {
                ShowPage(0, () =>
                {
                    Visibility = Visibility.Collapsed;
                });
            };


            //action btn add contents
            BTNAddContent.Worker += () =>
            {
                if (TextName.Text.Length >= 1)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageContent.AddContent(TextName.Text, result =>
                    {
                        if (result)
                        {
                            editor.Init();
                        }
                        else
                        {
                            Debug.WriteLine("Faild Add");
                        }
                        Debug.WriteLine(result);

                    });

                }
            };

        }

        void ShowPage(int TO, Action Act = null)
        {
            var Current = 0;

            if (TO != 0)
            {
                Current = 0;
            }
            else
            {
                Current = 1;
            }

            DoubleAnimation Anim = new DoubleAnimation(Current, TO, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                if (Act != null)
                {
                    Act();
                }
            };
            Storyboard.SetTargetName(Anim, Root.Name);
            Storyboard.SetTargetProperty(Anim, new System.Windows.PropertyPath("Opacity"));
            Storyboard story = new Storyboard();
            story.Children.Add(Anim);
            story.Begin(this);
        }
    }
}
