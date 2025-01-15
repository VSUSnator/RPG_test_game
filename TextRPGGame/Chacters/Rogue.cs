using System;
using TextRPGGame.Enemies;

namespace TextRPGGame.Chacters
{
    public class Rogue : Character
    {
        public Rogue(string name) : base(name)
        {
            Strength = 7;
            Defense = 4;
            Agility = 8;
            Intelligence = 2;
            InitializeHealth(21); // Инициализируем здоровье на основе силы
        }

        public override void Attack(Enemy enemy)
        {
            int damage = Agility; // Урон зависит от Agility
            enemy.Health -= Math.Max(damage, 0);
            Console.WriteLine($"{Name} атакует {enemy.Name} и наносит {damage} урона!");
        }

        public override void Defend()
        {
            Console.WriteLine($"{Name} целится в уязвимое место.");
            IsDefending = true; // Установите флаг защиты
        }

        public void TakeDamage(int damage)
        {
            if (!IsDefending) // Если не защищается, получает урон
            {
                base.TakeDamage(damage);
            }
            else
            {
                Console.WriteLine($"{Name} успешно защищается! Урон блокирован.");
                IsDefending = false; // Сбрасываем флаг после блока
            }
        }
    }
}