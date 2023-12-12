namespace GameHub.BLL.Models
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
