using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Game2017
{
    public class SceneManager
    {
        private SpriteBatch _spriteBatch;
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
            Scenes = new Stack<IScene>();
        }

        public void Load(SpriteBatch spriteBatch)
        {
            Scenes.Clear();
            _spriteBatch = spriteBatch;
        }

        public void Unload()
        {
            while (Scenes.Any())
            {
                Pop();
            }
            Scenes.Clear();
        }

        public void Run(float deltaTime)
        {
            if (Scenes.Any())
            {
                Scenes.Peek().Run(deltaTime);
            }
        }

        public void Draw(GameTime gameTime, float deltaTime, GameWindow gameWindow)
        {
            if (Scenes.Any())
            {
                Scenes.Peek().Draw(gameTime, gameWindow, _spriteBatch);
            }
        }

        public void Push(IScene scene)
        {
            scene.Load(_spriteBatch);
            Scenes.Push(scene);
        }

        public void Pop()
        {
            if (Scenes.Any())
            {
                var scene = Scenes.Pop();
                scene.Unload();
            }
        }
    }
}
