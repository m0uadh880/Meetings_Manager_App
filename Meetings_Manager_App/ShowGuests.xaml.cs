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
        private Frame mainFrame;

        public ShowGuests(List<UserMeeting> userMeeting)
        {
            InitializeComponent();
            GuestsListView.ItemsSource = userMeeting.Select(item => new
            {
                Email = item.Email,
            });
        }

        public void SetMainFrame(Frame frame)
        {
            mainFrame = new Frame();
            mainFrame = frame;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MeetingsPage meetingsPage = new MeetingsPage();
            meetingsPage.SetMainFrame(mainFrame);
            mainFrame.Navigate(meetingsPage);
        }
    }
}
