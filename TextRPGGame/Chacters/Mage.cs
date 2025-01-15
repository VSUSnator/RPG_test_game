using System;
using TextRPGGame.Enemies;

namespace TextRPGGame.Chacters
{
    public class Mage : Character
    {
        public Mage(string name) : base(name)
        {
            Strength = 6;
            Defense = 2;
            Agility = 4;
            Intelligence = 15;
            InitializeHealth(18); // Инициализируем здоровье на основе силы
        }

        public override void Attack(Enemy enemy)
        {
            int damage = Intelligence; // Урон зависит от интеллекта
            enemy.Health -= Math.Max(damage, 0);
            Console.WriteLine($"{Name} атакует {enemy.Name} и наносит {damage} урона!");
        }

        public override void Defend()
        {
            Console.WriteLine($"{Name} подготавливает заклинание для защиты.");
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