using Dashboard.Dashboards.Pages.Aut;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Pages.PageStudios.Moduls.SubPageAddNewStudio
{
    /// <summary>
    /// Interaction logic for SubPageAddNewStudio.xaml
    /// </summary>
    public partial class SubPageAddNewStudio : UserControl
    {
        public SubPageAddNewStudio(IPageStudio PageStudio)
        {
            InitializeComponent();
            //action btn close
            BTNClose.MouseDown += (s, e) =>
            {
                ShowPage(0, () =>
                {
                    Visibility = Visibility.Collapsed;
                });
            };


            //Actin btn add new Studio
            BTNAdd.Worker += () =>
            {
                if (InputNameStudio.Text.Length >= 5)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageStudios.CreatStudio(InputNameStudio.Text, Result =>
                    {
                        if (Result)
                        {
                            Debug.WriteLine("Created");
                            Visibility = Visibility.Collapsed;

                            PageStudio.InitStudios();
                        }
                        else
                        {
                            Debug.WriteLine("NotCreated");
                        }

                    });

                }
                else
                {
                    Debug.WriteLine("Input Name Studio Short");
                }
            };

        }

        void ShowPage(int To, Action Act = null)
        {
            var Current = 0;

            if (To != 0)
            {
                Current = 0;
            }
            else
            {
                Current = 1;
            }

            DoubleAnimation Anim = new DoubleAnimation(Current, To, TimeSpan.FromSeconds(0.3));
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
