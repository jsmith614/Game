using GameEngine2017.Interface;
using GameEngine2017.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine2017.Systems
{
    public class GameObjectManager
    {
        private List<IGameObject> _gameObjects;
        private SpriteBatch _spriteBatch;
        private static GameObjectManager _instance;
        public static GameObjectManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObjectManager();
                }
                return _instance;
            }
        }

        private GameObjectManager()
        {
            _gameObjects = new List<IGameObject>();
        }

        public void Load(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _gameObjects.Clear();
        }

        public void Unload()
        {
            _gameObjects.ForEach(go => go.Unload());
            _gameObjects.Clear();
        }
        
        public void AddObject(IGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void AddObjects(List<IGameObject> gameObjects)
        {
            _gameObjects.AddRange(gameObjects);
        }

        public void Update(float deltaTime)
        {
            _gameObjects.ForEach(go => go.Update(deltaTime));
        }

        public void Draw(float deltaTime)
        {
            _gameObjects.ForEach(go => go.Draw(deltaTime, _spriteBatch));
        }
    }
}
