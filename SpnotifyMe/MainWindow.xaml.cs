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
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using Hardcodet.Wpf.TaskbarNotification;

namespace SpnotifyMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispTimer = new DispatcherTimer();
        DispatcherTimer hsw = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 5;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 45;

            this.Topmost = true;

            hsw.Tick += new EventHandler(hsWindow_Tick);
            hsw.Interval = new TimeSpan(0, 0, 10);

            dispTimer.Tick += new EventHandler(dispTimer_Tick);
            dispTimer.Interval = new TimeSpan(0, 0, 1);
            dispTimer.Start();
        }

        private void hsWindow_Tick(object sender, EventArgs e)
        {
            this.hsw.Stop();

            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void dispTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Process[] spotify_proc = Process.GetProcessesByName("spotify");
                
                if(spotify_proc.Length > 0)
                {
                    string nameOfSong = spotify_proc[0].MainWindowTitle;
                    nameOfSong = nameOfSong.Remove(0, 10);

                    if (CurrentSong.Text == nameOfSong)
                    {
                        if(!hsw.IsEnabled && this.Visibility == System.Windows.Visibility.Visible)
                            hsw.Start();
                    }
                    else
                    {
                        CurrentSong.Text = nameOfSong;
                        this.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            catch (Exception af)
            {
                Debug.WriteLine(af.Message);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
