namespace GameHub.DAL.DataModels
{
    public class UserDataModel : BaseDataModel
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
