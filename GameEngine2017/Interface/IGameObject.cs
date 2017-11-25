using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine2017
{
    public interface IGameObject
    {
        string Type { get; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 Acceleration { get; set; }
        Vector2 Scale { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        float Depth { get; set; }
        float MoveSpeed { get; set; }

        void Load(ContentManager content);

        void Unload();

        void Update(float deltaTime);

        void Draw(float deltaTime, SpriteBatch spriteBatch);

        void HandleEvent(string name, IGameObject Source);
    }
}
