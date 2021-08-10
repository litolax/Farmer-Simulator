namespace Farmer_Simulator
{
    public class Watermelon : IPlant
    {
        public string PlantName { get; set; }
        public int GrowingTime { get; set; }
        public int Price { get; set; }
        public bool IsGrown { get; set; }
        public TypeOfPlant TypeOfPlant { get; set; }

        public Watermelon()
        {
            PlantName = "Арбуз";
            GrowingTime = 3;
            Price = 4;
            IsGrown = false;
            TypeOfPlant = TypeOfPlant.Watermelon;
        }
    }
}