using System;
using TextRPGGame.Chacters;

namespace TextRPGGame.Enemies
{
    public abstract class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int AttackPower { get; set; }

        protected Enemy(string name, int health, int strength, int defense, int attackPower)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Defense = defense;
            AttackPower = attackPower;
        }

        // Метод атаки, который использует метод TakeDamage у персонажа
        public abstract void Attack(Character character);
    }
}