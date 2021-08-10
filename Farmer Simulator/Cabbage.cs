namespace Farmer_Simulator
{
    public class Cabbage : IPlant
    {
        public string PlantName { get; set; }
        public int GrowingTime { get; set; }
        public int Price { get; set; }
        public bool IsGrown { get; set; }
        
        public TypeOfPlant TypeOfPlant { get; set; }

        public Cabbage()
        {
            PlantName = "Капуста";
            GrowingTime = 1;
            Price = 1;
            IsGrown = false;
            TypeOfPlant = TypeOfPlant.Cabbage;
        }
    }
}