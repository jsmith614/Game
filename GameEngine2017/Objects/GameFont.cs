using GameEngine2017.Constants;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine2017.Objects
{
    public class GameFont
    {
        public Font Name { get; set; }

        public SpriteFont SpriteFont { get; set; }

        public GameFont(Font name, SpriteFont spriteFont)
        {
            Name = name;
            SpriteFont = spriteFont;
        }
    }
}
