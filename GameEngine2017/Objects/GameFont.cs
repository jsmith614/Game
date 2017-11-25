
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine2017
{
    public class GameFont
    {
        public string Name { get; set; }

        public SpriteFont SpriteFont { get; set; }

        public GameFont(string name, SpriteFont spriteFont)
        {
            Name = name;
            SpriteFont = spriteFont;
        }
    }
}
