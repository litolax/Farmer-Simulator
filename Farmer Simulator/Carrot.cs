namespace Farmer_Simulator
{
    public class Carrot : IPlant
    {
        public string PlantName { get; set; }
        public int GrowingTime { get; set; }
        public int Price { get; set; }
        public bool IsGrown { get; set; }
        public TypeOfPlant TypeOfPlant { get; set; }

        public Carrot()
        {
            PlantName = "Морковка";
            GrowingTime = 2;
            Price = 2;
            IsGrown = false;
            TypeOfPlant = TypeOfPlant.Carrot;
        }
    }
}