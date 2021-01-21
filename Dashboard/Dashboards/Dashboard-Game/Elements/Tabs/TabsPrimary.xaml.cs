using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Tabs
{

    public partial class TabsPrimary : UserControl
    {
        public bool IsSelected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (value)
                {

                    TextTab.FontWeight = FontWeights.Bold;
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0f62fe");

                    if (Parent.GetType() == typeof(StackPanel))
                    {
                        foreach (var item in (Parent as StackPanel).Children)
                        {
                            if (item != this)
                            {
                                (item as TabsPrimary).IsSelected = false;
                            }
                        }
                    }
                    else if (Parent.GetType() == typeof(Grid))
                    {

                    }
                }
                else
                {
                    TextTab.FontWeight = FontWeights.Normal;
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#e0e0e0");
                }

                _Selected = value;
            }
        }

        bool _Selected;

        public TabsPrimary()
        {

            InitializeComponent();

            //action seleceted
            MouseDown += (s, e) =>
            {
                if (!_Selected)
                {
                    IsSelected = true;
                }

            };

            //action mouseover
            MouseEnter += (s, e) =>
            {
                if (!IsSelected&&!Bordedayimamad.IsFocused )
                {
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#a8a8a8");
                }
            };

            //action mouse leave
            MouseLeave += (s, e) =>
            {
                if (!IsSelected&&!Bordedayimamad.IsFocused)
                {
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#e0e0e0");
                }

            };

            GotFocus += (s, e) =>
            {
                Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                Bordedayimamad.BorderThickness = new Thickness(2);
            };
            LostFocus += (s, e) =>
            {
                if (!IsSelected)
                {
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#e0e0e0");
                    Bordedayimamad.BorderThickness = new Thickness(0, 0, 0, 2);
                }
                else
                {
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0f62fe");
                    Bordedayimamad.BorderThickness = new Thickness(0, 0, 0, 2);
                }
            };


            IsEnabledChanged += (s, e) =>
            {
                if (IsEnabled)
                {
                    TextTab.Foreground = (Brush)new BrushConverter().ConvertFromString("#393939");

                    if (IsSelected)
                    {
                        TextTab.FontWeight = FontWeights.Bold;
                        Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0f62fe");
                        Bordedayimamad.BorderThickness = new Thickness(0, 0, 0, 2);

                    }
                    else
                    {
                        TextTab.FontWeight = FontWeights.Normal;
                        Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#e0e0e0");
                        Bordedayimamad.BorderThickness = new Thickness(0, 0, 0, 2);
                    }

                }
                else
                {
                    TextTab.Foreground = (Brush)new BrushConverter().ConvertFromString("#e0e0e0");
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#e0e0e0");
                    Bordedayimamad.BorderThickness = new Thickness(0, 0, 0, 2);
                }

                if (IsFocused)
                {
                    Bordedayimamad.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
                    Bordedayimamad.BorderThickness = new Thickness(2);
                }
            };


        }
    }
}
