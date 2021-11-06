using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace FindWords
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

        private void ChooseDirectory_Click(object sender, RoutedEventArgs e)
        {
            using(var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                directoryTB.Text = dialog.SelectedPath;
            }
        }

        private void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(wordTB.Text))
            {
                return;
            }
            wordLB.Items.Clear();

            CheckCurrentDirectory(directoryTB.Text, wordTB.Text);
            StartProcessAsync(directoryTB.Text, wordTB.Text);
        }
        private async void CheckCurrentDirectory(string path,string word)
        {
            await Task.Run(async () =>
            {
                try
                {
                    foreach (var file in Directory.GetFiles(path))
                    {
                        using (var read = new StreamReader(file))
                        {
                            var wordsCount = (await read.ReadToEndAsync()).Split(' ').Count(w => w == word);

                            if(wordsCount > 0)
                            {
                                this.Dispatcher.Invoke(new Action(() =>
                                {
                                    wordLB.Items.Add(new ListBoxItem
                                    {
                                        Content = "File name: " + Path.GetFileName(file) + "  Word matches: " + wordsCount + "  File path: " + file + Environment.NewLine
                                    });
                                }));
                            }                           
                        }
                    }
                }
                catch
                {

                }
            });
        }
        private async void StartProcessAsync(string path, string word)
        {
            await Task.Run(async () =>
            {
                try
                {                                     
                    foreach(string dir in Directory.GetDirectories(path))
                    {
                        foreach(string file in Directory.GetFiles(dir))
                        {
                            using(var read = new StreamReader(file))
                            {
                                var wordsCount = (await read.ReadToEndAsync()).Split(' ').Count(w => w == word);

                                if (wordsCount > 0)
                                {
                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        wordLB.Items.Add(new ListBoxItem
                                        {
                                            Content = "File name: " + Path.GetFileName(file) + "  Word matches: " + wordsCount + "  File path: " + file + Environment.NewLine
                                        });
                                    }));
                                }
                            }
                        }

                        StartProcessAsync(dir, word);
                    }
                }
                catch
                {
                    
                }
            });
        }
    }
}
