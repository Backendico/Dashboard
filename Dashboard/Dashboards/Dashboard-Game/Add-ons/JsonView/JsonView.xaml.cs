using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonView
{
    /// <summary>
    /// Interaction logic for JsonView.xaml
    /// </summary>
    public partial class JsonView : UserControl
    {
        public JsonView(BsonDocument JsonData)
        {
            InitializeComponent();

            PlaceElement.Children.Add(new ElementObject(JsonData));

            BTNSave.Click += (s, e) =>
            {
                ShowOffPanelAchievements();
            };
        }


        void ShowOffPanelAchievements()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                Root.Visibility = Visibility.Collapsed;

                Root.Visibility = Visibility.Collapsed;

                DashboardGame.Dashboard.Root.Children.Remove(this);

            };

            Storyboard.SetTargetName(Anim, Root.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        public class Elements : StackPanel
        {

            internal new TextBlock Name;
            internal TextBlock Value;
            internal TextBlock Type;
            internal Border ShowMore;
            internal StackPanel PlaceSubElements;

            public void Init(BsonElement Data, BsonDocument MainData)
            {
                Background = new SolidColorBrush(Colors.Transparent);

                Margin = new Thickness(0, 5, 0, 5);

                var Grid = new Grid();
                Children.Add(Grid);


                var PlaceSubElements = new StackPanel() { Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.WhiteSmoke), Margin = new Thickness(60, 0, 0, 0) };
                Children.Add(PlaceSubElements);

                Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { });



                //Show More
                var ShowMore = new Border() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Left, Height = 20, Width = 20, Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.LightGray), CornerRadius = new CornerRadius(5) }; ;
                ShowMore.Child = new TextBlock() { Cursor = Cursors.Hand, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 15, Text = "\xE974", FontFamily = new FontFamily("Segoe MDL2 Assets") };
                var Show = false;
                ShowMore.MouseDown += (s, e) =>
                {
                    if (!Show)
                    {
                        Show = true;
                        (ShowMore.Child as TextBlock).Text = "\xE972";
                        PlaceSubElements.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        Show = false;
                        (ShowMore.Child as TextBlock).Text = "\xE974";
                        PlaceSubElements.Visibility = Visibility.Collapsed;
                    }
                };
                Grid.Children.Add(ShowMore);
                Grid.SetColumn(ShowMore, 0);
                Grid.SetRow(ShowMore, 0);


                //Name
                var Name = new TextBlock() { Margin = new Thickness(0, 0, 5, 0), MinWidth = 100, Text = Data.Name, Foreground = new SolidColorBrush(Colors.Black), TextAlignment = TextAlignment.Left };
                Grid.Children.Add(Name);
                Grid.SetColumn(Name, 1);
                Grid.SetRow(Name, 0);

                //Value
                var Value = new TextBlock() { Margin = new Thickness(0, 0, 5, 0), TextAlignment = TextAlignment.Left, MinWidth = 100, Text = Data.Value.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
                Grid.SetColumn(Value, 2);
                Grid.Children.Add(Value);
                Grid.SetRow(Value, 0);

                //type
                var Type = new TextBlock() { TextAlignment = TextAlignment.Right, Text = Data.Value.BsonType.ToString() };
                Grid.Children.Add(Type);
                Grid.SetColumn(Type, 3);
                Grid.SetRow(Type, 0);


                //actions
                MouseEnter += (s, e) =>
                {
                    Background = new SolidColorBrush(Colors.WhiteSmoke);
                };
                MouseLeave += (s, e) =>
                {
                    Background = new SolidColorBrush(Colors.Transparent);
                };

                this.Name = Name;
                this.Value = Value;
                this.Type = Type;
                this.ShowMore = ShowMore;
                this.PlaceSubElements = PlaceSubElements;
            }

        }

        public class ElementObject : StackPanel
        {
            public ElementObject(BsonDocument Data)
            {

                foreach (var item in Data.AsBsonDocument)
                {

                    switch (item.Value.BsonType)
                    {
                        case BsonType.EndOfDocument:
                            Children.Add(new TextBlock() { Text = "End" });
                            break;
                        case BsonType.Double:
                            Children.Add(new ElementDouble(item, Data));
                            break;
                        case BsonType.String:
                            Children.Add(new ElementString(item, Data));
                            break;
                        case BsonType.Document:
                            Children.Add(new ElementsDocument(item, Data));
                            break;
                        case BsonType.Array:
                            Children.Add(new ElementsArray(item, Data));
                            break;
                        case BsonType.Binary:
                            Children.Add(new ElementsBinary(item, Data));
                            break;
                        case BsonType.Undefined:
                            Children.Add(new ElementUndifined(item, Data));
                            break;
                        case BsonType.ObjectId:
                            Children.Add(new ElementsObjectid(item, Data));
                            break;
                        case BsonType.Boolean:
                            Children.Add(new ElementsBoolean(item, Data));
                            break;
                        case BsonType.DateTime:
                            Children.Add(new ElementsDateTime(item, Data));
                            break;
                        case BsonType.Null:
                            Children.Add(new ElementNull(item, Data));
                            break;
                        case BsonType.RegularExpression:
                            Children.Add(new ElementRegexpretion(item, Data));
                            break;
                        case BsonType.JavaScript:
                            Children.Add(new TextBlock() { Text = "JavaScript" });
                            break;
                        case BsonType.Symbol:
                            Children.Add(new ElementSymbol(item, Data));
                            break;
                        case BsonType.JavaScriptWithScope:
                            Children.Add(new ElementsCode(item, Data));
                            break;
                        case BsonType.Int32:
                            Children.Add(new ElementsInt32(item, Data));
                            break;
                        case BsonType.Timestamp:
                            Children.Add(new ElementTimeStamp(item, Data));
                            break;
                        case BsonType.Int64:
                            Children.Add(new ElementsInt64(item, Data));
                            break;
                        case BsonType.Decimal128:
                            Children.Add(new ElementDecimal128(item, Data));
                            break;
                        case BsonType.MinKey:
                            Children.Add(new ElementMinKey(item, Data));
                            break;
                        case BsonType.MaxKey:
                            Children.Add(new ElementMaxKey(item, Data));
                            break;
                    }
                }

            }
        }

        public class ElementsArray : Elements
        {
            public ElementsArray(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);

                Value.Text = "Array";
                Value.IsEnabled = false;
                ShowMore.Visibility = Visibility.Visible;


                InitInternalArray();


                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };

                void InitInternalArray()
                {
                    PlaceSubElements.Children.Clear();

                    int Count = 0;
                    foreach (var item in Data.Value.AsBsonArray)
                    {
                        switch (item.BsonType)
                        {
                            case BsonType.EndOfDocument:
                                PlaceSubElements.Children.Add(new TextBlock() { Text = "AEnd" });
                                break;
                            case BsonType.Double:
                                PlaceSubElements.Children.Add(new ElementDoubleArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.String:
                                PlaceSubElements.Children.Add(new ElementStringArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Document:
                                PlaceSubElements.Children.Add(new ElementObjectArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Array:
                                PlaceSubElements.Children.Add(new ElementArrayArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Binary:
                                PlaceSubElements.Children.Add(new ElementBinaryArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Undefined:
                                PlaceSubElements.Children.Add(new ElementUndifineArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.ObjectId:
                                PlaceSubElements.Children.Add(new ElementObjectIDArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Boolean:
                                PlaceSubElements.Children.Add(new ElementBooleanArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.DateTime:
                                PlaceSubElements.Children.Add(new ElementDateTimeArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Null:
                                PlaceSubElements.Children.Add(new ElementNullArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.RegularExpression:
                                PlaceSubElements.Children.Add(new ElementRegularExpresionArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.JavaScript:
                                PlaceSubElements.Children.Add(new TextBlock() { Text = "AJavaScript" });
                                break;
                            case BsonType.Symbol:
                                PlaceSubElements.Children.Add(new ElementSymbolArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.JavaScriptWithScope:
                                PlaceSubElements.Children.Add(new ElementCodeArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Int32:
                                PlaceSubElements.Children.Add(new ElementInt32Array(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Timestamp:
                                PlaceSubElements.Children.Add(new ElementTimeStampArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Int64:
                                PlaceSubElements.Children.Add(new ElementInt64Array(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.Decimal128:
                                PlaceSubElements.Children.Add(new ElementDecimal128Array(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.MinKey:
                                PlaceSubElements.Children.Add(new ElementMinKeyArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                            case BsonType.MaxKey:
                                PlaceSubElements.Children.Add(new ElementMaxKeyArray(Count, item, Data.Value.AsBsonArray, InitInternalArray));
                                break;
                        }
                        Count++;
                    }

                }

            }


            public class FElementArray : StackPanel
            {

                public TextBlock Name;
                public TextBlock Value;
                public TextBlock Type;
                public Border ShowMore;
                public StackPanel PlaceSubElements;


                public void Init(int Postion, BsonValue Data)
                {
                    Background = new SolidColorBrush(Colors.Transparent);

                    Margin = new Thickness(0, 5, 0, 5);

                    var Grid = new Grid();
                    Children.Add(Grid);


                    var PlaceSubElements = new StackPanel() { Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.WhiteSmoke), Margin = new Thickness(60, 0, 0, 0) };
                    Children.Add(PlaceSubElements);


                    Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                    Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                    Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                    Grid.ColumnDefinitions.Add(new ColumnDefinition() { });

                    //Show More
                    var ShowMore = new Border() { Height = 20, Width = 20, Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.LightGray), CornerRadius = new CornerRadius(5) }; ;
                    ShowMore.Child = new TextBlock() { Cursor = Cursors.Hand, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 15, Text = "\xE974", FontFamily = new FontFamily("Segoe MDL2 Assets") };
                    var Show = false;
                    ShowMore.MouseDown += (s, e) =>
                    {
                        if (!Show)
                        {
                            Show = true;
                            (ShowMore.Child as TextBlock).Text = "\xE972";
                            PlaceSubElements.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            Show = false;
                            (ShowMore.Child as TextBlock).Text = "\xE974";
                            PlaceSubElements.Visibility = Visibility.Collapsed;
                        }
                    };
                    Grid.Children.Add(ShowMore);
                    Grid.SetColumn(ShowMore, 0);
                    Grid.SetRow(ShowMore, 0);


                    //Name
                    var Name = new TextBlock() { Margin = new Thickness(0, 0, 5, 0), TextAlignment = TextAlignment.Left, MinWidth = 100, Text = Postion.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
                    Grid.Children.Add(Name);
                    Grid.SetColumn(Name, 1);
                    Grid.SetRow(Name, 0);

                    //Value
                    var Value = new TextBlock() { Margin = new Thickness(0, 0, 5, 0), MinWidth = 100, Text = Data.ToString(), TextAlignment = TextAlignment.Left, Foreground = new SolidColorBrush(Colors.Black) };
                    Grid.SetColumn(Value, 2);
                    Grid.Children.Add(Value);
                    Grid.SetRow(Value, 0);

                    //type
                    var Type = new TextBlock() { Text = Data.BsonType.ToString(), TextAlignment = TextAlignment.Right };
                    Grid.Children.Add(Type);
                    Grid.SetColumn(Type, 3);
                    Grid.SetRow(Type, 0);

                    this.Name = Name;
                    this.Value = Value;
                    this.Type = Type;
                    this.ShowMore = ShowMore;
                    this.PlaceSubElements = PlaceSubElements;

                }

            }

            public class ElementArrayArray : FElementArray
            {
                public ElementArrayArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.Text = "Array";
                    Value.IsEnabled = false;

                    ShowMore.Visibility = Visibility.Visible;

                    InitInternalArray();

                    void InitInternalArray()
                    {
                        PlaceSubElements.Children.Clear();
                        var Count = 0;

                        foreach (var item in Data.AsBsonArray)
                        {
                            switch (item.BsonType)
                            {
                                case BsonType.EndOfDocument:
                                    PlaceSubElements.Children.Add(new TextBlock() { Text = "AEnd" });
                                    break;
                                case BsonType.Double:
                                    PlaceSubElements.Children.Add(new ElementDoubleArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.String:
                                    PlaceSubElements.Children.Add(new ElementStringArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Document:
                                    PlaceSubElements.Children.Add(new ElementObjectArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Array:
                                    PlaceSubElements.Children.Add(new ElementArrayArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Binary:
                                    PlaceSubElements.Children.Add(new ElementBinaryArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Undefined:
                                    PlaceSubElements.Children.Add(new ElementUndifineArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.ObjectId:
                                    PlaceSubElements.Children.Add(new ElementObjectIDArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Boolean:
                                    PlaceSubElements.Children.Add(new ElementBooleanArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.DateTime:
                                    PlaceSubElements.Children.Add(new ElementDateTimeArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Null:
                                    PlaceSubElements.Children.Add(new ElementNullArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.RegularExpression:
                                    PlaceSubElements.Children.Add(new ElementRegularExpresionArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.JavaScript:
                                    PlaceSubElements.Children.Add(new TextBlock() { Text = "AJavaScript" });
                                    break;
                                case BsonType.Symbol:
                                    PlaceSubElements.Children.Add(new ElementSymbolArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.JavaScriptWithScope:
                                    PlaceSubElements.Children.Add(new ElementCodeArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Int32:
                                    PlaceSubElements.Children.Add(new ElementInt32Array(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Timestamp:
                                    PlaceSubElements.Children.Add(new ElementTimeStampArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Int64:
                                    PlaceSubElements.Children.Add(new ElementInt64Array(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.Decimal128:
                                    PlaceSubElements.Children.Add(new ElementDecimal128Array(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.MinKey:
                                    PlaceSubElements.Children.Add(new ElementMinKeyArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                                case BsonType.MaxKey:
                                    PlaceSubElements.Children.Add(new ElementMaxKeyArray(Count, item, Data.AsBsonArray, InitInternalArray));
                                    break;
                            }
                            Count++;
                        }
                    }
                }

            }

            public class ElementBinaryArray : FElementArray
            {
                public ElementBinaryArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.IsEnabled = false;

                }

            }

            public class ElementBooleanArray : FElementArray
            {
                public ElementBooleanArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        try
                        {
                            MainArray[Postion] = Boolean.Parse(Value.Text);

                            Background = new SolidColorBrush(Colors.Transparent);
                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            Background = new SolidColorBrush(Colors.Tomato);
                            Value.Text = Data.ToString();
                        }

                    };

                }
            }

            public class ElementCodeArray : FElementArray
            {
                public ElementCodeArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.IsEnabled = false;
                }
            }

            public class ElementDateTimeArray : FElementArray
            {
                public ElementDateTimeArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        try
                        {
                            MainArray[Postion] = DateTime.Parse(Value.Text);

                            Background = new SolidColorBrush(Colors.Transparent);
                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            Background = new SolidColorBrush(Colors.Tomato);
                            Value.Text = Data.ToString();
                        }

                    };
                }
            }

            public class ElementDecimal128Array : FElementArray
            {
                public ElementDecimal128Array(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        try
                        {
                            MainArray[Postion] = Decimal128.Parse(Value.Text);

                            Background = new SolidColorBrush(Colors.Transparent);
                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            Background = new SolidColorBrush(Colors.Tomato);
                            Value.Text = Data.ToString();
                        }

                    };

                }
            }

            public class ElementDoubleArray : FElementArray
            {
                public ElementDoubleArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        try
                        {
                            MainArray[Postion] = Double.Parse(Value.Text);

                            Background = new SolidColorBrush(Colors.Transparent);
                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            Background = new SolidColorBrush(Colors.Tomato);
                            Value.Text = Data.ToString();
                        }

                    };
                }
            }

            public class ElementInt32Array : FElementArray
            {
                public ElementInt32Array(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        try
                        {
                            MainArray[Postion] = Int32.Parse(Value.Text);

                            Background = new SolidColorBrush(Colors.Transparent);
                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            Background = new SolidColorBrush(Colors.Tomato);
                            Value.Text = Data.ToString();
                        }

                    };
                }
            }

            public class ElementInt64Array : FElementArray
            {
                public ElementInt64Array(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        try
                        {
                            MainArray[Postion] = Int64.Parse(Value.Text);

                            Background = new SolidColorBrush(Colors.Transparent);
                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            Background = new SolidColorBrush(Colors.Tomato);
                            Value.Text = Data.ToString();
                        }

                    };

                }
            }


            public class ElementMinKeyArray : FElementArray
            {
                public ElementMinKeyArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.IsEnabled = false;

                }
            }

            public class ElementMaxKeyArray : FElementArray
            {
                public ElementMaxKeyArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.IsEnabled = false;
                }
            }

            public class ElementNullArray : FElementArray
            {
                public ElementNullArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.Text = "Null";
                    Value.IsEnabled = false;
                }
            }

            public class ElementObjectArray : FElementArray
            {
                public ElementObjectArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.Text = "Object";
                    Value.IsEnabled = false;
                    ShowMore.Visibility = Visibility.Visible;

                    InitInternalObjects();

                    void InitInternalObjects()
                    {
                        PlaceSubElements.Children.Clear();

                        foreach (var item in Data.AsBsonDocument)
                        {
                            switch (item.Value.BsonType)
                            {
                                case BsonType.EndOfDocument:
                                    break;
                                case BsonType.Double:
                                    {
                                        PlaceSubElements.Children.Add(new ElementDouble(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.String:
                                    {
                                        PlaceSubElements.Children.Add(new ElementString(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Document:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsDocument(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Array:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsArray(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Binary:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsBinary(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Undefined:
                                    {
                                        PlaceSubElements.Children.Add(new ElementUndifined(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.ObjectId:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsObjectid(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Boolean:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsBoolean(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.DateTime:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsDateTime(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Null:
                                    {
                                        PlaceSubElements.Children.Add(new ElementNull(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.RegularExpression:
                                    {
                                        PlaceSubElements.Children.Add(new ElementRegexpretion(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.JavaScript:
                                    break;
                                case BsonType.Symbol:
                                    {
                                        PlaceSubElements.Children.Add(new ElementSymbol(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.JavaScriptWithScope:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsCode(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Int32:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsInt32(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Timestamp:
                                    {
                                        PlaceSubElements.Children.Add(new ElementTimeStamp(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Int64:
                                    {
                                        PlaceSubElements.Children.Add(new ElementsInt64(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.Decimal128:
                                    {
                                        PlaceSubElements.Children.Add(new ElementDecimal128(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.MinKey:
                                    {
                                        PlaceSubElements.Children.Add(new ElementMinKey(item, Data.AsBsonDocument));
                                    }
                                    break;
                                case BsonType.MaxKey:
                                    {
                                        PlaceSubElements.Children.Add(new ElementMaxKey(item, Data.AsBsonDocument));
                                    }
                                    break;

                            }
                        }

                    }
                }
            }

            public class ElementObjectIDArray : FElementArray
            {
                public ElementObjectIDArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.IsEnabled = false;

                }
            }

            public class ElementRegularExpresionArray : FElementArray
            {
                public ElementRegularExpresionArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.IsEnabled = false;

                }
            }

            public class ElementStringArray : FElementArray
            {
                public ElementStringArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        MainArray[Postion] = Value.Text;
                    };

                }
            }

            public class ElementSymbolArray : FElementArray
            {
                public ElementSymbolArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {

                    Init(Postion, Data);

                    Value.LostFocus += (ss, ee) =>
                    {
                        try
                        {
                            MainArray[Postion] = BsonSymbol.Create(Value.Text);

                            Background = new SolidColorBrush(Colors.Transparent);
                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            Background = new SolidColorBrush(Colors.Tomato);
                            Value.Text = Data.ToString();
                        }

                    };

                }

            }

            public class ElementTimeStampArray : FElementArray
            {
                public ElementTimeStampArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.IsEnabled = false;
                }

            }

            public class ElementUndifineArray : FElementArray
            {
                public ElementUndifineArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
                {
                    Init(Postion, Data);
                    Value.Text = "Undifine";
                    Value.IsEnabled = false;

                }
            }
        }

        public class ElementString : Elements
        {
            public ElementString(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.LostFocus += (s, e) =>
                {
                    if (Value.Text.Length >= 1)
                    {

                        Data = new BsonElement(Data.Name, Value.Text);
                        MainData.SetElement(Data);
                    }
                    else
                    {
                        Data = new BsonElement(Data.Name, " ");
                        MainData.SetElement(Data);
                    }
                };

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementsBoolean : Elements
        {
            public ElementsBoolean(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);


                Value.LostFocus += (ss, ee) =>
                {
                    try
                    {
                        Data = new BsonElement(Data.Name, bool.Parse(Value.Text));

                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);

                    }
                    catch (Exception ex)
                    {
                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        Background = new SolidColorBrush(Colors.Tomato);
                        Value.Text = Data.Value.ToString();
                    }

                };

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };

            }
        }

        public class ElementsObjectid : Elements
        {
            public ElementsObjectid(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.IsEnabled = false;

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementsDateTime : Elements
        {
            public ElementsDateTime(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);

                Value.LostFocus += (s1, e1) =>
                {

                    try
                    {
                        Data = new BsonElement(Data.Name, DateTime.Parse(Value.Text));
                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);
                        Debug.WriteLine("hi");
                    }
                    catch (Exception ex)
                    {
                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        Background = new SolidColorBrush(Colors.Tomato);
                        Value.Text = Data.Value.ToString();
                    }

                };

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementsDocument : Elements
        {
            public ElementsDocument(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.Text = "Object";
                Value.IsEnabled = false;
                ShowMore.Visibility = Visibility.Visible;

                foreach (var item in Data.Value.AsBsonDocument)
                {
                    switch (item.Value.BsonType)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                PlaceSubElements.Children.Add(new ElementDouble(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.String:
                            {
                                PlaceSubElements.Children.Add(new ElementString(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Document:
                            {
                                PlaceSubElements.Children.Add(new ElementsDocument(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Array:
                            {
                                PlaceSubElements.Children.Add(new ElementsArray(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Binary:
                            {
                                PlaceSubElements.Children.Add(new ElementsBinary(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                PlaceSubElements.Children.Add(new ElementUndifined(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                PlaceSubElements.Children.Add(new ElementsObjectid(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                PlaceSubElements.Children.Add(new ElementsBoolean(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.DateTime:
                            {
                                PlaceSubElements.Children.Add(new ElementsDateTime(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Null:
                            {
                                PlaceSubElements.Children.Add(new ElementNull(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                PlaceSubElements.Children.Add(new ElementRegexpretion(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                PlaceSubElements.Children.Add(new ElementSymbol(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                PlaceSubElements.Children.Add(new ElementsCode(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Int32:
                            {
                                PlaceSubElements.Children.Add(new ElementsInt32(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                PlaceSubElements.Children.Add(new ElementTimeStamp(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Int64:
                            {
                                PlaceSubElements.Children.Add(new ElementsInt64(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                PlaceSubElements.Children.Add(new ElementDecimal128(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.MinKey:
                            {
                                PlaceSubElements.Children.Add(new ElementMinKey(item, Data.Value.AsBsonDocument));
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                PlaceSubElements.Children.Add(new ElementMaxKey(item, Data.Value.AsBsonDocument));
                            }
                            break;

                    }
                }

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementsInt32 : Elements
        {
            public ElementsInt32(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);


                Value.LostFocus += (ss, ee) =>
                {
                    try
                    {
                        Data = new BsonElement(Data.Name, Int32.Parse(Value.Text));

                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);

                    }
                    catch (Exception ex)
                    {
                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        Background = new SolidColorBrush(Colors.Tomato);
                        Value.Text = Data.Value.ToString();
                    }

                };


                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };

            }
        }

        public class ElementsInt64 : Elements
        {
            public ElementsInt64(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);


                Value.LostFocus += (s1, e1) =>
                {
                    try
                    {
                        Data = new BsonElement(Data.Name, Int64.Parse(Value.Text));

                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);
                    }
                    catch (Exception ex)
                    {
                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        Background = new SolidColorBrush(Colors.Tomato);
                        Value.Text = Data.Value.ToString();
                    }

                };

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementsBinary : Elements
        {
            public ElementsBinary(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.IsEnabled = false;

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };



            }
        }

        public class ElementsCode : Elements
        {
            public ElementsCode(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);

                Value.IsEnabled = false;

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementDecimal128 : Elements
        {
            public ElementDecimal128(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);

                Value.LostFocus += (s1, e1) =>
                {
                    try
                    {

                        Data = new BsonElement(Data.Name, Decimal128.Parse(Value.Text));

                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);
                    }
                    catch (Exception ex)
                    {
                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        Background = new SolidColorBrush(Colors.Tomato);
                        Value.Text = Data.Value.ToString();
                    }

                };

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementDouble : Elements
        {
            public ElementDouble(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);


                Value.LostFocus += (s1, e1) =>
                {
                    try
                    {
                        Data = new BsonElement(Data.Name, Double.Parse(Value.Text));

                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);
                    }
                    catch (Exception ex)
                    {
                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        Background = new SolidColorBrush(Colors.Tomato);
                        Value.Text = Data.Value.ToString();
                    }

                };
                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementMaxKey : Elements
        {
            public ElementMaxKey(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.IsEnabled = false;
                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };

            }
        }

        public class ElementMinKey : Elements
        {
            public ElementMinKey(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.IsEnabled = false;

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };

            }
        }

        public class ElementNull : Elements
        {
            public ElementNull(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.IsEnabled = false;
                Value.Text = "Null";

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };

            }
        }

        public class ElementRegexpretion : Elements
        {
            public ElementRegexpretion(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.IsEnabled = false;

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementSymbol : Elements
        {
            public ElementSymbol(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);

                Value.LostFocus += (s1, e1) =>
                {
                    try
                    {
                        Data = new BsonElement(Data.Name, BsonSymbol.Create(Value.Text));

                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);
                    }
                    catch (Exception ex)
                    {
                        DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        Background = new SolidColorBrush(Colors.Tomato);
                        Value.Text = Data.Value.ToString();
                    }

                };


                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementTimeStamp : Elements
        {
            public ElementTimeStamp(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);
                Value.IsEnabled = false;

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;

                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };


            }
        }

        public class ElementUndifined : Elements
        {
            public ElementUndifined(BsonElement Data, BsonDocument MainData)
            {
                Init(Data, MainData);

                Value.IsEnabled = false;
                Value.Text = "Undifined";

                Name.LostFocus += (s, e) =>
                {
                    if (Name.Text != Data.Name)
                    {
                        if (Name.Text.Length >= 1)
                        {
                            try
                            {
                                _ = MainData[Name.Text];
                                Name.Text = Data.Name;
                                DashboardGame.Notifaction("Duplicate element", Notifaction.StatusMessage.Error);
                            }
                            catch (Exception)
                            {
                                var NewElement = new BsonElement(Name.Text, Data.Value);
                                MainData.RemoveElement(Data);
                                MainData.Add(new BsonElement(Name.Text, Data.Value));
                                Data = NewElement;
                            }
                        }
                        else
                        {
                            Name.Text = Data.Name;
                        }
                    }
                };

            }
        }
    }
}
