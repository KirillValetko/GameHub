namespace GameHub.DAL.Models
{
    public class User : BaseDbModel
    {
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
