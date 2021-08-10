using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Farmer_Simulator
{
    [BsonDiscriminator("Cabbage")]
    public class Ridge : IRidge
    {
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public bool IsPlanted { get; set; }
        public IPlant? Plant { get; set; }
        public DateTime TimeOfLanding { get; set; }
        
        public Ridge(string login)
        {
            this.Login = login;
        }

        public static void ShowGarden()
        {
            var ridges = mongoDB.ReadUserRidges();
            foreach (var item in ridges)
            {
                item.CheckForCollect(item);
                item.GetInfo();
            }
            Console.WriteLine("Введите /menu чтобы вернуться в меню и /play - чтобы заниматься дальше грядками");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
        }

        public static void Collect()
        {
            var ridges = mongoDB.ReadUserRidges();
            var index = 1;
            foreach (var item in ridges)
            {
                item.CheckForCollect(item);
                item.GetInfo();
            }
            bool success;
            int indexOfRidge;
            do
            {
                Console.WriteLine("Введите номер грядки сверху вниз, которую хотите собрать..");
                success = int.TryParse(Console.ReadLine(), out indexOfRidge);
                if (indexOfRidge > ridges.Count || indexOfRidge <= 0) success = false;
                if(!success)Console.WriteLine("Ошибка в выборе номера грядки, попробуйте еще раз...");
            } while (!success);
                foreach (var item in ridges)
                {
                    if (index != indexOfRidge)
                    {
                        index++;
                    }
                    else if (index == indexOfRidge)
                    {
                        //Это наша грядка
                        if (item.Plant is null)
                        {
                            Console.WriteLine("На этой грядке ничего не растет, вы ошиблись...");
                            Console.WriteLine("/garden - посмотреть состояние грядок, /plant - посадить  /collect - собрать нужную грядку");
                            do
                            { 
                                SlashCommandsManager.CommandProcessing(Console.ReadLine());
                            } while (!SlashCommandsManager.Successfully);
                        }
                        else if (item.Plant is not null && item.Plant.IsGrown)
                        {
                            Console.WriteLine("Грядка вскопана...");
                            Console.WriteLine($"Получены деньги за грядку(грошей): {item.Plant.Price}");
                            AuthenticationUser.CurrentUser.Money += item.Plant.Price;
                            var user = new User(AuthenticationUser.CurrentUser.Login, AuthenticationUser.CurrentUser.Password)
                            {
                                Money = AuthenticationUser.CurrentUser.Money, 
                                Id = AuthenticationUser.CurrentUser.Id,
                                Achievements = AuthenticationUser.CurrentUser.Achievements
                            };
                            item.Plant = null;
                            item.IsPlanted = false;
                            mongoDB.ReplaceOneRidge(item.Id, item);
                            mongoDB.ReplaceOneUser(user.Id, user);
                            mongoDB.UpdateInfoAchievement("Первая сборка урожая");
                            Console.WriteLine("Сбор урожая закончен...");
                            Console.WriteLine("Введите /menu чтобы вернуться в меню и /play - чтобы заниматься дальше грядками");
                            do
                            { 
                                SlashCommandsManager.CommandProcessing(Console.ReadLine());
                            } while (!SlashCommandsManager.Successfully);
                        }
                    }
                }
            }

        public static void PlantInTheGarden()
        {
            var ridges = mongoDB.ReadUserRidges();
            foreach (var item in ridges)
            {
                item.CheckForCollect(item);
                item.GetInfo();
            }
            bool success;
            bool success2;
            int indexOfRidge;
            int indexOfPlant;
            do
            { 
                Console.WriteLine("Введите номер грядки сверху вниз, которую вы хотите засадить:");
                success = int.TryParse(Console.ReadLine(), out indexOfRidge);
                if (indexOfRidge > ridges.Count || indexOfRidge <= 0) success = false;
                Console.WriteLine("Введите номер растения, которое хотите посадить: 1 - капуста, 2 - морковка, 3 - арбуз");
                success2 = int.TryParse(Console.ReadLine(), out indexOfPlant);
                if (indexOfPlant <= 0 || indexOfPlant > 3) success = false;
                if(!success || !success2)Console.WriteLine("Ошибка в вводе данных: неправильный номер грядки или растения, попробуйте еще раз...");
            } while (!success || !success2);
            var index = 1;
            foreach (var item in ridges)
            {
                if (index != indexOfRidge)
                {
                    index++;
                }
                else if (index == indexOfRidge)
                {
                    if (index > 0 && index <= ridges.Count)
                    {
                        TypeOfPlant typeOfPlant = (TypeOfPlant)indexOfPlant;
                        switch (typeOfPlant)
                        {
                            case TypeOfPlant.Cabbage:
                            {
                                Cabbage cabbage = new Cabbage();
                                var ridge = new Ridge(AuthenticationUser.CurrentUser.Login)
                                {
                                    IsPlanted = true,
                                    Plant = cabbage,
                                    TimeOfLanding = DateTime.UtcNow,
                                    Id = item.Id

                                };
                                mongoDB.ReplaceOneRidge(item.Id, ridge);
                                mongoDB.UpdateInfoAchievement("Посадить 10 саженцев капусты");
                                break;
                            }
                            case TypeOfPlant.Carrot:
                            {
                                Carrot carrot = new Carrot();
                                var ridge = new Ridge(AuthenticationUser.CurrentUser.Login)
                                {
                                    IsPlanted = true,
                                    Plant = carrot,
                                    TimeOfLanding = DateTime.UtcNow,
                                    Id = item.Id

                                };
                                mongoDB.ReplaceOneRidge(item.Id, ridge);
                                mongoDB.UpdateInfoAchievement("Посадить 10 саженцев морковки");
                                break;
                            }
                            case TypeOfPlant.Watermelon:
                            { 
                                Watermelon watermelon = new Watermelon();
                                var ridge = new Ridge(AuthenticationUser.CurrentUser.Login)
                                {
                                    IsPlanted = true,
                                    Plant = watermelon,
                                    TimeOfLanding = DateTime.UtcNow,
                                    Id = item.Id

                                };
                                mongoDB.ReplaceOneRidge(item.Id, ridge);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            Console.WriteLine("Грядка засажена...");
            Console.WriteLine("Введите /menu чтобы вернуться в меню и /play - чтобы заниматься дальше грядками");
            do
            { 
                SlashCommandsManager.CommandProcessing(Console.ReadLine());
            } while (!SlashCommandsManager.Successfully);
        }

        public void CheckForCollect(Ridge ridge)
        {
            if(ridge.Plant == null) return;
            TimeSpan timeSpan = new TimeSpan(0,ridge.Plant.GrowingTime,0);
            if (ridge.TimeOfLanding.ToLocalTime() + timeSpan < DateTime.Now.ToLocalTime() && ridge.IsPlanted)
            {
                ridge.Plant.IsGrown = true;
            }
        }

        public void GetInfo()
        {
            var plantedCondition = IsPlanted ? "Засажена" : "Пустая";
            string grownCondition = string.Empty;
            if (Plant is not null && Plant.IsGrown)
            {
                grownCondition = "Выросло";
            }
            else if (Plant is not null && !Plant.IsGrown)
            {
                grownCondition = "Растет";
            }
            else if (Plant is null)
            {
                grownCondition = "Неизвестно";
            }
            Console.WriteLine($"Грядка: Состояние грядки: {plantedCondition}, Растение: {Plant?.PlantName ?? "Пусто"}, " +
                              $"Состояние растения: {grownCondition}, Время роста(в минутах): {Plant?.GrowingTime.ToString() ?? "Неизвестно"}");
        }
        
    }
}