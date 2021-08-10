using System;
using MongoDB.Bson;

namespace Farmer_Simulator
{
    public interface IRidge
    {
        ObjectId Id { get; set; }
        string Login { get; set; }
        bool IsPlanted { get; set; }
        IPlant Plant { get; set; }

        DateTime TimeOfLanding { get; set; }
        
    }
}