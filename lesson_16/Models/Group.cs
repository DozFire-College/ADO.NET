namespace ASP_College.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public override string ToString()
        {
            return $" |{GroupId}| группа {GroupName} ";
        }
    }
}
