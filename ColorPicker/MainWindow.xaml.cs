using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ColorP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public SolidColorBrush Brush { get; set; }
        public ObservableCollection<colorItem> Items { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Items = new ObservableCollection<colorItem>();
            colorItemsList.ItemsSource = Items;
            DataContext = this;
        }

        private int alpha;
        public int Alpha
        {
            get { return alpha; }
            set
            {
                alpha = value;
                Brush = new SolidColorBrush(Color.FromArgb(Convert.ToByte(alpha), Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(blue)));
                OnNotify("Brush");
            }
        }

        private int red;
        public int Red
        {
            get { return blue; }
            set
            {
                blue = value;
                Brush = new SolidColorBrush(Color.FromArgb(Convert.ToByte(alpha), Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(blue)));
                OnNotify("Brush");
            }
        }

        private int green;
        public int Green
        {
            get { return green; }
            set
            {
                green = value;
                Brush = new SolidColorBrush(Color.FromArgb(Convert.ToByte(alpha), Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(blue)));
                OnNotify("Brush");
            }
        }

        private int blue;
        public int Blue
        {
            get { return blue; }
            set
            {
                blue = value;
                Brush = new SolidColorBrush(Color.FromArgb(Convert.ToByte(alpha), Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(blue)));
                OnNotify("Brush");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNotify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = new colorItem()
            {
                ItemColor = Brush,
                HexCode = "#" + Brush.Color.A.ToString("X2") + Brush.Color.R.ToString("X2") + Brush.Color.G.ToString("X2") + Brush.Color.B.ToString("X2")
            };

            Items.Add(item);
        }

        private void deleteButton(object sender, RoutedEventArgs e)
        {
            var itemButton = (Button)sender;
            if (itemButton.DataContext is colorItem)
            {
                Items.Remove((colorItem)itemButton.DataContext);
            }
        }

        private void alphaCB_Checked(object sender, RoutedEventArgs e)
        {
            if (alphaSlider == null)
                return;
            switch((sender as CheckBox).Name)
            {
                case "alphaCB":
                    {
                        alphaSlider.IsEnabled = !alphaSlider.IsEnabled;
                        break;
                    }
                case "redCB":
                    {
                        redSlider.IsEnabled = !redSlider.IsEnabled;
                        break;
                    }
                case "greenCB":
                    {
                        greenSlider.IsEnabled = !greenSlider.IsEnabled;
                        break;
                    }
                case "blueCB":
                    {
                        blueSlider.IsEnabled = !blueSlider.IsEnabled;
                        break;
                    }
            }
           
        }
    }
}
