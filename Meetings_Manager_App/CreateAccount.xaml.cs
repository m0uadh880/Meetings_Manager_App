using Meetings_Manager_App.Classes;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Meetings_Manager_App
{
    public partial class CreateAccount : Window
    {
        UserAccount userAccount= new UserAccount();
        private List<UserAccount> accounts = new List<UserAccount>();

        public CreateAccount()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            userAccount = new UserAccount();
            ReadDataBase();

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        void ReadDataBase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.UserAccountdatabasePath))
            {
                conn.CreateTable<UserAccount>();
                accounts = conn.Table<UserAccount>().ToList();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;
            string confirmPassword = ConfirmPasswordTextBox.Password;
            bool Emailtest = false;
            bool Usernametest = false;

            foreach (var item in accounts)
            {
                if(item.Email == email)
                {
                    Emailtest = true;
                    break;
                }
                if (item.Username == username)
                {
                    Usernametest = true;
                    break;
                }
            }

            if (email != "" && username != "" && password != "" && confirmPassword != "" && confirmPassword == password) {
                if (!Emailtest)
                {
                    if(!Usernametest)
                    {
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
                    else
                    {
                        System.Windows.MessageBox.Show("Username already exists", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    }
                }
               else
                {
                    System.Windows.MessageBox.Show("Email already exists", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
            }
            else
            {
                    System.Windows.MessageBox.Show("Please enter all the inputs", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }
    }
    
}
