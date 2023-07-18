﻿
using Meetings_Manager_App.Classes;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using TextBox = System.Windows.Controls.TextBox;
using System.Diagnostics.Contracts;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;
using System;
using MahApps.Metro.IconPacks;
using static System.Net.Mime.MediaTypeNames;

namespace Meetings_Manager_App
{
    public partial class AddMeetingWindow : Window
    {
        private Meetings meetings = null;
        private List<UserAccount> accounts;
        private List<Meetings> meetingsList;
        private List<UserMeeting> EmailsAdded = new List<UserMeeting>();
        UserMeeting userEmailAndProjectName = null;
        UserAccount selectedEmail = new UserAccount();
        List<string> selectedMails = new List<string>();


        public AddMeetingWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            ReadDataBase();

            //GuestsDataGrid.ItemsSource = accounts.Select(item => new {
            //   Email = item.Email
            //}).ToList();
        }

        public AddMeetingWindow(Meetings meetings)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            ReadDataBase();

            this.meetings = meetings;
            ProjectNametextBox.Text = meetings.ProjectName;
            DatetextBox.Text = meetings.Date;
            StartWithtextBox.Text = meetings.Time;
            DurationtextBox.Text = meetings.Duration;
            DescriptiontextBox.Text = meetings.Description;
            //GuestsDataGrid.ItemsSource = accounts.Select(item => new {
            //    Email = item.Email
            //}).ToList();
            SaveButton.Content = "Update";
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

        private void MeetingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure ?", "Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                LogInWindow logInWindow = new LogInWindow();
                logInWindow.Show();
                this.Close();
            }
        }

        private void MembersButton_Click(object sender, RoutedEventArgs e)
        {
            MembersWindow membersWindow = new MembersWindow();
            membersWindow.Show();
            Close();
        }

        private void AddMeetingButton_Click(object sender, RoutedEventArgs e)
        {

            if (SaveButton.Content == "Update")
            {
                meetings.ProjectName = ProjectNametextBox.Text;
                meetings.Date = DatetextBox.Text;
                meetings.Time = StartWithtextBox.Text;
                meetings.Duration = DurationtextBox.Text;
                meetings.Description = DescriptiontextBox.Text;

                using (SQLiteConnection conn = new SQLiteConnection(App.MeetingsdatabasePath))
                {
                    conn.CreateTable<Meetings>();
                    conn.Update(meetings);
                }

                using (SQLiteConnection connection = new SQLiteConnection(App.UserMeetingdatabasePath))
                {
                    connection.CreateTable<UserMeeting>();
                    foreach (var item in EmailsAdded)
                    {
                        connection.Insert(item);
                    }
                }

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();

                return;
            }

            Meetings meeting = new Meetings()
            {
                ProjectName = ProjectNametextBox.Text,
                Date = DatetextBox.Text,
                Time = StartWithtextBox.Text,
                Duration = DurationtextBox.Text,
                Description = DescriptiontextBox.Text,
            };

            if (ProjectNametextBox.Text != "" && DatetextBox.Text != "" && StartWithtextBox.Text != "" && DurationtextBox.Text != "" && /*GueststextBox.Text != "" &&*/ DescriptiontextBox.Text != "")
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

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
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
        }

        //private void GuestsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    //string aux = GuestsDataGrid.SelectedItem.ToString();
        //    //selectedEmail = aux.Substring(10, aux.Length - 12);
        //}

        //private void AddButton_Click(object sender, RoutedEventArgs e)
        //{

        //        UserMeeting userEmailAndProjectName = new UserMeeting()
        //        {
        //            Email = selectedEmail,
        //            ProjectName = ProjectNametextBox.Text,
        //        };

        //        EmailsAdded.Add(userEmailAndProjectName);
        //}

        private void GuestsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                
                Grid grid = new Grid();

                ColumnDefinition column1 = new ColumnDefinition();
                ColumnDefinition column2 = new ColumnDefinition();

                column1.Width = new GridLength(250);
                column2.Width = new GridLength(50);

                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);
                

                TextBlock textBlock = new TextBlock();
                textBlock.Text = userEmailAndProjectName.Email;
                textBlock.FontSize = 15;
                textBlock.VerticalAlignment = VerticalAlignment.Center;

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

                grid.Background = new  SolidColorBrush(Color.FromArgb(0xFF, 0xd4, 0xd4, 0xd8));
                stackPanel.Children.Add(grid);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is string text)
            {
                foreach(var item in EmailsAdded)
                {
                    if(text == item.Email)
                    {
                       EmailsAdded.Remove(item);
                        selectedMails.Remove(item.Email);
                        break;
                    }
                }
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            GuestsListView.ItemsSource = accounts.Select(item => item.Email) ;
            TextBox searchTextBox = sender as TextBox;
            var filtredList = accounts.Where(c => c.Email.ToLower().StartsWith(searchTextBox.Text.ToLower())).ToList();
            GuestsListView.ItemsSource = filtredList;
            
        }
    }
}
