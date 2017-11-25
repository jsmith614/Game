using GameEngine2017;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2017
{
    public class MainGame
    {
        public MainGame()
        {

        }

        public void Load()
        {
            GameObjectFactory.Instance.CreateGameObject<Player>();
            MapManager.Instance.ChangeMap(Constants.Map1);
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
