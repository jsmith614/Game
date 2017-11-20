using GameEngine2017.Constants;
using GameEngine2017.Objects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine2017.Systems
{
    public class FontManager
    {
        private List<GameFont> _fonts;
        private ContentManager _content;
        private static FontManager _instance;
        public static FontManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FontManager();
                }
                return _instance;
            }
        }

        private FontManager()
        {
            _fonts = new List<GameFont>();
        }

        public void Load(ContentManager content)
        {
            _content = content;
            _fonts.Clear();
            foreach (var font in Enum.GetValues(typeof(Font)).Cast<Font>())
            {
                _fonts.Add(new GameFont(font, _content.Load<SpriteFont>(font.ToString())));
            }
        }

        public void Unload()
        {
            _fonts.Clear();
        }

        public SpriteFont GetFont(Font font)
        {
            // do something here with fonts
            var gameFont =  _fonts.SingleOrDefault(f => f.Name == font);
            if(gameFont != null)
            {
                return gameFont.SpriteFont;
            }
            else
            {
                MessageHandler.Instance.AddError("Font not loaded: " + font.ToString());
                return _fonts.First().SpriteFont;
            }
        }
    }
}
