using Dashboard.GlobalElement;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    public partial class SearchUser : UserControl
    {
        public SearchUser()
        {
            InitializeComponent();
        }

        private void SearchUsername(object sender, RoutedEventArgs e)
        {

            //cheack leght username
            if (TextBoxUsername.Text.Length >= 6)
            {
                //recive detail
                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchUsername(TextBoxUsername.Text,
                    result =>
                    {
                        //cheack fill  place result
                        try
                        {
                            PlaceResult.Children.RemoveAt(1);
                            PlaceResult.Children.Add(new ModelAbstractUser(result));
                        }
                        catch (Exception)
                        {
                            PlaceResult.Children.Add(new ModelAbstractUser(result));
                        }


                    },
                    () =>
                    {

                        MessageBox.Show("Not Found");
                    });

            }
            else
            {
                MessageBox.Show("Username Short(More than 6 charecter)");
            }
        }


        private void SearchEmail(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchEmail(TextboxEmail.Text,
                result =>
                {
                    try
                    {
                        PlaceResult.Children.RemoveAt(1);
                        PlaceResult.Children.Add(new ModelAbstractUser(result));
                    }
                    catch (Exception)
                    {
                        PlaceResult.Children.Add(new ModelAbstractUser(result));
                    }

                },
                () =>
                {
                    MessageBox.Show("Not Found");

                });
        }


        private void SearchToken(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchToken(TextboxToken.Text,
                result =>
                {
                    try
                    {
                        PlaceResult.Children.RemoveAt(1);
                        PlaceResult.Children.Add(new ModelAbstractUser(result));
                    }
                    catch (Exception)
                    {
                        PlaceResult.Children.Add(new ModelAbstractUser(result));
                    }
                },
                () =>
                {
                    MessageBox.Show("Not Found");
                });


        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }

    }
}

