using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine2017
{
    public class TextureManager
    {
        private List<GameTexture> _textures;
        private ContentManager _content;
        private static TextureManager _instance;
        public static TextureManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TextureManager();
                }
                return _instance;
            }
        }

        private TextureManager()
        {
            _textures = new List<GameTexture>();
        }
        
        public void Load(ContentManager content)
        {
            _textures.Clear();
            _content = content;
        }

        public void Unload()
        {
            _textures.Clear();
        }

        public void LoadTexture(string textureName)
        {
            if(_textures.Select(t => t.Name).Contains(textureName) == false)
            {
                var texture = _content.Load<Texture2D>(textureName);
                _textures.Add(new GameTexture(textureName, texture));
            }
        }

        public Texture2D GetTexture(string textureName)
        {
            var gameTexture = _textures.SingleOrDefault(t => t.Name == textureName);
            if (gameTexture == null)
            {
                LoadTexture(textureName);
                gameTexture = _textures.SingleOrDefault(t => t.Name == textureName);
                if (gameTexture == null)
                {
                    MessageHandler.Instance.AddError("Texture {0} could not be loaded.", textureName);
                    return null;
                }
            }
            return gameTexture.Texture;
        }
    }
}
