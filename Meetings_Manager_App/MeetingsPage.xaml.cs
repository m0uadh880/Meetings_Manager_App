using Meetings_Manager_App.Classes;
using SQLite;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Meetings_Manager_App
{
    public partial class MeetingsPage : Page
    {
        private Meetings selectedMeeting;
        private UserAccount userAccount;
        private List<UserMeeting> userMeeting;
        private List<UserMeeting> GuestesEmailsOfSelectedProject;
        private Frame mainFrame = new Frame();

        public MeetingsPage()
        {
            InitializeComponent();
            ReadDataBase();
        }
        public MeetingsPage(UserAccount userAccount)
        {
            InitializeComponent();
            ReadDataBase();

            userAccount = new UserAccount();
            this.userAccount = userAccount;
        }
        void ReadDataBase()
        {
            List<Meetings> meetings = new List<Meetings>();
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
            selectedMeeting = new Meetings();
            selectedMeeting = (Meetings)MeetingsDataGrid.SelectedItem;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMeeting != null)
            {
                AddNewMeetingPage addNewMeetingPage = new AddNewMeetingPage(selectedMeeting);
                addNewMeetingPage.SetMainFrame(mainFrame);
                mainFrame.Navigate(addNewMeetingPage);
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
                using (SQLiteConnection conn = new SQLiteConnection(App.UserMeetingdatabasePath))
                {
                    conn.CreateTable<UserMeeting>();
                    foreach (var item in userMeeting)
                    {
                        if (item.ProjectName == selectedMeeting.ProjectName)
                        {
                            conn.Delete(item);
                        }
                    }
                }
                ReadDataBase();
            }

        }

        public void SetMainFrame(Frame frame)
        {
            mainFrame = frame;
        }

        private void ShowGuestsButton_Click(object sender, RoutedEventArgs e)
        {

            string projectName = selectedMeeting.ProjectName;
            if (projectName != null)
            {

                GuestesEmailsOfSelectedProject = new List<UserMeeting>();
                GuestesEmailsOfSelectedProject = userMeeting.Where(item => item.ProjectName == projectName).ToList();

                ShowGuests showGuests = new ShowGuests(GuestesEmailsOfSelectedProject);
                showGuests.SetMainFrame(mainFrame);
                mainFrame.Navigate(showGuests);
            }
        }
    }
}