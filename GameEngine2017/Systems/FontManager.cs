using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine2017
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

        public GameFont DefaultFont { get; set; }

        private FontManager()
        {
            _fonts = new List<GameFont>();
        }

        public void Load(ContentManager content, List<string> fonts)
        {
            _content = content;
            _fonts.Clear();
            foreach(var fontName in fonts)
            {
                _fonts.Add(new GameFont(fontName, _content.Load<SpriteFont>(fontName)));
            }
            DefaultFont = _fonts.FirstOrDefault();
        }

        public void Unload()
        {
            _fonts.Clear();
        }

        public SpriteFont GetFont(string fontName)
        {
            // do something here with fonts
            var gameFont =  _fonts.SingleOrDefault(f => f.Name == fontName);
            if(gameFont != null)
            {
                return gameFont.SpriteFont;
            }
            else
            {
                MessageHandler.Instance.AddError("Font not loaded: " + fontName);
                return _fonts.First().SpriteFont;
            }
        }
    }
}
