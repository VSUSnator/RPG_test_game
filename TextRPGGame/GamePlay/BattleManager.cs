using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using TextRPGGame.Chacters;
using TextRPGGame.Enemies;

namespace TextRPGGame.GamePlay
{
    internal class BattleManager
    {
        public void StartBattle(Character player, Enemy enemy)
        {

            while (player.Health > 0 && enemy.Health > 0)
            {
                // Вражеский ход
                enemy.Attack(player);
                if (player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name} был убит!");
                    break;
                }

                // Ход игрока
                int choice = ChooseAction();
                switch (choice)
                {
                    case 1:
                        player.Attack(enemy);
                        break;
                    case 2:
                        player.Defend();
                        break;
                    case 3:
                        // Логика использования предметов
                        Console.WriteLine("Использование предметов пока не реализовано.");
                        break;
                }

                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"{enemy.Name} был убит!");
                    Loot();
                    break;
                }
            }
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

        private void Loot()
        {
            Console.WriteLine("Вы получили золото и предметы!");
            // Здесь можно добавить логику для получения предметов
        }
    }
}