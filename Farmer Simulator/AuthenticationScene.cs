using System;

namespace Farmer_Simulator
{
    public class AuthenticationScene
    {
        public static void StartScene()
        {
            Console.WriteLine("Вы хотите зарегистрироваться или войти? /reg - регистрация, /login - вход.");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
            MenuScene.StartScene();
        }
    }
    
    
}