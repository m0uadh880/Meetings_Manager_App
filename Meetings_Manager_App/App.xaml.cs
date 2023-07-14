using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Meetings_Manager_App
{
    public partial class App : Application
    {
        static string DatabaseName1 = "Meetings.db";
        static string folderPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string MeetingsdatabasePath = System.IO.Path.Combine(folderPath1, DatabaseName1);

        static string DatabaseName2 = "UserAccount.db";
        static string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string UserAccountdatabasePath = System.IO.Path.Combine(folderPath2, DatabaseName2);

        static string DatabaseName3 = "UserMeeting.db";
        static string folderPath3 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string UserMeetingdatabasePath = System.IO.Path.Combine(folderPath3, DatabaseName3);
    }
}
