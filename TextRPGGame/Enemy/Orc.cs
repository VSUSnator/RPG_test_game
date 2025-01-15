using System;
using TextRPGGame.Chacters;

namespace TextRPGGame.Enemies
{
    class Orc : Enemy
    {
        private bool isPreparingSpecialAttack = false;

        public Orc(string name, int health, int strength, int defense, int attackPower)
            : base(name, health, strength, defense, attackPower) { }

        public override void Attack(Character character)
        {
            Random rand = new Random();
            if (isPreparingSpecialAttack)
            {
                // Выполняем специальную атаку
                int damage = (Strength * 2) - character.Defense; // Урон в 2 раза больше
                damage = Math.Max(damage, 0);
                character.TakeDamage(damage);
                Console.WriteLine($"{Name} наносит мощный удар и наносит {damage} урона!");

                // Если игрок не защитился, он оглушен
                if (!character.IsDefending)
                {
                    
                    character.IsStunned = true; // Предполагается, что у класса Character есть свойство IsStunned
                }
                isPreparingSpecialAttack = false; // Сбросим флаг подготовки
            }
            else
            {
                // 35% шанс на подготовку специальной атаки
                if (rand.Next(0, 100) < 35)
                {
                    Console.WriteLine($"{Name} замахивается для мощного удара!");
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