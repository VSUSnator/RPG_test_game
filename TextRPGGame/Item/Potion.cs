using System;
using TextRPGGame.Chacters;

namespace TextRPGGame.Item
{
    public class Potion : GameItem
    {
        private int healingAmount;

        public Potion(string name, string description, int healingAmount)
            : base(name, description)
        {
            this.healingAmount = healingAmount;
        }

        public int HealthRestored => healingAmount; // Добавлено свойство для восстановления здоровья

        public override void Use(Character character)
        {
            character.Health += healingAmount; // Увеличиваем здоровье персонажа
            Console.WriteLine($"{character.Name} восстановил {healingAmount} здоровья!");
        }
    }
}