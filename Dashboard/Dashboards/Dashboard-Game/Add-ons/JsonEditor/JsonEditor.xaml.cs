using MongoDB.Bson;
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

            BTNAddElement.MouseDown += (s, e) =>
            {
                PlaceElement.Children.Add(new ElementString(new BsonElement("New Name", "New Value")));
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
            var Delete = new Border() { CornerRadius = new CornerRadius(5),Margin=new Thickness(5,0,5,0), Visibility = Visibility.Collapsed, Background =new SolidColorBrush(Colors.Tomato) };
                Delete.Child=  new TextBlock() { Cursor = Cursors.Hand, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontSize = 15, Text = "\xF78A", FontFamily = new FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.White) };
           
            Grid.Children.Add(Delete);
            Grid.SetColumn(Delete, 0);
            Grid.SetRow(Delete, 0);
            Delete.MouseDown += (s, e) =>
            {
                Visibility = Visibility.Collapsed;
            };

            //add
            var Add = new Border() {Margin=new Thickness(5,0,5,0), Visibility=Visibility.Collapsed, Background=new SolidColorBrush(Colors.LightGreen), CornerRadius=new CornerRadius(5) };
                Add.Child=new TextBlock() { Cursor = Cursors.Hand,  VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontSize = 15, Text = "\xE710", FontFamily = new FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.White) };
            Grid.Children.Add(Add);
            Grid.SetColumn(Add, 1);
            Grid.SetRow(Add, 0);

            //Show More
            var ShowMore=new Border() { Margin = new Thickness(5, 0, 5, 0), Visibility = Visibility.Collapsed, Background = new SolidColorBrush(Colors.LightGray), CornerRadius = new CornerRadius(5) }; ;
           ShowMore.Child = new TextBlock() { Cursor = Cursors.Hand, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 15, Text = "\xE974", FontFamily = new FontFamily("Segoe MDL2 Assets") };
            var Show = false;
            ShowMore.MouseDown += (s, e) =>
            {
                if (!Show)
                {
                    Show = true;
                    (ShowMore.Child as TextBlock) .Text = "\xE972";
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
            var Name = new TextBox() { Margin = new Thickness(0, 0, 5, 0), BorderBrush = new SolidColorBrush(Colors.Transparent), MinWidth = 100, Text = Data.Name, Foreground = new SolidColorBrush(Colors.Black) };
            Grid.Children.Add(Name);
            Grid.SetColumn(Name, 3);
            Grid.SetRow(Name, 0);

            //Value
            var Value = new TextBox() { Margin = new Thickness(0, 0, 5, 0), BorderBrush = new SolidColorBrush(Colors.Transparent), MinWidth = 100, Text = Data.Value.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
            Grid.SetColumn(Value, 4);
            Grid.Children.Add(Value);
            Grid.SetRow(Value, 0);

            //type
            var Type = new ComboBox();
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
                            Children.Add(new ElementString(item));
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

                            Children.Add(new ElementsBoolean(item));
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
                            Children.Add(new ElementsInt32(item));
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

            for (int i = 0; i < Data.Value.AsBsonArray.Count; i++)
            {
                switch (Data.Value.AsBsonArray[i].BsonType)
                {
                    case BsonType.EndOfDocument:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AEnd" });
                        break;
                    case BsonType.Double:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ADouble" });
                        break;
                    case BsonType.String:
                        {
                            PlaceSubElements.Children.Add(new TextBlock() { Text = "AString" });
                        }
                        break;
                    case BsonType.Document:
                        {
                            PlaceSubElements.Children.Add(new TextBlock() { Text = "ADoc" });
                        }
                        break;
                    case BsonType.Array:
                        {
                            PlaceSubElements.Children.Add(new TextBlock() { Text = "AArray" });
                        }
                        break;
                    case BsonType.Binary:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ABinary" });
                        break;
                    case BsonType.Undefined:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AUndefined" });
                        break;
                    case BsonType.ObjectId:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AObjectID" });
                        break;
                    case BsonType.Boolean:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ABoolean" });
                        break;
                    case BsonType.DateTime:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ATime" });
                        break;
                    case BsonType.Null:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ANull" });
                        break;
                    case BsonType.RegularExpression:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ARegularExpression" });
                        break;
                    case BsonType.JavaScript:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AJavaScript" });
                        break;
                    case BsonType.Symbol:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ASymbol" });
                        break;
                    case BsonType.JavaScriptWithScope:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AJavaScriptWithScope" });
                        break;
                    case BsonType.Int32:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AInt32" });
                        break;
                    case BsonType.Timestamp:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ATimestamp" });
                        break;
                    case BsonType.Int64:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AInt64" });
                        break;
                    case BsonType.Decimal128:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "ADecimal" });
                        break;
                    case BsonType.MinKey:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AMinKey" });
                        break;
                    case BsonType.MaxKey:
                        PlaceSubElements.Children.Add(new TextBlock() { Text = "AMaxKey" });
                        break;
                }
            }
        }


    }

    public class ElementString : Elements
    {
        public ElementString(BsonElement Data)
        {
            Init(Data);

        }
    }

    public class ElementsBoolean : Elements
    {
        public ElementsBoolean(BsonElement Data)
        {
            Init(Data);
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
                            PlaceSubElements.Children.Add(new ElementString(item));
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
                            PlaceSubElements.Children.Add(new ElementsBoolean(item));
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
                            PlaceSubElements.Children.Add(new ElementsInt32(item));
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

    public class ElementsInt32 : Elements
    {
        public ElementsInt32(BsonElement Data)
        {
            Init(Data);
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

    public class ElementTimeStamp:Elements
    {
        public ElementTimeStamp(BsonElement Data)
        {
            Init(Data);
        }
    }

public class ElementUndifined:Elements
    {
        public ElementUndifined(BsonElement Data)
        {
            Init(Data);
            Value.IsEnabled = false;
            Value.Text = "Undifined";
        }
    }
}
