using System;
using TextRPGGame.Enemies;

namespace TextRPGGame.Chacters
{
    public class Warrior : Character
    {
        private bool isDefending = false;

        public Warrior(string name) : base(name)
        {
            Strength = 10;
            Defense = 2;
            Agility = 3;
            Intelligence = 1;
            InitializeHealth(30); // Инициализируем здоровье на основе силы
        }

        public override void Attack(Enemy enemy)
        {
            int damage = Strength; // Урон зависит от силы
            enemy.Health -= Math.Max(damage, 0);
            Console.WriteLine($"{Name} атакует {enemy.Name} и наносит {damage} урона!");
        }

        public override void Defend()
        {
            Console.WriteLine($"{Name} готовится к удару.");
            isDefending = true; // Установите флаг защиты
        }

        public void TakeDamage(int damage)
        {
            if (!isDefending) // Если не защищается, получает урон
            {
                base.TakeDamage(damage);
            }
            else
            {
                Console.WriteLine($"{Name} успешно защищается! Урон блокирован.");
                isDefending = false; // Сбрасываем флаг после блока
            }
        }
    }
}