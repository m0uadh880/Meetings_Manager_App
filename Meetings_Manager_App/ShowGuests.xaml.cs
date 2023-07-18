using Meetings_Manager_App.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Meetings_Manager_App
{
    
    public partial class ShowGuests : Page
    {
        public ShowGuests(List<UserMeeting> userMeeting)
        {
            InitializeComponent();
            GuestsListView.ItemsSource = userMeeting.Select(item => new
            {
                Email = item.Email,
            });
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
