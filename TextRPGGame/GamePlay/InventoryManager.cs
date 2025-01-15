using System;
using TextRPGGame.Chacters;
using TextRPGGame.Item;

namespace TextRPGGame.GamePlay
{
    internal class InventoryManager
    {
        public void ShowInventory(Inventory inventory)
        {
            inventory.ShowItems(); // Исправлено
        }

        public void UseItem(Inventory inventory, string itemName, Character character)
        {
            inventory.UseItem(itemName, character);
        }
    }
}