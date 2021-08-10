using System;

namespace Farmer_Simulator
{
    public static class GameScene
    {
        public static void StartScene()
        {
            Console.WriteLine("Вот ты своими ногами и дошел до грядок... Осмотрись и подумай что ты будешь делать..");
            Console.WriteLine("/garden - посмотреть состояние грядок, /plant - посадить  /collect - собрать нужную грядку");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
        }
    }
}