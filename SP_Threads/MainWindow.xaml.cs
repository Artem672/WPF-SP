using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace SP_Threads
{
    public partial class MainWindow : Window
    {
        public static List<Thread> threads { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            threads = new List<Thread>
            {
                new Thread((limit) =>
                {
                    var result = 1;

                    for (var i = 2; i <= (int)limit; i++)
                    {
                        result *= i;
                    }

                    factorialTB.Dispatcher.Invoke(new Action(() => factorialTB.Text = result.ToString()));
                })
                ,
                new Thread((limit) =>
                {
                    var number = 0;
                    var step = 1;

                    for (var i = 0; i < (int)limit; i++)
                    {
                        var temp = number;
                        number = step;
                        step = temp + number;
                    }

                    fibonacchiTB.Dispatcher.Invoke(new Action(() => fibonacchiTB.Text = number.ToString()));
                })
                ,
                new Thread((limit) =>
                {
                    var number = 0;

                    for (var i = 2; i <= (int)limit; i++)
                    {
                        var isSimple = true;
                        for (var j = 2; j < (i / 2); j++)
                        {
                            if(i % j == 0)
                            {
                                isSimple = false;
                            }
                        }
                        number = isSimple ? i : number;
                    }

                    simpleTB.Dispatcher.Invoke(new Action(() => simpleTB.Text = number.ToString()));
                })
            };

            var random = new Random();

            foreach (var item in threads)
            {
                item.Start(random.Next(20));
            }
        }
    }
}
