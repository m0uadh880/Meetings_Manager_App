using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Collections.Generic;
using Meetings_Manager_App.Classes;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;

namespace Meetings_Manager_App
{

    public partial class MainWindow : Window
    {

        private UserAccount userAccount;
        private List<UserMeeting> userMeeting;
        private Button lastClickedButton;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            MeetingsButton_Click(MeetingsButton, null);
        }

        public MainWindow(UserAccount userAccount)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            userAccount = new UserAccount();
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
            Button clickedButton = (Button)sender;

            if (lastClickedButton != null)
            {
                lastClickedButton.Background = new SolidColorBrush(Colors.Transparent);
                lastClickedButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            }

            AddMeetingsButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            AddMeetingsButton.Foreground = new SolidColorBrush(Colors.White);

            lastClickedButton = clickedButton;

            AddNewMeetingPage addNewMeetingPage = new AddNewMeetingPage();
            addNewMeetingPage.SetMainFrame(mainFrame);
            mainFrame.Navigate(addNewMeetingPage);
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

        private void MembersButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (lastClickedButton != null)
            {
                lastClickedButton.Background = new SolidColorBrush(Colors.Transparent);
                lastClickedButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            }

            clickedButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            clickedButton.Foreground = new SolidColorBrush(Colors.White);

            lastClickedButton = clickedButton;

            MembersPage membersPage = new MembersPage();
            membersPage.SetMainFrame(mainFrame);
            mainFrame.Navigate(membersPage);
        }

        private void MeetingsButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (lastClickedButton != null)
            {
                lastClickedButton.Background = new SolidColorBrush(Colors.Transparent);
                lastClickedButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            }

            MeetingsButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x58, 0x0C));
            MeetingsButton.Foreground = new SolidColorBrush(Colors.White);

            lastClickedButton = clickedButton;

            MeetingsPage meetingsPage = new MeetingsPage();
            meetingsPage.SetMainFrame(mainFrame);
            mainFrame.Navigate(meetingsPage);
        }

    }
}
