using System;
using System.Collections.Generic;

namespace Farmer_Simulator
{

    public class Achievements
    {
        public Dictionary<string, int> Achievement = new Dictionary<string, int>();
        public Dictionary<string, int> AchievementProgress = new Dictionary<string, int>();
        public Achievements()
        {
            Achievement.Add("Посадить 10 саженцев капусты", 0);
            Achievement.Add("Посадить 10 саженцев морковки", 0);
            Achievement.Add("Расширить грядки до 15 ячеек", 2);
            Achievement.Add("Первая сборка урожая", 0);
            Achievement.Add("Пять первых заходов в игру", 0);
            //
            AchievementProgress.Add("Посадить 10 саженцев капусты", 10);
            AchievementProgress.Add("Посадить 10 саженцев морковки", 10);
            AchievementProgress.Add("Расширить грядки до 15 ячеек", 15);
            AchievementProgress.Add("Первая сборка урожая", 1);
            AchievementProgress.Add("Пять первых заходов в игру", 5);
        }
        
        public void GetInfo(Dictionary<string, int> achievements, Dictionary<string, int> achievementsProgress)
        {
            var index = 0;
            foreach (var item in achievements)
            {
                foreach (var item2 in achievementsProgress)
                {
                    if (item.Key == item2.Key)
                    {
                        var success = item.Value >= item2.Value;
                        string str = String.Empty;
                        if (success)
                        {
                            str = "Выполнено";
                        }
                        else if (!success)
                        {
                            str = $"{item.Value} / {item2.Value}";
                        }
                        Console.WriteLine($"{item.Key} = {str}");
                    }
                }    
            }
        }
    }
}