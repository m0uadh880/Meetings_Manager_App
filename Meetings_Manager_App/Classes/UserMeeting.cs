using SQLite;

namespace Meetings_Manager_App.Classes
{
    public class UserMeeting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
    }
}
