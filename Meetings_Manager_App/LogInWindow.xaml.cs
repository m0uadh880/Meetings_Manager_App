using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Meetings_Manager_App.Classes;
using SQLite;

namespace Meetings_Manager_App
{
    public partial class LogInWindow : Window
    {
        List<UserAccount> userAccounts;
        public LogInWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            userAccounts = new List<UserAccount>();
            ReadDataBase();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void Signin_Click(object sender, RoutedEventArgs e)
        {
            string EmailInput = EmailtextBox.Text;
            string passwordInput = PasswordtextBox.Password;

            if(EmailInput != "" && passwordInput != "")
            {
                var user = userAccounts.Where(c => c.Email == EmailInput).ToList();
                try
                {
                    if (user[0] != null && user[0].Password == passwordInput && !user[0].IsAdmin)
                    {
                        UserWindow userWindow = new UserWindow(user[0]);
                        userWindow.Show();
                        Close();
                    }
                    else if (user[0] != null && user[0].Password == passwordInput && user[0].IsAdmin)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Email or password is incorrect.", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Email or password is incorrect.", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
                
            }
            else
            {
                if(EmailInput == "" && passwordInput == "")
                    System.Windows.MessageBox.Show("Email and password are empty.", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                else if (EmailInput == "" )
                    System.Windows.MessageBox.Show("Email is empty.", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                else
                    System.Windows.MessageBox.Show("password is empty.", "Invalid Credentials", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);


            }

        }

        private void Create_account_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
            Close();
        }

        void ReadDataBase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.UserAccountdatabasePath))
            {
                conn.CreateTable<UserAccount>();
                userAccounts = conn.Table<UserAccount>().ToList();
            }

            var user = userAccounts.Where(c => c.Email == "admin@gmail.com").ToList();
            if(user.Count == 0) {
                AddAdmin();
            }
        }

        void AddAdmin()
        {
            UserAccount Admin1 = new UserAccount();
            Admin1.IsAdmin = true;
            Admin1.Username = "admin";
            Admin1.Email = "admin@gmail.com";
            Admin1.Password = "admin";
            userAccounts.Add(Admin1);
        }

        private void EmailtextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailtextBox.Text == "Enter Email")
            {
                EmailtextBox.Text = string.Empty;
            }
        }

        private void EmailtextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailtextBox.Text))
            {
                EmailtextBox.Text = "Enter Email";
            }
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBlock.Text == "Enter Password")
            {
                PasswordTextBlock.Text = string.Empty;
            }
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordtextBox.Password))
            {
                PasswordTextBlock.Text = "Enter Password";
            }
        }
    }
}
