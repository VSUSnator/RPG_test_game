using System;
using System.Collections.Generic;
using TextRPGGame.Chacters;
using TextRPGGame.Item;
using TextRPGGame.Enemies;

namespace TextRPGGame.GamePlay
{
    class GameManager
    {
        private Character player;
        private Enemy currentEnemy;
        private Random random = new Random();

        public void Start()
        {
            CreatePlayer();
            ExploreWorld();
        }

        private void CreatePlayer()
        {
            Console.WriteLine("Добро пожаловать в мир RPG!");
            Console.WriteLine("Как зовут вашего героя?");
            string name = Console.ReadLine();

            Console.WriteLine("Какой класс вы выберете для " + name + "?");
            Console.WriteLine("1. Воин");
            Console.WriteLine("2. Маг");
            Console.WriteLine("3. Вор");

            int choice = GetValidIntInput(1, 3);
            player = choice switch
            {
                1 => new Warrior(name),
                2 => new Mage(name),
                3 => new Rogue(name),
                _ => throw new ArgumentOutOfRangeException()
            };

            player.Inventory = new Inventory();
            Console.WriteLine($"{name} теперь {player.GetType().Name}!");

            Potion healingPotion = new Potion("Зелье здоровья", "Восстанавливает здоровье.", 10);
            player.Inventory.AddItem(healingPotion);
        }

        private void ExploreWorld()
        {
            while (player.Health > 0)
            {
                Console.WriteLine($"{player.Name} идет по дороге и встречает врага...");
                currentEnemy = GetRandomEnemy(); // Получение случайного врага

                Console.WriteLine($"{currentEnemy.Name} появляется! Готовьтесь к бою!");
                Battle(currentEnemy); // Передаем текущего врага в метод Battle

                // Возможность передышки после боя
                Console.WriteLine("Хотите отдохнуть? (y/n)");

                // 30% шанс на предупреждающее сообщение
                if (random.Next(0, 100) < 30)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Установим желтый цвет для предупреждения
                    Console.WriteLine("ВЫ ЧУТСВУЕТЕ ЧТО ЕСЛИ ОСТАНОВИТЕСЬ ДЛЯ ОТДЫХА ТО ВЫ МОЖЕТЕ ПОЖАЛЕЕТЕ О ТОМ ЧТО ЖИВЫ!");
                    Console.ResetColor(); // Сбрасываем цвет текста
                }

                if (Console.ReadLine().ToLower() == "y")
                {
                    Rest();
                }
            }

            Console.WriteLine("К сожалению, ваш герой погиб. Игра окончена.");
        }

        private void StartBossEncounter()
        {
            Console.ForegroundColor = ConsoleColor.Red; // Устанавливаем красный цвет текста
            Console.WriteLine("Вы чувствуете, как земля дрожит под ногами... Вы встречаете СВОЮ СМЕРТЬ ВО ПЛОТИ!");
            Console.ResetColor(); // Сбрасываем цвет текста

            Boss boss = new Boss("Гуманоидный Демон", 100, 20, 10, 5);
            boss.Draw(); // Отрисовываем босса
            Battle(boss); // Начинаем бой с боссом
        }

        private void Rest()
        {
            Console.WriteLine($"{player.Name} находит укрытие и отдыхает...");
            int healthRestored = Math.Min(10, player.GetMaxHealth() - player.Health);
            player.Health += healthRestored;
            Console.WriteLine($"Здоровье {player.Name} восстановлено до {player.Health}.");

            // Проверка на появление босса во время отдыха
            if (random.Next(0, 100) < 30) // 30% шанс на появление босса
            {
                StartBossEncounter();
            }
        }

        private void Battle(Enemy enemy)
        {
            currentEnemy = enemy; // Устанавливаем текущего врага

            while (player.Health > 0 && currentEnemy.Health > 0)
            {
                if (player.IsStunned)
                {
                    player.SkipTurn();
                    Console.WriteLine($"{player.Name} пропускает ход из-за оглушения.");
                    continue;
                }

                PerformEnemyTurn();
                if (player.Health <= 0) break;

                PerformPlayerTurn();
            }
        }

        private void PerformEnemyTurn()
        {
            currentEnemy.Attack(player);
            Console.WriteLine($"{currentEnemy.Name} атакует {player.Name}. Осталось здоровья: {player.Health}");
        }

        private void PerformPlayerTurn()
        {
            int choice = ChooseAction();
            switch (choice)
            {
                case 1:
                    player.Attack(currentEnemy);
                    break;
                case 2:
                    player.Defend();
                    break;
                case 3:
                    UsePotion(); // Вызов метода для использования зелья
                    break;
            }

            if (currentEnemy.Health > 0)
            {
                Console.WriteLine($"{player.Name} атакует {currentEnemy.Name}. Осталось здоровья: {currentEnemy.Health}");
            }
            else
            {
                Console.WriteLine($"{currentEnemy.Name} был убит!");
                Loot();
                player.GainExperience(10);
            }
        }

        private void UsePotion()
        {
            // Проверка наличия зелий в инвентаре
            if (player.Inventory.HasItem("Зелье здоровья"))
            {
                Potion healingPotion = (Potion)player.Inventory.GetItem("Зелье здоровья");
                player.Health += healingPotion.HealthRestored; // Восстанавливаем здоровье
                player.Inventory.RemoveItem(healingPotion); // Удаляем зелье из инвентаря
                Console.WriteLine($"{player.Name} использует {healingPotion.Name} и восстанавливает {healingPotion.HealthRestored} здоровья.");
            }
            else
            {
                Console.WriteLine("У вас нет зелий для использования!");
            }
        }

        private Enemy GetRandomEnemy()
        {
            List<Enemy> enemies = new List<Enemy>
            {
                new Goblin("Гоблин", 20, 5, 2, 4),
                new Skeleton("Скелет", 25, 4, 1, 2),
                new Orc("Орк", 30, 6, 3, 8)
            };
            return enemies[random.Next(enemies.Count)];
        }

        private void Loot()
        {
            Console.WriteLine("Вы получили золото и предметы!");
        }

        private int ChooseAction()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Атаковать");
            Console.WriteLine("2. Защищаться");
            Console.WriteLine("3. Использовать предмет");
            return GetValidIntInput(1, 3);
        }

        private int GetValidIntInput(int min, int max)
        {
            int input;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out input) && input >= min && input <= max)
                {
                    return input;
                }
                Console.WriteLine($"Пожалуйста, введите число от {min} до {max}.");
            }
        }
    }
}