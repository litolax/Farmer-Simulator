using System;
using System.Collections.Generic;

namespace Farmer_Simulator
{
    public class SlashCommandsManager
    {
        public static Dictionary<string, Action> Commands = new Dictionary<string, Action>();
        public static bool Successfully = false;
        static SlashCommandsManager()
        {
            Commands.Add("/login", AuthenticationUser.Login);
            Commands.Add("/reg", AuthenticationUser.Registration);
            Commands.Add("/play", GameScene.StartScene);
            Commands.Add("/achievements", AchievementsScene.StartScene);
            Commands.Add("/shop", ShopScene.StartScene);
            Commands.Add("/garden", Ridge.ShowGarden);
            Commands.Add("/collect", Ridge.Collect);
            Commands.Add("/plant", Ridge.PlantInTheGarden);
            Commands.Add("/menu", MenuScene.StartScene);
            Commands.Add("/buy", ShopScene.BuyProduct);
        }
        public static void CommandProcessing(string command)
        {
            Successfully = false;
            foreach (var item in Commands)
            {
                if (item.Key == command)
                {
                    Console.Clear();
                    item.Value.Invoke();
                    Successfully = true;
                }
            }
            if (!Successfully)
            { 
                Console.WriteLine("Увы, команды такой команды не существует, попробуйте еще раз.");
            }
        }
    }
}