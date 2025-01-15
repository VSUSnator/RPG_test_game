using System;
using System.Collections.Generic;
using TextRPGGame.Chacters;

namespace TextRPGGame.Item
{
    public abstract class GameItem  // Убедитесь, что Item также public
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected GameItem(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Use(Character character);
    }
}
