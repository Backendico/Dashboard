using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonEditor
{

    public partial class JsonEditor : UserControl
    {
        public JsonEditor(BsonDocument JsonData)
        {
            InitializeComponent();

            PlaceElement.Children.Add(new ElementObject(JsonData));

            BTNSave.MouseDown += (s, e) =>
            {
                Debug.WriteLine(JsonData);

            };

            BTNAddElement.MouseDown += (s, e) =>
            {
                PlaceElement.Children.Add(new ElementString(new BsonElement("New Name", "New Value"), JsonData));
            };

        }
    }

    public class Elements : StackPanel
    {

        internal new TextBox Name;
        internal TextBox Value;
        internal ComboBox Type;
        internal Border ShowMore;
        internal StackPanel PlaceSubElements;
        internal Border Add;

        public void Init(BsonElement Data)
        {
            Background = new SolidColorBrush(Colors.Transparent);

            Margin = new Thickness(0, 5, 0, 5);

            var Grid = new Grid();
            Children.Add(Grid);


            var PlaceSubElements = new StackPanel() { Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.WhiteSmoke), Margin = new Thickness(20, 0, 0, 0) };
            Children.Add(PlaceSubElements);

            Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { });


            //Delete
            var Delete = new Border() { CornerRadius = new CornerRadius(5), Margin = new Thickness(5, 0, 5, 0), Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.Tomato) };
            Delete.Child = new TextBlock() { Cursor = Cursors.Hand, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontSize = 15, Text = "\xF78A", FontFamily = new FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.White) };

            Grid.Children.Add(Delete);
            Grid.SetColumn(Delete, 0);
            Grid.SetRow(Delete, 0);
            Delete.MouseDown += (s, e) =>
            {
                Visibility = Visibility.Collapsed;
            };

            //add
            var Add = new Border() { Margin = new Thickness(5, 0, 5, 0), Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.LightGreen), CornerRadius = new CornerRadius(5) };
            Add.Child = new TextBlock() { Cursor = Cursors.Hand, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontSize = 15, Text = "\xE710", FontFamily = new FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.White) };
            Grid.Children.Add(Add);
            Grid.SetColumn(Add, 1);
            Grid.SetRow(Add, 0);

            //Show More
            var ShowMore = new Border() { Margin = new Thickness(5, 0, 5, 0), Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.LightGray), CornerRadius = new CornerRadius(5) }; ;
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
            Grid.SetColumn(ShowMore, 2);
            Grid.SetRow(ShowMore, 0);


            //Name
            var Name = new TextBox() { Margin = new Thickness(0, 0, 5, 0), BorderBrush = new SolidColorBrush(Colors.Transparent), MinWidth = 100, Text = Data.Name, Foreground = new SolidColorBrush(Colors.Black), TextAlignment = TextAlignment.Center };
            Grid.Children.Add(Name);
            Grid.SetColumn(Name, 3);
            Grid.SetRow(Name, 0);

            //Value
            var Value = new TextBox() { Margin = new Thickness(0, 0, 5, 0), BorderBrush = new SolidColorBrush(Colors.Transparent), MinWidth = 100, Text = Data.Value.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
            Grid.SetColumn(Value, 4);
            Grid.Children.Add(Value);
            Grid.SetRow(Value, 0);

            //type
            var Type = new ComboBox() { };
            Type.Items.Add(BsonType.Array);
            Type.Items.Add(BsonType.Binary);
            Type.Items.Add(BsonType.Boolean);
            Type.Items.Add(BsonType.DateTime);
            Type.Items.Add(BsonType.Decimal128);
            Type.Items.Add(BsonType.Document);
            Type.Items.Add(BsonType.Double);
            Type.Items.Add(BsonType.EndOfDocument);
            Type.Items.Add(BsonType.Int32);
            Type.Items.Add(BsonType.Int64);
            Type.Items.Add(BsonType.JavaScript);
            Type.Items.Add(BsonType.JavaScriptWithScope);
            Type.Items.Add(BsonType.MaxKey);
            Type.Items.Add(BsonType.MinKey);
            Type.Items.Add(BsonType.Null);
            Type.Items.Add(BsonType.ObjectId);
            Type.Items.Add(BsonType.RegularExpression);
            Type.Items.Add(BsonType.String);
            Type.Items.Add(BsonType.Symbol);
            Type.Items.Add(BsonType.Timestamp);
            Type.Items.Add(BsonType.Undefined);
            Grid.Children.Add(Type);
            Grid.SetColumn(Type, 5);
            Grid.SetRow(Type, 0);
            Type.SelectedItem = Data.Value.BsonType;


            //actions
            MouseEnter += (s, e) =>
            {
                
                    Delete.Visibility = Visibility.Visible;
                    Background = new SolidColorBrush(Colors.WhiteSmoke);

            };

            MouseLeave += (s, e) =>
            {

                    Delete.Visibility = Visibility.Collapsed;
                    if (!Show)
                    {
                        Background = new SolidColorBrush(Colors.Transparent);
                    }
            };

            this.Name = Name;
            this.Value = Value;
            this.Type = Type;
            this.ShowMore = ShowMore;
            this.PlaceSubElements = PlaceSubElements;
            this.Add = Add;
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
                        {
                            Children.Add(new TextBlock() { Text = "End" });
                        }
                        break;
                    case BsonType.Double:
                        {
                            Children.Add(new ElementDouble(item));
                        }
                        break;
                    case BsonType.String:
                        {
                            Children.Add(new ElementString(item, Data));
                        }
                        break;
                    case BsonType.Document:
                        {
                            Children.Add(new ElementsDocument(item));
                        }
                        break;
                    case BsonType.Array:
                        {
                            Children.Add(new ElementsArray(item));
                        }
                        break;
                    case BsonType.Binary:
                        {
                            Children.Add(new ElementsBinary(item));
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            Children.Add(new ElementUndifined(item));
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            Children.Add(new ElementsObjectid(item));
                        }
                        break;
                    case BsonType.Boolean:
                        {

                            Children.Add(new ElementsBoolean(item, Data));
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            Children.Add(new ElementsDateTime(item));
                        }
                        break;
                    case BsonType.Null:
                        {
                            Children.Add(new ElementNull(item));
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            Children.Add(new ElementRegexpretion(item));
                        }
                        break;
                    case BsonType.JavaScript:
                        {
                            Children.Add(new TextBlock() { Text = "JavaScript" });
                        }
                        break;
                    case BsonType.Symbol:
                        {
                            Children.Add(new ElementSymbol(item));
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            Children.Add(new ElementsCode(item));
                        }
                        break;
                    case BsonType.Int32:
                        {
                            Children.Add(new ElementsInt32(item, Data));
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            Children.Add(new ElementTimeStamp(item));
                        }
                        break;
                    case BsonType.Int64:
                        {
                            Children.Add(new ElementsInt64(item));
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            Children.Add(new ElementDecimal128(item));
                        }
                        break;
                    case BsonType.MinKey:
                        {
                            Children.Add(new ElementMinKey(item));
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            Children.Add(new ElementMaxKey(item));
                        }
                        break;
                }
            }
        }
    }

    public class ElementsArray : Elements
    {
        public ElementsArray(BsonElement Data)
        {
            Init(Data);

            Value.Text = "Array";
            Value.IsEnabled = false;
            ShowMore.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Visible;

            int Count = 0;
            foreach (var item in Data.Value.AsBsonArray)
            {
                switch (item.BsonType)
                {
                    case BsonType.EndOfDocument:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AEnd" });
                        break;
                    case BsonType.Double:
                        PlaceSubElements.Children.Add(new ElementDoubleArray(Count, item));
                        break;
                    case BsonType.String:
                        {
                            PlaceSubElements.Children.Add(new ElementStringArray(Count, item));
                        }
                        break;
                    case BsonType.Document:
                        {
                            PlaceSubElements.Children.Add(new ElementObjectArray(Count, item));
                        }
                        break;
                    case BsonType.Array:
                        {
                            PlaceSubElements.Children.Add(new ElementArrayArray(Count, item));
                        }
                        break;
                    case BsonType.Binary:
                        {

                            PlaceSubElements.Children.Add(new ElementBinaryArray(Count, item));
                        }
                        break;
                    case BsonType.Undefined:
                        PlaceSubElements.Children.Add(new ElementUndifineArray(Count, item));
                        break;
                    case BsonType.ObjectId:
                        PlaceSubElements.Children.Add(new ElementObjectIDArray(Count, item));
                        break;
                    case BsonType.Boolean:
                        PlaceSubElements.Children.Add(new ElementBooleanArray(Count, item));
                        break;
                    case BsonType.DateTime:
                        PlaceSubElements.Children.Add(new ElementDateTimeArray(Count, item));
                        break;
                    case BsonType.Null:
                        {

                            PlaceSubElements.Children.Add(new ElementNullArray(Count, item));
                        }
                        break;
                    case BsonType.RegularExpression:
                        {

                            PlaceSubElements.Children.Add(new ElementRegularExpresionArray(Count, item));
                        }
                        break;
                    case BsonType.JavaScript:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AJavaScript" });
                        break;
                    case BsonType.Symbol:
                        PlaceSubElements.Children.Add(new ElementSymbolArray(Count, item));
                        break;
                    case BsonType.JavaScriptWithScope:
                        PlaceSubElements.Children.Add(new ElementCodeArray(Count, item));
                        break;
                    case BsonType.Int32:
                        PlaceSubElements.Children.Add(new ElementInt32Array(Count, item));
                        break;
                    case BsonType.Timestamp:
                        PlaceSubElements.Children.Add(new ElementTimeStampArray(Count, item));
                        break;
                    case BsonType.Int64:
                        PlaceSubElements.Children.Add(new ElementInt64Array(Count, item));
                        break;
                    case BsonType.Decimal128:
                        PlaceSubElements.Children.Add(new ElementDecimal128Array(Count, item));
                        break;
                    case BsonType.MinKey:
                        PlaceSubElements.Children.Add(new ElementMinKeyArray(Count, item));
                        break;
                    case BsonType.MaxKey:
                        PlaceSubElements.Children.Add(new ElementMaxKeyArray(Count, item));
                        break;
                }
                Count++;
            }
        }

        public class FElementArray : StackPanel
        {

            public TextBlock Name;
            public TextBox Value;
            public ComboBox Type;
            public Border ShowMore;
            public StackPanel PlaceSubElements;
            public Border Add;
            public void Init(int Postion, BsonValue Data)
            {
                Background = new SolidColorBrush(Colors.Transparent);

                Margin = new Thickness(0, 5, 0, 5);

                var Grid = new Grid();
                Children.Add(Grid);


                var PlaceSubElements = new StackPanel() { Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.WhiteSmoke), Margin = new Thickness(20, 0, 0, 0) };
                Children.Add(PlaceSubElements);


                Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                Grid.ColumnDefinitions.Add(new ColumnDefinition() { });

                //Delete
                var Delete = new Border() { CornerRadius = new CornerRadius(5), Margin = new Thickness(5, 0, 5, 0), Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.Tomato) };
                Delete.Child = new TextBlock() { Cursor = Cursors.Hand, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontSize = 15, Text = "\xF78A", FontFamily = new FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.White) };

                Grid.Children.Add(Delete);
                Grid.SetColumn(Delete, 0);
                Grid.SetRow(Delete, 0);
                Delete.MouseDown += (s, e) =>
                {
                    Visibility = Visibility.Collapsed;
                };

                //add
                var Add = new Border() { Margin = new Thickness(5, 0, 5, 0), Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.LightGreen), CornerRadius = new CornerRadius(5) };
                Add.Child = new TextBlock() { Cursor = Cursors.Hand, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontSize = 15, Text = "\xE710", FontFamily = new FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.White) };
                Grid.Children.Add(Add);
                Grid.SetColumn(Add, 1);
                Grid.SetRow(Add, 0);

                //Show More
                var ShowMore = new Border() { Margin = new Thickness(5, 0, 5, 0), Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.LightGray), CornerRadius = new CornerRadius(5) }; ;
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
                Grid.SetColumn(ShowMore, 2);
                Grid.SetRow(ShowMore, 0);


                //Name
                var Name = new TextBlock() { Margin = new Thickness(0, 0, 5, 0), MinWidth = 100, Text = Postion.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
                Grid.Children.Add(Name);
                Grid.SetColumn(Name, 3);
                Grid.SetRow(Name, 0);

                //Value
                var Value = new TextBox() { Margin = new Thickness(0, 0, 5, 0), BorderBrush = new SolidColorBrush(Colors.Transparent), MinWidth = 100, Text = Data.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
                Grid.SetColumn(Value, 4);
                Grid.Children.Add(Value);
                Grid.SetRow(Value, 0);

                //type
                var Type = new ComboBox() { };
                Type.Items.Add(BsonType.Array);
                Type.Items.Add(BsonType.Binary);
                Type.Items.Add(BsonType.Boolean);
                Type.Items.Add(BsonType.DateTime);
                Type.Items.Add(BsonType.Decimal128);
                Type.Items.Add(BsonType.Document);
                Type.Items.Add(BsonType.Double);
                Type.Items.Add(BsonType.EndOfDocument);
                Type.Items.Add(BsonType.Int32);
                Type.Items.Add(BsonType.Int64);
                Type.Items.Add(BsonType.JavaScript);
                Type.Items.Add(BsonType.JavaScriptWithScope);
                Type.Items.Add(BsonType.MaxKey);
                Type.Items.Add(BsonType.MinKey);
                Type.Items.Add(BsonType.Null);
                Type.Items.Add(BsonType.ObjectId);
                Type.Items.Add(BsonType.RegularExpression);
                Type.Items.Add(BsonType.String);
                Type.Items.Add(BsonType.Symbol);
                Type.Items.Add(BsonType.Timestamp);
                Type.Items.Add(BsonType.Undefined);
                Grid.Children.Add(Type);
                Grid.SetColumn(Type, 5);
                Grid.SetRow(Type, 0);
                Type.SelectedItem = Data.BsonType;




                //actions
                MouseEnter += (s, e) =>
                {
                    Delete.Visibility = Visibility.Visible;
                    Background = new SolidColorBrush(Colors.WhiteSmoke);
                };

                MouseLeave += (s, e) =>
                {
                    Delete.Visibility = Visibility.Collapsed;
                    if (!Show)
                    {
                        Background = new SolidColorBrush(Colors.Transparent);
                    }
                };

                this.Name = Name;
                this.Value = Value;
                this.Type = Type;
                this.ShowMore = ShowMore;
                this.PlaceSubElements = PlaceSubElements;
                this.Add = Add;
            }

        }

        public class ElementArrayArray : FElementArray
        {
            public ElementArrayArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);

                Value.Text = "Array";
                Value.IsEnabled = false;

                ShowMore.Visibility = Visibility.Visible;

                var Count = 0;
                foreach (var item in Data.AsBsonArray)
                {
                    switch (item.BsonType)
                    {
                        case BsonType.EndOfDocument:
                            PlaceSubElements.Children.Add(new TextBlock() { Text = "AEnd" });
                            break;
                        case BsonType.Double:
                            PlaceSubElements.Children.Add(new ElementDoubleArray(Count, item));
                            break;
                        case BsonType.String:
                            {
                                PlaceSubElements.Children.Add(new ElementStringArray(Count, item));
                            }
                            break;
                        case BsonType.Document:
                            {
                                PlaceSubElements.Children.Add(new ElementObjectArray(Count, item));
                            }
                            break;
                        case BsonType.Array:
                            {
                                PlaceSubElements.Children.Add(new ElementArrayArray(Count, item));
                            }
                            break;
                        case BsonType.Binary:
                            {

                                PlaceSubElements.Children.Add(new ElementBinaryArray(Count, item));
                            }
                            break;
                        case BsonType.Undefined:
                            PlaceSubElements.Children.Add(new ElementUndifineArray(Count, item));
                            break;
                        case BsonType.ObjectId:
                            PlaceSubElements.Children.Add(new ElementObjectIDArray(Count, item));
                            break;
                        case BsonType.Boolean:
                            PlaceSubElements.Children.Add(new ElementBooleanArray(Count, item));
                            break;
                        case BsonType.DateTime:
                            PlaceSubElements.Children.Add(new ElementDateTimeArray(Count, item));
                            break;
                        case BsonType.Null:
                            {

                                PlaceSubElements.Children.Add(new ElementNullArray(Count, item));
                            }
                            break;
                        case BsonType.RegularExpression:
                            {

                                PlaceSubElements.Children.Add(new ElementRegularExpresionArray(Count, item));
                            }
                            break;
                        case BsonType.JavaScript:
                            PlaceSubElements.Children.Add(new TextBlock() { Text = "AJavaScript" });
                            break;
                        case BsonType.Symbol:
                            PlaceSubElements.Children.Add(new ElementSymbolArray(Count, item));
                            break;
                        case BsonType.JavaScriptWithScope:
                            PlaceSubElements.Children.Add(new ElementCodeArray(Count, item));
                            break;
                        case BsonType.Int32:
                            PlaceSubElements.Children.Add(new ElementInt32Array(Count, item));
                            break;
                        case BsonType.Timestamp:
                            PlaceSubElements.Children.Add(new ElementTimeStampArray(Count, item));
                            break;
                        case BsonType.Int64:
                            PlaceSubElements.Children.Add(new ElementInt64Array(Count, item));
                            break;
                        case BsonType.Decimal128:
                            PlaceSubElements.Children.Add(new ElementDecimal128Array(Count, item));
                            break;
                        case BsonType.MinKey:
                            PlaceSubElements.Children.Add(new ElementMinKeyArray(Count, item));
                            break;
                        case BsonType.MaxKey:
                            PlaceSubElements.Children.Add(new ElementMaxKeyArray(Count, item));
                            break;
                    }
                    Count++;
                }
            }

        }

        public class ElementBinaryArray : FElementArray
        {
            public ElementBinaryArray(int Postion, BsonValue Data)
            {

                Init(Postion, Data);
            }

        }

        public class ElementBooleanArray : FElementArray
        {
            public ElementBooleanArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementCodeArray : FElementArray
        {
            public ElementCodeArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementDateTimeArray : FElementArray
        {
            public ElementDateTimeArray(int Postion, BsonValue Data)
            {

                Init(Postion, Data);
                Value.IsEnabled = false;
            }
        }

        public class ElementDecimal128Array : FElementArray
        {
            public ElementDecimal128Array(int Postion, BsonValue Data)
            {
                Init(Postion, Data);

            }
        }

        public class ElementDoubleArray : FElementArray
        {
            public ElementDoubleArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementInt32Array : FElementArray
        {
            public ElementInt32Array(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementInt64Array : FElementArray
        {
            public ElementInt64Array(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementMinKeyArray : FElementArray
        {
            public ElementMinKeyArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementMaxKeyArray : FElementArray
        {
            public ElementMaxKeyArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementNullArray : FElementArray
        {
            public ElementNullArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
                Value.Text = "Null";
                Value.IsEnabled = false;
            }
        }

        public class ElementObjectArray : FElementArray
        {
            public ElementObjectArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
                Value.Text = "Object";
                Value.IsEnabled = false;
                ShowMore.Visibility = Visibility.Visible;

                foreach (var item in Data.AsBsonDocument)
                {
                    switch (item.Value.BsonType)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                PlaceSubElements.Children.Add(new ElementDouble(item));
                            }
                            break;
                        case BsonType.String:
                            {
                                PlaceSubElements.Children.Add(new ElementString(item, Data.AsBsonDocument));
                            }
                            break;
                        case BsonType.Document:
                            {
                                PlaceSubElements.Children.Add(new ElementsDocument(item));
                            }
                            break;
                        case BsonType.Array:
                            {
                                PlaceSubElements.Children.Add(new ElementsArray(item));
                            }
                            break;
                        case BsonType.Binary:
                            {
                                PlaceSubElements.Children.Add(new ElementsBinary(item));
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                PlaceSubElements.Children.Add(new ElementUndifined(item));
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                PlaceSubElements.Children.Add(new ElementsObjectid(item));
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                PlaceSubElements.Children.Add(new ElementsBoolean(item, Data.AsBsonDocument));
                            }
                            break;
                        case BsonType.DateTime:
                            {
                                PlaceSubElements.Children.Add(new ElementsDateTime(item));
                            }
                            break;
                        case BsonType.Null:
                            {
                                PlaceSubElements.Children.Add(new ElementNull(item));
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                PlaceSubElements.Children.Add(new ElementRegexpretion(item));
                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                PlaceSubElements.Children.Add(new ElementSymbol(item));
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                PlaceSubElements.Children.Add(new ElementsCode(item));
                            }
                            break;
                        case BsonType.Int32:
                            {
                                PlaceSubElements.Children.Add(new ElementsInt32(item, Data.AsBsonDocument));
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                PlaceSubElements.Children.Add(new ElementTimeStamp(item));
                            }
                            break;
                        case BsonType.Int64:
                            {
                                PlaceSubElements.Children.Add(new ElementsInt64(item));
                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                PlaceSubElements.Children.Add(new ElementDecimal128(item));
                            }
                            break;
                        case BsonType.MinKey:
                            {
                                PlaceSubElements.Children.Add(new ElementMinKey(item));
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                PlaceSubElements.Children.Add(new ElementMaxKey(item));
                            }
                            break;

                    }
                }
            }
        }

        public class ElementObjectIDArray : FElementArray
        {
            public ElementObjectIDArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
                Value.IsEnabled = false;
            }
        }

        public class ElementRegularExpresionArray : FElementArray
        {
            public ElementRegularExpresionArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }
        }

        public class ElementStringArray : FElementArray
        {
            public ElementStringArray(int Postion, BsonValue Data)
            {

                Init(Postion, Data);


            }

        }

        public class ElementSymbolArray : FElementArray
        {
            public ElementSymbolArray(int Postion, BsonValue Data)
            {

                Init(Postion, Data);
            }

        }

        public class ElementTimeStampArray : FElementArray
        {
            public ElementTimeStampArray(int Postion, BsonValue Data)
            {
                Init(Postion, Data);
            }

        }

        public class ElementUndifineArray : FElementArray
        {
            public ElementUndifineArray(int Postion, BsonValue Data)
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
            Init(Data);
            Value.TextChanged += (s, e) =>
            {
                Data = new BsonElement(Data.Name, Value.Text);
                MainData.SetElement(Data);
            };
        }
    }

    public class ElementsBoolean : Elements
    {
        public ElementsBoolean(BsonElement Data, BsonDocument MainData)
        {
            Init(Data);

            Loaded += (s, e) =>
            {

                Value.TextChanged += (ss, ee) =>
                {
                    try
                    {
                        Data = new BsonElement(Data.Name, bool.Parse(Value.Text));

                        MainData.SetElement(Data);
                        Background = new SolidColorBrush(Colors.Transparent);

                    }
                    catch (Exception)
                    {
                        Background = new SolidColorBrush(Colors.Tomato);
                        Debug.WriteLine("Err" + Data);
                    }

                };

            };

        }
    }

    public class ElementsObjectid : Elements
    {
        public ElementsObjectid(BsonElement Data)
        {
            Init(Data);
            Value.IsEnabled = false;
        }


    }


    public class ElementsDateTime : Elements
    {
        public ElementsDateTime(BsonElement Data)
        {
            Init(Data);
            Value.IsEnabled = false;
        }
    }

    public class ElementsDocument : Elements
    {
        public ElementsDocument(BsonElement Data)
        {
            Init(Data);
            Value.Text = "Object";
            Value.IsEnabled = false;
            ShowMore.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Visible;

            foreach (var item in Data.Value.AsBsonDocument)
            {
                switch (item.Value.BsonType)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            PlaceSubElements.Children.Add(new ElementDouble(item));
                        }
                        break;
                    case BsonType.String:
                        {
                            PlaceSubElements.Children.Add(new ElementString(item, Data.Value.AsBsonDocument));
                        }
                        break;
                    case BsonType.Document:
                        {
                            PlaceSubElements.Children.Add(new ElementsDocument(item));
                        }
                        break;
                    case BsonType.Array:
                        {
                            PlaceSubElements.Children.Add(new ElementsArray(item));
                        }
                        break;
                    case BsonType.Binary:
                        {
                            PlaceSubElements.Children.Add(new ElementsBinary(item));
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            PlaceSubElements.Children.Add(new ElementUndifined(item));
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            PlaceSubElements.Children.Add(new ElementsObjectid(item));
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            PlaceSubElements.Children.Add(new ElementsBoolean(item, Data.Value.AsBsonDocument));
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            PlaceSubElements.Children.Add(new ElementsDateTime(item));
                        }
                        break;
                    case BsonType.Null:
                        {
                            PlaceSubElements.Children.Add(new ElementNull(item));
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            PlaceSubElements.Children.Add(new ElementRegexpretion(item));
                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            PlaceSubElements.Children.Add(new ElementSymbol(item));
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            PlaceSubElements.Children.Add(new ElementsCode(item));
                        }
                        break;
                    case BsonType.Int32:
                        {
                            PlaceSubElements.Children.Add(new ElementsInt32(item, Data.Value.AsBsonDocument));
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            PlaceSubElements.Children.Add(new ElementTimeStamp(item));
                        }
                        break;
                    case BsonType.Int64:
                        {
                            PlaceSubElements.Children.Add(new ElementsInt64(item));
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            PlaceSubElements.Children.Add(new ElementDecimal128(item));
                        }
                        break;
                    case BsonType.MinKey:
                        {
                            PlaceSubElements.Children.Add(new ElementMinKey(item));
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            PlaceSubElements.Children.Add(new ElementMaxKey(item));
                        }
                        break;

                }
            }


            //acation add 
            Add.MouseDown += (s, e) =>
            {
                PlaceSubElements.Children.Add(new ElementString(new BsonElement("Name", "Value"), Data.Value.AsBsonDocument));
            };

        }
    }

    public class ElementsInt32 : Elements
    {
        public ElementsInt32(BsonElement Data, BsonDocument MainData)
        {
            Init(Data);

            Loaded += (s, e) =>
            {

                Value.TextChanged += (ss, ee) =>
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
                        Value.Text = Data.Value.ToString() ;
                    }

                };

            };

        }
    }

    public class ElementsInt64 : Elements
    {
        public ElementsInt64(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementsBinary : Elements
    {
        public ElementsBinary(BsonElement Data)
        {
            Init(Data);

        }
    }

    public class ElementsCode : Elements
    {
        public ElementsCode(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementDecimal128 : Elements
    {
        public ElementDecimal128(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementDouble : Elements
    {
        public ElementDouble(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementMaxKey : Elements
    {
        public ElementMaxKey(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementMinKey : Elements
    {
        public ElementMinKey(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementNull : Elements
    {
        public ElementNull(BsonElement Data)
        {
            Init(Data);
            Value.IsEnabled = false;
            Value.Text = "Null";

        }
    }

    public class ElementRegexpretion : Elements
    {
        public ElementRegexpretion(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementSymbol : Elements
    {
        public ElementSymbol(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementTimeStamp : Elements
    {
        public ElementTimeStamp(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementUndifined : Elements
    {
        public ElementUndifined(BsonElement Data)
        {
            Init(Data);
            Value.IsEnabled = false;
            Value.Text = "Undifined";
        }
    }
}
