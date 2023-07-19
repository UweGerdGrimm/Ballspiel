using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Beispiel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _animationsTimer = new DispatcherTimer();
        private bool gehtNachRechts = true;
        private bool gehtNachUnten = true;
         

        private int zaehler = 0;
        
        public MainWindow()
        {
            InitializeComponent();

            _animationsTimer.Interval = TimeSpan.FromMilliseconds(50);
            _animationsTimer.Tick += PositioniereBall;
        }

        private void PositioniereBall(object? sender, EventArgs e)
        {
            var x = Canvas.GetLeft(Ball);
            var y = Canvas.GetTop(Ball);

            if(gehtNachRechts)
            {
                Canvas.SetLeft(Ball, x + 5);
            }
            else
            {
                Canvas.SetLeft(Ball, x - 5);
            }

            if(x >= Spielplatz.ActualWidth - Ball.ActualWidth) 
            {
                gehtNachRechts = false;
            }
            else if(x <= 0)
            {
                gehtNachRechts = true;
            }

            if (gehtNachUnten)
            {
                Canvas.SetTop(Ball, y + 5);
            }
            else
            {
                Canvas.SetTop(Ball, y - 5);
            }

            if (y >= Spielplatz.ActualHeight - Ball.ActualHeight)
            {
                gehtNachUnten = false;
            }
            else if (y <= 0)
            {
                gehtNachUnten = true;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_animationsTimer.IsEnabled) 
            {
                _animationsTimer.Stop();
            }
            else
            {
                _animationsTimer.Start();
                zaehler = 0;
                SpielStandLabel.Content = $"{zaehler} Clicks";
            }
            
            /*var mittex = Spielplatz.ActualWidth / 2 - Ball.Height;
            var mittey = Spielplatz.ActualHeight / 2 - Ball.Width;

            Canvas.SetLeft(Ball, mittex);
            Canvas.SetTop(Ball, mittey);*/
        }

        private void Ball_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_animationsTimer.IsEnabled)
            {
                zaehler += 1;
                SpielStandLabel.Content = $"{zaehler} Clicks";
            }
        }

        private void Ball_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F)
            {
                Ball.Fill = Brushes.Red;
            }
            if (e.Key == Key.E)
            {
                Ball.Fill = Brushes.Blue;
            }
        }
    }
}
