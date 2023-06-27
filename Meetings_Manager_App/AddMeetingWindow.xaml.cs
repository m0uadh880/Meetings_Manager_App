
using Meetings_Manager_App.Classes;
using SQLite;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Meetings_Manager_App
{
    /// <summary>
    /// Interaction logic for AddMeetingWindow.xaml
    /// </summary>
    public partial class AddMeetingWindow : Window
    {

        public AddMeetingWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
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

        private void MeetingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo);

            if(result == System.Windows.Forms.DialogResult.Yes)
            {
                LogInWindow logInWindow = new LogInWindow();
                logInWindow.Show();
                this.Close();
            }
        }

        private void AddMeetingButton_Click(object sender, RoutedEventArgs e)
        {
            Meetings meeting = new Meetings()
            {
                ProjectName = ProjectNametextBox.Text,
                Date = DatetextBox.Text,
                Time = StartWithtextBox.Text,
                Duration = DurationtextBox.Text,
                Guests = GueststextBox.Text,
                Description = DescriptiontextBox.Text,
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Meetings>();
                connection.Insert(meeting);
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
