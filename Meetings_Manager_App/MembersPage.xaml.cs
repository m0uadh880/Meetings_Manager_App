using Meetings_Manager_App.Classes;
using SQLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;


namespace Meetings_Manager_App
{
    public partial class MembersPage : Page
    {
        private UserAccount selectedMeeting;
        private Frame mainFrame;

        public MembersPage()
        {
            InitializeComponent();
            ReadDataBase();

        }
        void ReadDataBase()
        {
            List<UserAccount> userAccounts;

            using (SQLiteConnection conn = new SQLiteConnection(App.UserAccountdatabasePath))
            {
                conn.CreateTable<UserAccount>();
                userAccounts = conn.Table<UserAccount>().ToList();
            }

            if (userAccounts != null)
            {
                MembersDataGrid.ItemsSource = userAccounts;
            }

        }
        private void MembersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMeeting = new UserAccount();
            selectedMeeting = (UserAccount)MembersDataGrid.SelectedItem;

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                using (SQLiteConnection conn = new SQLiteConnection(App.UserAccountdatabasePath))
                {
                    conn.CreateTable<UserAccount>();
                    conn.Delete(selectedMeeting);
                }
                ReadDataBase();
            }

        }
        public void SetMainFrame(Frame frame)
        {
            mainFrame = new Frame();
            mainFrame = frame;
        }
    }
}
