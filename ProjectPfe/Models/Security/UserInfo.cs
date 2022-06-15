namespace ProjectPfe.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string? jwttoken { get; set; }
        public string? Role { get; set; }


    }
    public class UserConnected
    {
        public static User user { get; set; }
    }
}