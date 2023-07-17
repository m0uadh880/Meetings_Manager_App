using Meetings_Manager_App.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Meetings_Manager_App
{
    public partial class GuestsWindow : Window
    {
        public GuestsWindow(List<UserMeeting> userMeeting)
        {
            InitializeComponent();
            GuestsListView.ItemsSource = userMeeting.Select(item => new
            {
                Email = item.Email,
            });
        }
    }
}
