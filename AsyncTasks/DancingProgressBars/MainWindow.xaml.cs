using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace DancingProgressBars
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            progressDP.Children.Clear();

            var count = int.Parse(countTB.Text);

            for (var i = 0; i < count; i++)
            {
                var progressBar = new ProgressBar
                {
                    Width = 350,
                    Height = 40,
                    Margin = new Thickness(10),
                    Orientation = Orientation.Horizontal
                };
                progressDP.Children.Add(progressBar);

                StartProcessAsync(progressBar);
            }
        }
        Random random = new Random();
        private async void StartProcessAsync(object progressBar)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    var value = random.Next(0, 100);
                    var color = Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        (progressBar as ProgressBar).Value = value;
                        (progressBar as ProgressBar).Foreground = new SolidColorBrush(color);
                    }));
                    Thread.Sleep(random.Next(200));
                }
            });          
        }
    }
}
