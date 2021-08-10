using MongoDB.Bson;

namespace Farmer_Simulator
{
    public class User : IUser 
    {
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Money { get; set; }
        public Achievements Achievements { get; set; }

        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
            Money = 0;
            var achievements = new Achievements();
        }
    }
}