using Game2017.Objects;
using GameEngine2017.Constants;
using GameEngine2017.Interface;
using GameEngine2017.Objects;
using GameEngine2017.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Game2017.Systems
{
    public class GameObjectFactory
    {
        private ContentManager _content;
        private static GameObjectFactory _instance;
        public static GameObjectFactory Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameObjectFactory();
                }
                return _instance;
            }
        }

        private GameObjectFactory()
        {

        }

        public void Load(ContentManager content)
        {
            _content = content;
        }

        public void Unload()
        {

        }

        public T CreateGameObject<T>()
        {
            if (typeof(IGameObject).IsAssignableFrom(typeof(T)))
            {
                IGameObject gameObject;
                var type = typeof(T);
                if (type == typeof(Player))
                {
                    gameObject = new Player(new Vector2(300, 200), 100, 100);
                }
                else
                {
                    MessageHandler.Instance.AddError("CreateGameObject: Invalid GameObject Type.");
                    return default(T);
                }
                gameObject.Load(_content);
                GameObjectManager.Instance.AddObject(gameObject);
                return (T)gameObject;
            }
            else
            {
                MessageHandler.Instance.AddError("CreateGameObject: Type must be of IGameObject.");
                return default(T);
            }
        }
    }
}
