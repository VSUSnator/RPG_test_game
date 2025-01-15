using System;
using TextRPGGame.Chacters;

namespace TextRPGGame.Enemies
{
    class Skeleton : Enemy
    {
        private bool isPreparingSpecialAttack = false;

        public Skeleton(string name, int health, int strength, int defense, int attackPower)
            : base(name, health, strength, defense, attackPower) { }

        public override void Attack(Character character)
        {
            Random rand = new Random();
            if (isPreparingSpecialAttack)
            {
                // Выполняем специальную атаку
                Health = Math.Min(Health + 10, 25); // Восстановление здоровья
                Console.WriteLine($"{Name} подставляет щит и восстанавливает 10 здоровья. Текущее здоровье: {Health}");
                isPreparingSpecialAttack = false; // Сбросим флаг подготовки
            }
            else
            {
                // 30% шанс на подготовку специальной атаки
                if (rand.Next(0, 100) < 10)
                {
                    Console.WriteLine($"{Name} поднимает щит для блока и готовится восстановить здоровье!");
                    isPreparingSpecialAttack = true; // Установим флаг подготовки
                }
                else
                {
                    // Обычная атака
                    int damage = Strength - character.Defense;
                    damage = Math.Max(damage, 0);
                    character.TakeDamage(damage);
                    Console.WriteLine($"{Name} атакует {character.Name} и наносит {damage} урона.");
                }
            }
        }
    }
}