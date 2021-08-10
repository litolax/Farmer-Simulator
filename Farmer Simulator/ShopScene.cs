using System;
using System.Collections.Generic;
using System.Threading;

namespace Farmer_Simulator
{
    public class ShopScene
    {
        public static Dictionary<string, int> Products = new Dictionary<string, int>(); //название товара, цена

        static ShopScene()
        {
            Products.Add("Одна грядка", 5);
            Products.Add("Две грядки", 9);
            Products.Add("Три грядки", 12);
        }
        public static void StartScene()
        {
            Console.WriteLine("Привет странник, что ты забыл в моей халупе?");
            Console.WriteLine("Вот список товаров, которые я могу тебе продать: ");
            foreach (var item in Products)
            {
                Console.WriteLine($"Название товара: {item.Key}, цена товара(в беларусских донгах): {item.Value}");
            }
            Console.WriteLine("Если хочешь что-то купить - введи команду /buy, если хочешь выйти в меню - /menu");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
        }

        public static void BuyProduct()
        {
            foreach (var item in Products)
            {
                Console.WriteLine($"Название товара: {item.Key}, цена товара(в беларусских донгах): {item.Value}");
            }
            var indexOfProduct = 0;
            var index = 1;
            bool success;
            do
            { 
                Console.WriteLine("Введи номер товара, сверху вниз");
                success = int.TryParse(Console.ReadLine(), out indexOfProduct);
                if (indexOfProduct <= 0 || indexOfProduct > Products.Count) success = false;
                if(!success) Console.WriteLine("Неправильно введён номер товара, попробуйте снова...");
            } while (!success);
            foreach (var item in Products)
            {
                if (index!= indexOfProduct)
                {
                    index++;
                }
                else if (index == indexOfProduct)
                {
                    AuthenticationUser.CurrentUser = mongoDB.FindUser(AuthenticationUser.CurrentUser.Login,
                        AuthenticationUser.CurrentUser.Password);
                    if (AuthenticationUser.CurrentUser.Money >= item.Value)
                    {
                        Console.WriteLine("Пытаемся заскамить вас на деньги..");
                        AuthenticationUser.CurrentUser.Money -= item.Value;
                        var user = new User(AuthenticationUser.CurrentUser.Login,
                            AuthenticationUser.CurrentUser.Password)
                        {
                            Id = AuthenticationUser.CurrentUser.Id,
                            Money = AuthenticationUser.CurrentUser.Money,
                            Achievements = AuthenticationUser.CurrentUser.Achievements
                        };
                        mongoDB.ReplaceOneUser(AuthenticationUser.CurrentUser.Id, user);
                        for (int i = 0; i < indexOfProduct; i++)
                        {
                            mongoDB.CreateRidge(AuthenticationUser.CurrentUser.Login);
                            mongoDB.UpdateInfoAchievement("Расширить грядки до 15 ячеек");
                            Thread.Sleep(300);
                        }
                        Console.WriteLine("Покупка успешно проведена...");
                    }
                    else if (AuthenticationUser.CurrentUser.Money < item.Value)
                    {
                        Console.WriteLine("Увы, денег у тебя мало, приходи позже");
                    }
                    break;
                }
            }
            Console.WriteLine("Если хочешь выйти в меню - /menu");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
        }
    }
}