﻿using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios;
using Dashboard.GlobalElement;
using Dashboard.Properties;
using System;
using System.Net.Mail;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PageAUT.Login
{
    public partial class Register : UserControl
    {

        public Register()
        {
            InitializeComponent();
        }

        private void _Login(object sender, MouseButtonEventArgs e)
        {
            var _parent = Parent as Grid;
            _parent.Children.Remove(this);
            _parent.Children.Add(new Login());
        }

        private void _Register(object sender, MouseButtonEventArgs e)
        {
            if (TextUsername.Text.Length >= 6 && TextPassword.Password.Length >= 6)
            {
                try
                {
                    new MailAddress(TextEmail.Text);

                    SDK.SDK_PageAUT.Register(TextUsername.Text, TextPassword.Password, TextEmail.Text, TextPhone.Text,
                        (Token) =>
                        {
                            DashboardGame.Notifaction("Registered", StatusMessage.Ok);

                            Settings.Default._id = Token;
                            Settings.Default.Save();


                            //remove page and Effect
                            DashboardGame.Dashboard.Blure(false);
                            DashboardGame.Dashboard.Root.Children.Remove(this);

                            DashboardGame.Dashboard.Root.Children.Add(new SubPageStudios());

                        },
                        () =>
                        {
                            DashboardGame.Notifaction("Faild to Register", StatusMessage.Error);
                        });
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, StatusMessage.Warrning);
                }

            }
            else
            {
                DashboardGame.Notifaction("Username or Password Short", StatusMessage.Error);
            }
        }

        private void CheackUsername(object sender, TextChangedEventArgs e)
        {
            var Text = sender as TextBox;

            if (Text.Text.Length >= 6)
            {
                SDK.SDK_PageAUT.CheackUsername(Text.Text, result =>
                {
                    if (result)
                    {
                        Text.Text += new Random().Next();
                        DashboardGame.Notifaction("Username available", StatusMessage.Warrning);
                    }

                    Text.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                });
            }
            else
            {
                Text.BorderBrush = new SolidColorBrush(Colors.Tomato);
            }
        }

        Action InitDashbaord;
    }
}
