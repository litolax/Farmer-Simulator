using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;


namespace Farmer_Simulator
{

    public static class mongoDB
    {
        public static string tableDBName = "Farmer-Simulator";
        public static string UserCollectionName = "User";
        public static string RidgesCollectionName = "Ridges";

        public static string connectionString =
            "mongodbString";

        public static MongoClient client = new MongoClient(connectionString);
        public static IMongoDatabase database = client.GetDatabase(tableDBName);
        public static IMongoCollection<User> userCollection = database.GetCollection<User>(UserCollectionName);
        

        public static IMongoCollection<Ridge> ridgesCollection = database.GetCollection<Ridge>(RidgesCollectionName);


        static mongoDB()
        {
            BsonClassMap.RegisterClassMap<Cabbage>();
            BsonClassMap.RegisterClassMap<Carrot>();
            BsonClassMap.RegisterClassMap<Watermelon>();
        }
        public static User FindUser(string login, string password)
        {
            var user = userCollection.Find(u => u.Login == login && u.Password == password).FirstOrDefault();
            return user;
        }
        public static void ReplaceOneUser(ObjectId id, User user)
        {
            userCollection.ReplaceOne(s => s.Id == id, user);
        }
        public static void CreateRidge(string login)
        {
            var ridges = new Ridge(login);
            ridgesCollection.InsertOne(ridges);
        }
        public static void ReplaceOneRidge(ObjectId id, Ridge ridge)
        {
            ridgesCollection.ReplaceOne(s => s.Id == id, ridge);
        }
        public static List<Ridge> ReadUserRidges()
        {
            var ridges = ridgesCollection.Find(r => r.Login == AuthenticationUser.CurrentUser.Login).ToList();
            return ridges;
        }
        public static void UpdateInfoAchievement(string key)
        {
            var user = FindUser(AuthenticationUser.CurrentUser.Login, AuthenticationUser.CurrentUser.Password);
            user.Achievements.Achievement[key]++;
            
            ReplaceOneUser(AuthenticationUser.CurrentUser.Id, user);
        }
    }
}