namespace Farmer_Simulator
{
    public interface IPlant
    {
        public string PlantName { get; set; }
        int GrowingTime { get; set; }
        int Price { get; set; }
        bool IsGrown { get; set; }
        TypeOfPlant TypeOfPlant { get; set; }
    }
}