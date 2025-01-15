using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGGame.Chacters;

namespace TextRPGGame.Item
{
    class Armor : GameItem
    {
        public int DefenseBonus { get; set; }

        public Armor(string name, string description, int defenseBonus)
            : base(name, description)
        {
            DefenseBonus = defenseBonus;
        }

        public override void Use(Character character)
        {
            character.Defense += DefenseBonus; // Увеличение защиты персонажа
            Console.WriteLine($"{character.Name} надел {Name} и получил бонус к защите: +{DefenseBonus}.");
        }
    }
}
