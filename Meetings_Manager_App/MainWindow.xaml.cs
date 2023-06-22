using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

using System.Windows.Media;


namespace Meetings_Manager_App
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            ObservableCollection<Meeting> meetings = new ObservableCollection<Meeting>();

            
            MeetingsDataGrid.ItemsSource = meetings;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void AddNewMeeeting_Click(object sender, RoutedEventArgs e)
        {
            AddMeetingWindow addMeetingWindow = new AddMeetingWindow();
            
            addMeetingWindow.Show();
            Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Show();
            Close();
        }
    }

    public class Meeting
    {
        public string Number { get; set; }
        public string ProjectName { get; set; }
        public string DateAndTime { get; set; }
        public string Duration { get; set; }
        public string Guests { get; set; }
        public string Description { get; set; }

    }
}
