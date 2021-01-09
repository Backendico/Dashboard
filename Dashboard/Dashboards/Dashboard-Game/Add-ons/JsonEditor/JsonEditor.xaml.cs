using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonEditor
{

    public partial class JsonEditor : UserControl
    {
        public JsonEditor(BsonDocument JsonData)
        {
            InitializeComponent();

            PlaceElement.Children.Add(new ElementObject(JsonData));

            BTNSave.Click += (s, e) =>
            {
                Debug.WriteLine(JsonData);

            };

            BTNAddElement.MouseDown += (s, e) =>
            {
                var NewElement = new BsonElement(ObjectId.GenerateNewId().ToString(), "New Value");

                JsonData.Add(NewElement);
                PlaceElement.Children.Add(new ElementString(NewElement, JsonData));
            };
        }

        public JsonEditor(BsonDocument JsonData,Action Save)
        {
            InitializeComponent();

            PlaceElement.Children.Add(new ElementObject(JsonData));

            BTNSave.Click += (s, e) =>
            {
                Save();
            };

            BTNAddElement.MouseDown += (s, e) =>
            {
                var NewElement = new BsonElement(ObjectId.GenerateNewId().ToString(), "New Value");

                JsonData.Add(NewElement);
                PlaceElement.Children.Add(new ElementString(NewElement, JsonData));
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
        internal Border Delete;

        public void Init(BsonElement Data, BsonDocument MainData)
        {
            Background = new SolidColorBrush(Colors.Transparent);

            Margin = new Thickness(0, 5, 0, 5);

            var Grid = new Grid();
            Children.Add(Grid);


            var PlaceSubElements = new StackPanel() { Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.WhiteSmoke), Margin = new Thickness(60, 0, 0, 0) };
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
                MainData.RemoveElement(Data);
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
            this.Delete = Delete;
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
            Add.Visibility = Visibility.Visible;


            InitInternalArray();

            Add.MouseDown += (s, e) =>
            {
                Data.Value.AsBsonArray.Add(" ");
                InitInternalArray();
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
            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
                }
            };
        }


        public class FElementArray : StackPanel
        {

            public TextBlock Name;
            public TextBox Value;
            public ComboBox Type;
            public Border ShowMore;
            public StackPanel PlaceSubElements;
            public Border Add;
            public Border Delete;

            public void Init(int Postion, BsonValue Data)
            {
                Background = new SolidColorBrush(Colors.Transparent);

                Margin = new Thickness(0, 5, 0, 5);

                var Grid = new Grid();
                Children.Add(Grid);


                var PlaceSubElements = new StackPanel() { Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.WhiteSmoke), Margin = new Thickness(60, 0, 0, 0) };
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
                var Name = new TextBlock() { Margin = new Thickness(0, 0, 5, 0), TextAlignment = TextAlignment.Center, MinWidth = 100, Text = Postion.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
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
                this.Delete = Delete;
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
                Add.Visibility = Visibility.Visible;

                InitInternalArray();

                //action add 
                Add.MouseDown += (s, e) =>
                {
                    Data.AsBsonArray.Add(" ");
                    InitInternalArray();
                };

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };

                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };



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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };

                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
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
                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };

                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
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
                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };

                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };
            }
        }

        public class ElementMaxKeyArray : FElementArray
        {
            public ElementMaxKeyArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
            {
                Init(Postion, Data);
                Value.IsEnabled = false;
                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };

            }
        }

        public class ElementNullArray : FElementArray
        {
            public ElementNullArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
            {
                Init(Postion, Data);
                Value.Text = "Null";
                Value.IsEnabled = false;

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };

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
                Add.Visibility = Visibility.Visible;


                InitInternalObjects();

                Add.MouseDown += (s, e) =>
                {
                    Data.AsBsonDocument.Add(ObjectId.GenerateNewId().ToString(), " ");

                    InitInternalObjects();
                };

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };

                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };
            }
        }

        public class ElementRegularExpresionArray : FElementArray
        {
            public ElementRegularExpresionArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
            {
                Init(Postion, Data);
                Value.IsEnabled = false;
                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };

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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
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

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };
            }

        }

        public class ElementUndifineArray : FElementArray
        {
            public ElementUndifineArray(int Postion, BsonValue Data, BsonArray MainArray, Action UpdateMainArray)
            {
                Init(Postion, Data);
                Value.Text = "Undifine";
                Value.IsEnabled = false;

                Delete.MouseDown += (s, e) =>
                {
                    MainArray.RemoveAt(Postion);
                    UpdateMainArray();
                };
                Type.SelectionChanged += (s, e) =>
                {
                    var MainParent = Parent as StackPanel;

                    switch ((BsonType)Type.SelectedItem)
                    {
                        case BsonType.EndOfDocument:
                            break;
                        case BsonType.Double:
                            {
                                MainArray[Postion] = new BsonDouble(0.0);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDoubleArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.String:
                            {
                                MainArray[Postion] = "";
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementStringArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Document:
                            {
                                MainArray[Postion] = new BsonDocument();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Array:
                            {
                                MainArray[Postion] = new BsonArray();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementArrayArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Binary:
                            {
                                MainArray[Postion] = new BsonBinaryData(new byte[] { 0, 0, 0, 0 });
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBinaryArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Undefined:
                            {
                                MainArray[Postion] = BsonUndefined.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifineArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.ObjectId:
                            {
                                MainArray[Postion] = ObjectId.GenerateNewId();
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementObjectIDArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Boolean:
                            {
                                MainArray[Postion] = BsonBoolean.True;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementBooleanArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.DateTime:
                            {
                                MainArray[Postion] = new BsonDateTime(SettingUser.ServerTime);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDateTimeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Null:
                            {
                                MainArray[Postion] = BsonNull.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNullArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.RegularExpression:
                            {
                                MainArray[Postion] = BsonRegularExpression.Create("");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegularExpresionArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.JavaScript:
                            break;
                        case BsonType.Symbol:
                            {
                                MainArray[Postion] = BsonSymbol.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbolArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.JavaScriptWithScope:
                            {
                                MainArray[Postion] = BsonJavaScriptWithScope.Create("A");
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementCodeArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int32:
                            {
                                MainArray[Postion] = new BsonInt32(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt32Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Timestamp:
                            {
                                MainArray[Postion] = new BsonTimestamp(1);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStampArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.Int64:
                            {
                                MainArray[Postion] = new BsonInt64(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementInt64Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.Decimal128:
                            {
                                MainArray[Postion] = new BsonDecimal128(33);
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128Array(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        case BsonType.MinKey:
                            {
                                MainArray[Postion] = BsonMinKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);
                            }
                            break;
                        case BsonType.MaxKey:
                            {
                                MainArray[Postion] = BsonMaxKey.Value;
                                MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKeyArray(Postion, MainArray[Postion], MainArray, UpdateMainArray));
                                MainParent.Children.Remove(this);

                            }
                            break;
                        default:
                            break;
                    }
                };
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

            Delete.MouseDown += (s, e) =>
            {
                MainData.RemoveElement(Data);
            };

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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


            Delete.MouseDown += (s, e) =>
            {
                MainData.RemoveElement(Data);
            };

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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


            Delete.MouseDown += (s, e) =>
            {
                MainData.RemoveElement(Data);
            };

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Delete.MouseDown += (s, e) =>
            {
                MainData.RemoveElement(Data);
            };

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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
            Add.Visibility = Visibility.Visible;

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


            //acation add 
            Add.MouseDown += (s, e) =>
            {
                PlaceSubElements.Children.Clear();
                MainData.Add(ObjectId.GenerateNewId().ToString(), " ");

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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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
            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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
            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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
            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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
            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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

            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
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
            Type.SelectionChanged += (s, e) =>
            {

                var MainParent = Parent as StackPanel;

                switch ((BsonType)Type.SelectedItem)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDouble(0.0));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDouble(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.String:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), "");
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementString(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Document:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDocument());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDocument(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Array:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonArray());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsArray(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Binary:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonBinaryData(new byte[] { 0, 0, 0, 0 }));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBinary(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Undefined:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonUndefined.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementUndifined(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.ObjectId:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), ObjectId.GenerateNewId());
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsObjectid(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Boolean:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonBoolean.True);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsBoolean(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.DateTime:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDateTime(SettingUser.ServerTime));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsDateTime(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Null:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonNull.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementNull(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.RegularExpression:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonRegularExpression.Create(""));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementRegexpretion(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);

                        }
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonSymbol.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementSymbol(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.JavaScriptWithScope:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonJavaScriptWithScope.Create("A"));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsCode(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int32:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt32(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt32(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Timestamp:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonTimestamp(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementTimeStamp(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Int64:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonInt64(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementsInt64(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.Decimal128:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), new BsonDecimal128(33));
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementDecimal128(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MinKey:
                        {

                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMinKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMinKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    case BsonType.MaxKey:
                        {
                            MainData.Set(MainData.IndexOfName(Data.Name), BsonMaxKey.Value);
                            MainParent.Children.Insert(MainParent.Children.IndexOf(this), new ElementMaxKey(MainData.GetElement(Data.Name), MainData));
                            MainParent.Children.Remove(this);
                        }
                        break;
                    default:
                        break;
                }
            };
        }
    }
}
