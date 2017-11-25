using GameEngine2017;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game2017.Animations;

namespace Game2017
{
    public class Player : GameObject
    {
        public Player(Vector2 position, string textureName)
        {
            Initialize(position, textureName);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Camera.Instance.Position = new Vector2(Position.X, Position.Y);
        }

        public override void Draw(float deltaTime, SpriteBatch spriteBatch)
        {
            base.Draw(deltaTime, spriteBatch);
        }

        public override void HandleEvent(string eventName, IGameObject Source)
        {
            base.HandleEvent(eventName, Source);
        }

        public override void Initialize(Vector2 position, string textureName)
        {
            CurrentAnimation = new Player_Idle();
            base.Initialize(position, textureName);
            ObjectType = ObjectType.Player;
            MoveSpeed = 100.0f;
            Width = 25;
            Height = 25;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        public override void Unload()
        {
            base.Unload();
        }

        protected override void HandleInput(float deltaTime)
        {

        }
    }
}
