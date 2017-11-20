using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine2017.Constants;
using GameEngine2017.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game2017.Animations;
using GameEngine2017.Systems;
using Microsoft.Xna.Framework.Input;
using GameEngine2017.Utilities;

namespace Game2017.Objects
{
    public class Player : GameObject
    {
        public Player(Vector2 position, int width, int height)
        {
            Initialize(position, width, height);
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

        public override void HandleEvent(EventName name, IGameObject Source)
        {
            base.HandleEvent(name, Source);
        }

        public override void Initialize(Vector2 position, int width, int height)
        {
            CurrentAnimation = new Player_Idle();
            base.Initialize(position, width, height);
            Type = ObjectType.Player;
            MoveSpeed = 200.0f;
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
