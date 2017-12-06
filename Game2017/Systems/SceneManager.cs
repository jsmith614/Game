using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Game2017.Systems
{
    public class SceneManager
    {
        private static SceneManager _instance;
        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManager();
                }
                return _instance;
            }
        }

        public Stack<IScene> Scenes { get; set; }

        private SceneManager()
        {

        }

        public void Load()
        {

        }

        public void Unload()
        {

        }

        public void Run(float deltaTime)
        {
            if (Scenes.Any())
            {
                Scenes.Peek().Run(deltaTime);
            }
        }

        public void Draw(float deltaTime, GameWindow gameWindow, SpriteBatch spriteBatch)
        {
            if (Scenes.Any())
            {
                Scenes.Peek().Draw(deltaTime, gameWindow, spriteBatch);
            }
        }

        public void Push(IScene scene)
        {
            Scenes.Push(scene);
        }

        public void Pop()
        {
            Scenes.Pop();
        }
    }
}
