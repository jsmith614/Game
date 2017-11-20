using Game2017.Objects;
using Game2017.Systems;
using GameEngine2017.Constants;
using GameEngine2017.Systems;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Game2017.Gameplay
{
    public class MainGame
    {
        public MainGame()
        {

        }

        public void Load()
        {
            MessageHandler.Instance.AddMessage("Rawr");
            GameObjectFactory.Instance.CreateGameObject<Player>();
            MessageHandler.Instance.AddMessage("Rawr 2", MessageType.Normal, 3000f);
            MessageHandler.Instance.AddMessage("Rawr 3", MessageType.Normal, 4000f);
        }

        public void Run(float deltaTime)
        {
            Camera.Instance.Update(deltaTime);
        }

        public void Unload()
        {

        }

        internal void Draw(float deltaTime, GameWindow window, SpriteBatch spriteBatch)
        {
            // Background
            //spriteBatch.Draw()
        }
    }
}
