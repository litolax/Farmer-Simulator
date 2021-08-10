using System;
using System.Threading;
using MongoDB.Driver;

namespace Farmer_Simulator
{
    public class AuthenticationUser
    {
        public static User CurrentUser;


        public static void Login()
        {
            var successfully = false;
            do
            {
                Console.WriteLine("Введите свой логин и затем пароль");
                var login = Console.ReadLine();
                var password = Console.ReadLine();
                Console.WriteLine("Идет проверка данных...");
                CurrentUser = mongoDB.FindUser(login, password);
                if (CurrentUser is not null)
                {
                    mongoDB.UpdateInfoAchievement("Пять первых заходов в игру");
                    Console.WriteLine("Вход выполнен успешно...");
                    successfully = true;
                    Thread.Sleep(1200);
                }
                else if (CurrentUser is null)
                {
                    Console.WriteLine("Вы неверно ввели данные или такого плоьзователя не существует. Попробуйте еще раз...");
                    successfully = false;
                    Thread.Sleep(1200);
                }
            }while(!successfully);
        }

        public static void Registration()
        {  
            var successfully = false;
            do
            {
                Console.WriteLine("Введите сначало свой логин и затем пароль");
                var login = Console.ReadLine();
                var password = Console.ReadLine();
                Console.WriteLine("Идет регистрация и проверка данных...");
                var user = mongoDB.userCollection.Find(u => u.Login == login).FirstOrDefault();
                if (user is null)
                {
                    user = new User(login, password){Money = 0, Achievements = new Achievements()};
                    mongoDB.userCollection.InsertOneAsync(user);
                    CurrentUser = mongoDB.FindUser(login, password);
                    //Инициализация грядок
                    for (int i = 0; i < 2; i++)
                    {
                        mongoDB.CreateRidge(CurrentUser.Login);
                    }
                    Console.WriteLine("Регистрация прошла успешна..");
                    successfully = true;
                    Thread.Sleep(1200);
                }
                else if (user is not null)
                {
                    Console.WriteLine($"Пользователь с логином {login} уже существует, попробуйте ввести другой.");
                    successfully = false;
                    Thread.Sleep(1200);
                }
            } while (!successfully);
        }
    }
}