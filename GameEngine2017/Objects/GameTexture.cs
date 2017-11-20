using GameEngine2017.Constants;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine2017.Objects
{
    public class GameTexture
    {
        public Texture2D Texture { get; set; }

        public TextureName Name { get; set; }

        public int FrameCount { get; set; }

        public GameTexture(TextureName name, Texture2D texture)
        {
            Name = name;
            Texture = texture;
            FrameCount = FrameCount;
        }
    }
}
