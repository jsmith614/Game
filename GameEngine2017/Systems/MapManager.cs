using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace GameEngine2017
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

        public List<Shape> MapGeometry { get; set; }

        private MapManager()
        {

        }

        public void Load(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            ImportGeometry();
        }

        private void ImportGeometry()
        {
            
        }

        public void Unload()
        {

        }

        public void Draw(GameWindow window)
        {
            if(_currentMap != null)
            {
                var texture = TextureManager.Instance.GetTexture(_currentMap.TextureName);

                _spriteBatch.Draw(texture: texture, position: new Vector2(0, 0), scale: new Vector2(1.0f, 1.0f), layerDepth: 0);
            }
        }

        public void ChangeMap(string mapName)
        {
            _currentMap = new GameMap(mapName);
        }
    }
}
