using System;

namespace Farmer_Simulator
{
    public class AchievementsScene
    {
        public static void StartScene()
        {
            Console.WriteLine("Дарова умный... Тут ты свои достижения узреть можешь:");
            var user = mongoDB.FindUser(AuthenticationUser.CurrentUser.Login, AuthenticationUser.CurrentUser.Password);
            user.Achievements.GetInfo(user.Achievements.Achievement, user.Achievements.AchievementProgress);
            Console.WriteLine("Если хочешь выйти в меню - /menu");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
        }
    }
}