﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{

    public partial class BTNPrimary : UserControl
    {
        public string Text
        {
            get { return _Textbutton; }
            set
            {
                NameButton.Text = value;
                _Textbutton = value;
            }
        }

        string _Textbutton;
        public BTNPrimary()
        {
            InitializeComponent();

            MouseDown += (s, e) =>
            {
                if (Worker != null)
                {
                    Worker();
                }
            };


            GotFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(2);
            };

            LostFocus += (s, e) =>
            {

                BorderFocuse.BorderThickness = new Thickness(0);
            };

        }

        public event Action Worker;
    }
}
