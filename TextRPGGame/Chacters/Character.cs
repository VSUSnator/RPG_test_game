using System;
using TextRPGGame.Enemies;
using TextRPGGame.GamePlay;

namespace TextRPGGame.Chacters
{
    public abstract class Character
    {
        public string Name { get; set; }
        protected int baseHealth;
        protected int health;
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public Inventory Inventory { get; set; }

        public int Level { get; private set; } // Уровень персонажа
        public int Experience { get; private set; } // Опыт, необходимый для повышения уровня

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health < 0) health = 0;
            }
        }

        public bool IsDefending { get; set; }
        public bool IsStunned { get; set; }
        public int MaxHealth { get; internal set; }

        protected Character(string name)
        {
            Name = name;
            Inventory = new Inventory();
            IsDefending = false;
            IsStunned = false;
            Level = 1;
            Experience = 0;
        }

        public abstract void Attack(Enemy enemy);
        public abstract void Defend();

        // Новый метод для получения максимального здоровья
        public int GetMaxHealth()
        {
            return baseHealth + (Strength * 2); // Пример: максимальное здоровье зависит от силы
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            Console.WriteLine($"{Name} получил {amount} опыта!");

            // Проверяем, достаточно ли опыта для повышения уровня
            if (Experience >= Level * 10) // Для повышения уровня нужно 10 * уровень опыта
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience -= Level * 10; // Сбрасываем опыт, необходимый для следующего уровня
            Console.WriteLine($"{Name} прокачался до уровня {Level}!");

            // Позволяем игроку выбрать, какую характеристику улучшить
            ChooseStatUpgrade();
        }

        private void ChooseStatUpgrade()
        {
            Console.WriteLine("Выберите, какую характеристику хотите улучшить:");
            Console.WriteLine("1. Сила");
            Console.WriteLine("2. Ловкость");
            Console.WriteLine("3. Интеллект");
            Console.WriteLine("4. Защита");
            int choice = GetValidIntInput(1, 4);

            switch (choice)
            {
                case 1:
                    Strength++;
                    Console.WriteLine($"{Name} увеличил Силу на 1!");
                    break;
                case 2:
                    Agility++;
                    Console.WriteLine($"{Name} увеличил Ловкость на 1!");
                    break;
                case 3:
                    Intelligence++;
                    Console.WriteLine($"{Name} увеличил Интеллект на 1!");
                    break;
                case 4:
                    Defense++;
                    Console.WriteLine($"{Name} увеличил Защиту на 1!");
                    break;
            }

            baseHealth += 5; // Увеличиваем базовое здоровье
            Health = GetMaxHealth(); // Обновляем текущее здоровье
            Console.WriteLine($"Новые характеристики: Сила: {Strength}, Ловкость: {Agility}, Интеллект: {Intelligence}, Защита: {Defense}");
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

        public void InitializeHealth(int baseHealth)
        {
            this.baseHealth = baseHealth; // Устанавливаем базовое здоровье
            Health = GetMaxHealth(); // Устанавливаем текущее здоровье
        }

        public void TakeDamage(int damage)
        {
            if (IsDefending)
            {
                damage = (int)(damage * 0.5); // Уменьшаем урон на 50% при защите
                IsDefending = false; // Сбрасываем защиту
            }

            Health -= damage;
        }

        public void SkipTurn()
        {
            IsStunned = false; // Сброс состояния оглушения
            Console.WriteLine($"{Name} пропускает ход из-за оглушения.");
        }
    }
}