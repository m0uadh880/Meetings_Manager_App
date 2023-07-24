﻿using MahApps.Metro.IconPacks;
using Meetings_Manager_App.Classes;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace Meetings_Manager_App
{
    public partial class AddNewMeetingPage : Page
    {
        private Meetings meetings = null;
        private List<UserAccount> accounts = new List<UserAccount>();
        private List<Meetings> meetingsList = new List<Meetings>();
        private List<UserMeeting> userMeetings = new List<UserMeeting>();
        private HashSet<UserMeeting> EmailsAdded = null;
        private HashSet<UserMeeting> EmailsAddedAfteUpdate;

        private UserMeeting userEmailAndProjectName = null;
        private UserAccount selectedEmail = new UserAccount();
        private List<string> selectedMails = new List<string>();
        private Frame mainFrame;


        public AddNewMeetingPage()
        {
            InitializeComponent();

            EmailsAdded = new HashSet<UserMeeting>();
            ReadDataBase();
        }
        public AddNewMeetingPage(Meetings meetings)
        {
            InitializeComponent();
            ReadDataBase();
            EmailsAddedAfteUpdate = new HashSet<UserMeeting>();

            this.meetings = meetings;
            ProjectNametextBox.Text = meetings.ProjectName;
            DatetextBox.Text = meetings.Date;
            TimePicker.Text = meetings.Time;
            //DurationtextBox.Text = meetings.Duration;
            DescriptiontextBox.Text = meetings.Description;
            selectedMails.Clear();

            EmailsAdded = userMeetings.Where(item => item.ProjectName.Equals(meetings.ProjectName)).Distinct().ToHashSet();
            foreach (var item in EmailsAdded)
            {
                if (!selectedMails.Contains(item.Email))
                {
                    selectedMails.Add(item.Email);
                    showEmailsAdded(item.Email);
                }
            }

            SaveButton.Content = "Update";
        }

        private void AddMeetingButton_Click(object sender, RoutedEventArgs e)
        {

            if ((string)SaveButton.Content == "Update")
            {
                meetings.ProjectName = ProjectNametextBox.Text;
                meetings.Date = DatetextBox.Text;
                meetings.Time = TimePicker.Text;
                //meetings.Time = StartWithtextBox.Text;
                //meetings.Duration = DurationtextBox.Text;
                meetings.Description = DescriptiontextBox.Text;

                using (SQLiteConnection conn = new SQLiteConnection(App.MeetingsdatabasePath))
                {
                    conn.CreateTable<Meetings>();
                    conn.Update(meetings);
                }

                using (SQLiteConnection connection = new SQLiteConnection(App.UserMeetingdatabasePath))
                {
                    connection.CreateTable<UserMeeting>();
                    foreach (var item in EmailsAddedAfteUpdate)
                    {
                        connection.Insert(item);
                    }
                }

                MeetingsPage meetingsPage = new MeetingsPage();
                meetingsPage.SetMainFrame(mainFrame);
                mainFrame.Navigate(meetingsPage);
            }
            else
            {
                Meetings meeting = new Meetings()
                {
                    ProjectName = ProjectNametextBox.Text,
                    Date = DatetextBox.Text,
                    Time = TimePicker.Text,
                    Duration = hoursIntegerUpDown.Text + "h and " + minutesIntegerUpDown.Text + "min",
                    Description = DescriptiontextBox.Text,
                };

                if (ProjectNametextBox.Text != "" && DatetextBox.Text != "" && /*StartWithtextBox.Text != "" &&*/ /*DurationtextBox.Text != "" &&*/ /*GueststextBox.Text != "" &&*/ DescriptiontextBox.Text != "")
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.MeetingsdatabasePath))
                    {
                        connection.CreateTable<Meetings>();
                        connection.Insert(meeting);
                    }

                    using (SQLiteConnection connection = new SQLiteConnection(App.UserMeetingdatabasePath))
                    {
                        connection.CreateTable<UserMeeting>();
                        foreach (var item in EmailsAdded)
                        {
                            connection.Insert(item);
                        }
                    }

                    MeetingsPage meetingsPage = new MeetingsPage();
                    meetingsPage.SetMainFrame(mainFrame);
                    mainFrame.Navigate(meetingsPage);
                }
            }

        }
        void ReadDataBase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.UserAccountdatabasePath))
            {
                connection.CreateTable<UserAccount>();
                accounts = connection.Table<UserAccount>().ToList();
            }

            using (SQLiteConnection connection = new SQLiteConnection(App.MeetingsdatabasePath))
            {
                connection.CreateTable<Meetings>();
                meetingsList = connection.Table<Meetings>().ToList();
            }

            using (SQLiteConnection connection = new SQLiteConnection(App.UserMeetingdatabasePath))
            {
                connection.CreateTable<UserMeeting>();
                userMeetings = connection.Table<UserMeeting>().ToList();
            }
        }
        private void GuestsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SaveButton.Content == "Update")
            {
                selectedEmail = (UserAccount)GuestsListView.SelectedItem;

                if (selectedEmail != null && !selectedMails.Contains(selectedEmail.Email))
                {
                    selectedMails.Add(selectedEmail.Email);

                    UserMeeting userEmailAndProjectName = new UserMeeting()
                    {
                        Email = selectedEmail.Email,
                        ProjectName = ProjectNametextBox.Text,
                    };


                    EmailsAddedAfteUpdate.Add(userEmailAndProjectName);
                    showEmailsAdded(userEmailAndProjectName.Email);
                }
            }
            else
            {
                selectedEmail = (UserAccount)GuestsListView.SelectedItem;

                if (selectedEmail != null && !selectedMails.Contains(selectedEmail.Email))
                {
                    selectedMails.Add(selectedEmail.Email);

                    UserMeeting userEmailAndProjectName = new UserMeeting()
                    {
                        Email = selectedEmail.Email,
                        ProjectName = ProjectNametextBox.Text,
                    };


                    EmailsAdded.Add(userEmailAndProjectName);
                    showEmailsAdded(userEmailAndProjectName.Email);
                }
            }

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.DataContext is string text)
            {
                foreach (var item in EmailsAdded)
                {
                    if (text == item.Email)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(App.UserMeetingdatabasePath))
                        {
                            conn.CreateTable<UserMeeting>();
                            conn.Delete(item);
                        }
                        EmailsAdded.Remove(item);
                        selectedMails.Remove(item.Email);
                        break;
                    }
                }
                ReadDataBase();
            }

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filtredList = accounts.Where(c => c.Email.ToLower().StartsWith(searchTextBox.Text.ToLower())).ToList();
            GuestsListView.ItemsSource = filtredList;
        }

        private void showEmailsAdded(string email)
        {


            Grid grid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();

            column1.Width = new GridLength(250);
            column2.Width = new GridLength(50);

            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);
            grid.Height = 25;
            grid.Margin = new Thickness(5);


            TextBlock textBlock = new TextBlock();
            textBlock.Text = email;
            textBlock.FontSize = 18;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.Padding = new Thickness(10);

            PackIconMaterial packIcon = new PackIconMaterial();
            packIcon.Kind = PackIconMaterialKind.Close;

            Button deleteButton = new Button();

            deleteButton.BorderBrush = null;
            deleteButton.BorderThickness = new Thickness(0);
            deleteButton.Content = packIcon;
            deleteButton.Click += DeleteButton_Click;
            deleteButton.VerticalAlignment = VerticalAlignment.Center;
            deleteButton.Background = null;
            deleteButton.DataContext = textBlock.Text;


            deleteButton.Click += (sender3, e3) =>
            {
                if (stackPanel.Children.Contains(grid))
                {
                    stackPanel.Children.Remove(grid);
                }
            };

            grid.Children.Add(textBlock);
            grid.Children.Add(deleteButton);

            Grid.SetColumn(textBlock, 0);
            Grid.SetColumn(deleteButton, 1);

            grid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xa8, 0xa2, 0x9e));
            stackPanel.Children.Add(grid);
        }
        public void SetMainFrame(Frame frame)
        {
            mainFrame = frame;
        }

        private void GuestsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filtredList = accounts.Where(c => c.Email.ToLower().StartsWith(searchTextBox.Text.ToLower())).ToList();
            GuestsListView.ItemsSource = filtredList;
        }
    }
}