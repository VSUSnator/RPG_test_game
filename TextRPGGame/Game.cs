using TextRPGGame.Chacters;
using TextRPGGame.Enemies;

namespace TextRPGGame
{
    class Game
    {
        private Character player;
        private Enemy currentEnemy;

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
            switch (choice)
            {
                case 1:
                    player = new Warrior(name);
                    break;
                case 2:
                    player = new Mage(name);
                    break;
                case 3:
                    player = new Rogue(name);
                    break;
            }

            Console.WriteLine($"{name} теперь {player.GetType().Name}!");
        }

        private void ExploreWorld()
        {
            while (player.Health > 0)
            {
                currentEnemy = GenerateRandomEnemy();
                Console.WriteLine($"{player.Name} идет по дороге и встречает {currentEnemy.Name}. Готовьтесь к бою!");
                Battle();
            }

            Console.WriteLine("К сожалению, ваш герой погиб. Игра окончена.");
        }

        private Enemy GenerateRandomEnemy()
        {
            Random rand = new Random();
            int enemyType = rand.Next(1, 4); // Случайное число от 1 до 3

            switch (enemyType)
            {
                case 1:
                    return new Goblin("Гоблин", 20, 5, 2, 4); // Добавлен attackPower
                case 2:
                    return new Skeleton("Скелет", 15, 4, 1, 3); // Предположим, что Skeleton также требует attackPower
                case 3:
                    return new Orc("Орк", 25, 6, 3, 5); // Предположим, что Orc также требует attackPower
                default:
                    return null; // Не должно происходить
            }
        }

        private void Battle()
        {
            while (player.Health > 0 && currentEnemy.Health > 0)
            {
                Console.WriteLine($"{currentEnemy.Name} атакует первым!");
                currentEnemy.Attack(player);
                if (player.Health <= 0)
                {
                    break;
                }

                Console.WriteLine($"У {player.Name} осталось {player.Health} здоровья.");
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
                        // Здесь вы можете добавить логику для использования предметов
                        Console.WriteLine("Использование предметов пока не реализовано.");
                        break;
                }

                if (currentEnemy.Health <= 0)
                {
                    Console.WriteLine($"{currentEnemy.Name} убит!");
                    Loot();
                    break;
                }
            }
        }

        private void Loot()
        {
            Console.WriteLine("Вы получили золото и предметы!");
            // Здесь можно добавить логику для получения предметов
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