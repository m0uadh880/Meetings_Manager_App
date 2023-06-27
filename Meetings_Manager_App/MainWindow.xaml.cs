using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using SQLite;
using System.Collections.Generic;
using Meetings_Manager_App.Classes;

namespace Meetings_Manager_App
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
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
            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Meetings>();
                meetings = conn.Table<Meetings>().ToList();
            }

            if (meetings != null)
            {
                MeetingsDataGrid.ItemsSource = meetings;
            }

        }

    }

}
