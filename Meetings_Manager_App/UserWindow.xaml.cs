using Meetings_Manager_App.Classes;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Meetings_Manager_App
{
    
    public partial class UserWindow : Window
    {
        UserAccount user;
        List<Meetings> meetings;
        public UserWindow(UserAccount user)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            this.user = user;
            UserNameTextBlock.Text = user.Username;
            ReadMeetingsDataBase();

            string projectName = ButtonTextBlock.Text;

            var relatedMeetings = meetings.Where(m => m.ProjectName == projectName).ToList();
            MeetingsDataGrid.ItemsSource = relatedMeetings;
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                LogInWindow logInWindow = new LogInWindow();
                logInWindow.Show();
                this.Close();
            }
        }


        void ReadMeetingsDataBase()
        {
            using(SQLiteConnection connection = new SQLiteConnection(App.MeetingsdatabasePath))
            {
                connection.CreateTable<Meetings>();
                meetings = connection.Table<Meetings>().ToList();
            }
        }
    }

}
