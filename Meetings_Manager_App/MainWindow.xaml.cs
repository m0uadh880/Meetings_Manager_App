using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using SQLite;
using System.Collections.Generic;
using Meetings_Manager_App.Classes;
using Meetings_Manager_App.Properties;
using System.Linq;

namespace Meetings_Manager_App
{

    public partial class MainWindow : Window
    {

        Meetings selectedMeeting = new Meetings();
        UserAccount userAccount;
        private List<UserMeeting> userMeeting;
        private List<UserMeeting> GuestesEmailsOfSelectedProject = new List<UserMeeting>();


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;


            ReadDataBase();
        }
        public MainWindow(UserAccount userAccount)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            ReadDataBase();
            this.userAccount = userAccount;
            AdminNameTextBlock.Text = userAccount.Username;

            
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
            ReadDataBase();
            Close();

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                LogInWindow logInWindow = new LogInWindow();
                logInWindow.Show();
                Close();
            }
        }

        void ReadDataBase()
        {
            List<Meetings> meetings;
            using (SQLiteConnection conn = new SQLiteConnection(App.MeetingsdatabasePath))
            {
                conn.CreateTable<Meetings>();
                meetings = conn.Table<Meetings>().ToList();
            }

            if (meetings != null)
            {
                MeetingsDataGrid.ItemsSource = meetings;
            }


            using (SQLiteConnection connection = new SQLiteConnection(App.UserMeetingdatabasePath))
            {
                connection.CreateTable<UserMeeting>();
                userMeeting = connection.Table<UserMeeting>().ToList();
            }

        }

        private void MeetingsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedMeeting = (Meetings)MeetingsDataGrid.SelectedItem;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMeeting != null)
            {
                AddMeetingWindow updateMeetingWindow = new AddMeetingWindow(selectedMeeting);
                updateMeetingWindow.Show();
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.MeetingsdatabasePath))
                {
                    conn.CreateTable<Meetings>();
                    conn.Delete(selectedMeeting);
                }
                ReadDataBase();
            }
            
        }

        private void MembersButton_Click(object sender, RoutedEventArgs e)
        {
            MembersWindow membersWindow = new MembersWindow();
            membersWindow.Show();
            Close();
        }

        private void ShowGuestsButton_Click(object sender, RoutedEventArgs e)
        {
            string projectName = selectedMeeting.ProjectName;
            if (projectName != null) {

                GuestesEmailsOfSelectedProject = userMeeting.Where(item => item.ProjectName == projectName).ToList();

                GuestsWindow guestsWindow = new GuestsWindow(GuestesEmailsOfSelectedProject);
                guestsWindow.Show();
            }
        }
    }

}
