using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Farmer_Simulator
{
    public interface IUser
    {
        [BsonId]
        ObjectId Id { get; set; }
        string Login { get; set; }
        string Password { get; set; } 
        int Money { get; set; }
    }
}