using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace HorseRacing
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
            for (int i = 0; i < main.Children.Count; i++)
            {
                if(main.Children[i] is ProgressBar)
                {
                    var thread = new Thread(ProcessStart);
                    thread.Start(main.Children[i]);
                }
            }
        }
        Random random = new Random();
        private void ProcessStart(object progressBar)
        {
            var stopWatch = new Stopwatch();
            
            var bar = (progressBar as ProgressBar);
            stopWatch.Start();

            while(this.Dispatcher.Invoke(new Func<double>(() => bar.Value)) < 100)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    bar.Value += random.NextDouble();
                }));
                Thread.Sleep(50);
            }

            stopWatch.Stop();
            this.Dispatcher.Invoke(new Action(() =>
            {
                resultsTB.Text += bar.Name + "\ttime: " + stopWatch.Elapsed.TotalSeconds + Environment.NewLine;
            }
            ));
            
        }
    }
}
