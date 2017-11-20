using GameEngine2017.Constants;
using GameEngine2017.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine2017.Systems
{
    public class MapManager
    {
        private GameMap _currentMap;
        private SpriteBatch _spriteBatch;
        private static MapManager _instance;
        public static MapManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MapManager();
                }
                return _instance;
            }
        }

        public GameMap CurrentMap { get { return _currentMap; } }

        private MapManager()
        {
            _currentMap = new GameMap(MapName.Default);
        }

        public void Load(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Unload()
        {

        }

        public void Draw(GameWindow window)
        {
            var texture = TextureManager.Instance.GetTexture(_currentMap.Map);

            _spriteBatch.Draw(texture: texture, position: new Vector2(0, 0), scale: new Vector2(1.0f, 1.0f), layerDepth: 0);
        }
    }
}
