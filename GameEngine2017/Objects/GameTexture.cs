using Microsoft.Xna.Framework.Graphics;

namespace GameEngine2017
{
    public class GameTexture
    {
        public Texture2D Texture { get; set; }

        public string Name { get; set; }

        public int FrameCount { get; set; }

        public GameTexture(string name, Texture2D texture)
        {
            Name = name;
            Texture = texture;
            FrameCount = FrameCount;
        }
    }
}
