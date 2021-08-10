using System;


namespace Farmer_Simulator
{
    class MenuScene
    { 
        public static void StartScene()
        {
            Console.Write($"Приветствуем {AuthenticationUser.CurrentUser.Login}, команды для продолжения: ");
            Console.WriteLine("/play - идти возиться с грядками, /shop - магазин, /achievements - посмотреть достижения");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
        }
    }
}