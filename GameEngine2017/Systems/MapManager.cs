using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

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
        }

        private void ImportGeometry()
        {
            MessageHandler.Instance.AddMessage("Import Begin");

            MapGeometry = new List<Shape>();
            var filePath = @"Content\" + Config.Instance.MapDirectory + CurrentMap.TextureName + ".xml";

            //var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            //path = path.Substring(6);

            if (File.Exists(filePath))
            {
                var fileStream = new FileStream(filePath, FileMode.Open);

                var stream = new MemoryStream();

                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(stream);

                fileStream.Close();

                if (stream.Length > 0)
                {
                    var ioObject = Utility.DeserializeFromStream(stream);

                    MapGeometry.AddRange(ioObject.Handles.Select(h => h.Shape));

                    MessageHandler.Instance.AddSuccess("Import Complete");
                }
                else
                {
                    MessageHandler.Instance.AddError("Import Fail!");
                }
            }
        }

        public void Unload()
        {

        }

        public void Draw(GameWindow window)
        {
            if(_currentMap != null)
            {
                var texture = TextureManager.Instance.GetTexture(Config.Instance.MapDirectory + _currentMap.TextureName);

                _spriteBatch.Draw(texture: texture, position: new Vector2(0, 0), scale: new Vector2(1.0f, 1.0f), layerDepth: 0);
            }
        }

        public void ChangeMap(string mapName)
        {
            _currentMap = new GameMap(mapName);
            TextureManager.Instance.LoadTexture(Config.Instance.MapDirectory + mapName);
            ImportGeometry();
        }
    }
}
