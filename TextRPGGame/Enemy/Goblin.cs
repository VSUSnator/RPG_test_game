using System;
using TextRPGGame.Chacters;

namespace TextRPGGame.Enemies
{
    class Goblin : Enemy
    {
        private bool isPreparingSpecialAttack = false;

        public Goblin(string name, int health, int strength, int defense, int attackPower)
            : base(name, health, strength, defense, attackPower) { }

        public override void Attack(Character character)
        {
            Random rand = new Random();
            if (isPreparingSpecialAttack)
            {
                // Выполняем специальную атаку
                int criticalDamage = (Strength * 2) - character.Defense;
                criticalDamage = Math.Max(criticalDamage, 0);
                character.TakeDamage(criticalDamage);
                Console.WriteLine($"{Name} наносит критический удар в слабое место и наносит {criticalDamage} урона!");
                isPreparingSpecialAttack = false; // Сбросим флаг подготовки
            }
            else
            {
                // 25% шанс на подготовку специальной атаки
                if (rand.Next(0, 100) < 25)
                {
                    Console.WriteLine($"{Name} готовится ударить в слабое место!");
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