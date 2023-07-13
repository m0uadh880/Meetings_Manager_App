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
using System.Windows.Shapes;

namespace Meetings_Manager_App
{
    public partial class MembersWindow : Window
    {
        UserAccount selectedMeeting = new UserAccount();

        public MembersWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            ReadDataBase();

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
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                LogInWindow logInWindow = new LogInWindow();
                logInWindow.Show();
                Close();
            }
        }

        void ReadDataBase()
        {
            List<UserAccount> userAccounts;

            //string DatabaseName = "UserAccount.db";
            //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string databasePath = System.IO.Path.Combine(folderPath, DatabaseName);

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

        private void MembersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedMeeting = (UserAccount)MembersDataGrid.SelectedItem;

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //string DatabaseName = "UserAccount.db";
                //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //string databasePath = System.IO.Path.Combine(folderPath, DatabaseName);

                using (SQLiteConnection conn = new SQLiteConnection(App.UserAccountdatabasePath))
                {
                    conn.CreateTable<UserAccount>();
                    conn.Delete(selectedMeeting);
                }
                ReadDataBase();
            }

        }

        private void MembersButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
