using Meetings_Manager_App.Classes;
using SQLite;
using System.Windows;

namespace Meetings_Manager_App
{
    public partial class CreateAccount : Window
    {
        UserAccount userAccount;

        public CreateAccount()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            userAccount = new UserAccount();

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;
            string confirmPassword = ConfirmPasswordTextBox.Password;

            if(email != null && username != null && password != null && confirmPassword != null && confirmPassword == password) { 
                userAccount.Email = email;
                userAccount.Username = username;
                userAccount.Password = password;
                userAccount.IsAdmin = false;

                using (SQLiteConnection connection = new SQLiteConnection(App.UserAccountdatabasePath))
                {
                    connection.CreateTable<UserAccount>();
                    connection.Insert(userAccount);
                }

                LogInWindow logInWindow = new LogInWindow();
                logInWindow.Show();
                Close();
            }
        }
    }
    
}
