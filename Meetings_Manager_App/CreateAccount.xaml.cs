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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Meetings_Manager_App
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        UserAccount userAccount = new UserAccount();

        public CreateAccount()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

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
