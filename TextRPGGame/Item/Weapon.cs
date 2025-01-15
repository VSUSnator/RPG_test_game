using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPGGame.Chacters;

namespace TextRPGGame.Item
{
    class Weapon : GameItem
    {
        public int DamageBonus { get; set; }

        public Weapon(string name, string description, int damageBonus)
            : base(name, description)
        {
            DamageBonus = damageBonus;
        }

        public override void Use(Character character)
        {
            character.Strength += DamageBonus; // Увеличение силы персонажа
            Console.WriteLine($"{character.Name} экипировал {Name} и получил бонус к силе: +{DamageBonus}.");
        }
    }
}
