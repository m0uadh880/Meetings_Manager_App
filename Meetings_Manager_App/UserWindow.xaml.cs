using Meetings_Manager_App.Classes;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Button = System.Windows.Controls.Button;

namespace Meetings_Manager_App
{

    public partial class UserWindow : Window
    {
        UserAccount user;
        List<Meetings> meetings;
        List<UserMeeting> userMeetings;
        private Button lastClickedButton;


        public UserWindow(UserAccount user)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            user = new UserAccount();
            meetings = new List<Meetings>();
            userMeetings = new List<UserMeeting>();
            lastClickedButton = new Button();
            ReadMeetingsDataBase();

            this.user = user;
            UserNameTextBlock.Text = user.Username;

            var projectNames = userMeetings.Where(a => a.Email == user.Email).Select(item => item.ProjectName);
            ButtonsItemControl.ItemsSource = projectNames;
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
            using (SQLiteConnection connection = new SQLiteConnection(App.MeetingsdatabasePath))
            {
                connection.CreateTable<Meetings>();
                meetings = connection.Table<Meetings>().ToList();
            }

            using (SQLiteConnection connection = new SQLiteConnection(App.UserMeetingdatabasePath))
            {
                connection.CreateTable<UserMeeting>();
                userMeetings = connection.Table<UserMeeting>().ToList();
            }
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (lastClickedButton != null)
            {
                // Restore the background of the last clicked button
                lastClickedButton.Background = null;
                lastClickedButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            }

            // Change the background of the clicked button
            clickedButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            clickedButton.Foreground = new SolidColorBrush(Colors.White);

            // Update the last clicked button reference
            lastClickedButton = clickedButton;

            
            string projectName = (string)clickedButton.Content;
            pageTitle.Text = projectName;

            var relatedMeetings = meetings.Where(m => m.ProjectName == projectName).ToList();
            MeetingsDataGrid.ItemsSource = relatedMeetings;
        }

    }

}
