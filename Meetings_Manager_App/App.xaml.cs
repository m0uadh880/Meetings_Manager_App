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
        static string DatabaseName = "Meetings.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(folderPath, DatabaseName);
    }
}
