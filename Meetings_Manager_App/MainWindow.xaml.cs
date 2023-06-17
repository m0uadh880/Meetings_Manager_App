using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace Meetings_Manager_App
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<Meeting> meetings = new ObservableCollection<Meeting>();

            meetings.Add(new Meeting { Number = "1", ProjectName = "Project 1", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "2", ProjectName = "Project 2", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "3", ProjectName = "Project 3", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "4", ProjectName = "Project 4", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "5", ProjectName = "Project 5", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "6", ProjectName = "Project 6", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "7", ProjectName = "Project 7", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "8", ProjectName = "Project 8", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "9", ProjectName = "Project 9", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "10", ProjectName = "Project 10", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "11", ProjectName = "Project 11", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "12", ProjectName = "Project 12", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min",Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "1", ProjectName = "Project 1", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "2", ProjectName = "Project 2", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "3", ProjectName = "Project 3", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "4", ProjectName = "Project 4", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "5", ProjectName = "Project 5", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "6", ProjectName = "Project 6", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "7", ProjectName = "Project 7", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "8", ProjectName = "Project 8", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "9", ProjectName = "Project 9", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "10", ProjectName = "Project 10", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "11", ProjectName = "Project 11", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });
            meetings.Add(new Meeting { Number = "12", ProjectName = "Project 12", DateAndTime = "23 juin 2023 , 7AM", Duration = "30 min", Guests = "The marketing team" });

            MeetingsDataGrid.ItemsSource = meetings;
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
    }

    public class Meeting
    {
        public string Number { get; set; }
        public string ProjectName { get; set; }
        public string DateAndTime { get; set; }
        public string Duration { get; set; }
        public string Guests { get; set; }
    }
}
