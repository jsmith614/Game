using GameEngine2017.Constants;
using GameEngine2017.Objects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameEngine2017.Systems
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

        public void LoadTexture(TextureName textureName)
        {
            if(_textures.Select(t => t.Name).Contains(textureName) == false)
            {
                var texture = _content.Load<Texture2D>(textureName.ToString());
                _textures.Add(new GameTexture(textureName, texture));
            }
        }

        internal Texture2D GetTexture(MapName map)
        {
            var textureName = GetTextureName(map);
            return GetTexture(textureName);
        }

        public Texture2D GetTexture(ObjectType type)
        {
            var textureName = GetTextureName(type);
            return GetTexture(textureName);
        }

        public Texture2D GetTexture(TextureName textureName)
        {
            var gameTexture = _textures.SingleOrDefault(t => t.Name == textureName);
            if (gameTexture == null)
            {
                LoadTexture(textureName);
                gameTexture = _textures.SingleOrDefault(t => t.Name == textureName);
                if (gameTexture == null)
                {
                    MessageHandler.Instance.AddError("Texture {0} could not be loaded.", textureName.ToString());
                    return null;
                }
            }
            return gameTexture.Texture;
        }

        private TextureName GetTextureName(ObjectType type)
        {
            switch (type)
            {
                default:
                    return TextureName.Penguins;
            }
        }

        private TextureName GetTextureName(MapName map)
        {
            switch (map)
            {
                default:
                    return TextureName.Map;
            }
        }
    }
}
