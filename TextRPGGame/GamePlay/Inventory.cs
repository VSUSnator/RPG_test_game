using System;
using System.Collections.Generic;
using System.Linq;
using TextRPGGame.Chacters;
using TextRPGGame.Item;

namespace TextRPGGame.GamePlay
{
    public class Inventory // Убедитесь, что Inventory публичный
    {
        private List<GameItem> items;

        public Inventory()
        {
            items = new List<GameItem>();
        }

        public void AddItem(GameItem item)
        {
            items.Add(item);
            Console.WriteLine($"Предмет {item.Name} добавлен в инвентарь.");
        }

        public bool RemoveItem(GameItem item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                Console.WriteLine($"Предмет {item.Name} удален из инвентаря.");
                return true;
            }
            Console.WriteLine($"Предмет {item.Name} не найден в инвентаре.");
            return false;
        }

        public bool HasItem(string itemName)
        {
            return items.Exists(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public GameItem GetItem(string itemName)
        {
            return items.Find(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public bool HasHealingPotion()
        {
            return items.OfType<Potion>().Any(); // Проверяет, есть ли в инвентаре зелья
        }

        public void ConsumeHealingPotion(Character character)
        {
            Potion healingPotion = items.OfType<Potion>().FirstOrDefault();
            if (healingPotion != null)
            {
                healingPotion.Use(character);
                RemoveItem(healingPotion); // Удаляем зелье после использования
            }
            else
            {
                Console.WriteLine("У вас нет зелья лечения.");
            }
        }

        public void UseItem(string itemName, Character character)
        {
            GameItem item = items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                item.Use(character);
                RemoveItem(item); // Удаляем предмет после использования
            }
            else
            {
                Console.WriteLine($"Предмет с именем {itemName} не найден в инвентаре.");
            }
        }

        public void ShowItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Инвентарь пуст.");
                return;
            }

            Console.WriteLine("Предметы в инвентаре:");
            foreach (var item in items)
            {
                Console.WriteLine($"- {item.Name}: {item.Description}");
            }
        }
    }
}