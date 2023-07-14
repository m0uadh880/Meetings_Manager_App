using SQLite;

namespace Meetings_Manager_App.Classes
{
    public class UserMeeting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int userId { get; set; }
        [Indexed]
        public int MeetingId { get; set; }
    }
}
