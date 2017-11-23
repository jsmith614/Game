﻿using GameEngine2017.Constants;
using GameEngine2017.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine2017.Objects
{
    public class GameMessage
    {
        public string Text { get; set; }

        public MessageType Type { get; set; }

        public float LifeTime { get; set; }

        public GameMessage(string text, MessageType type, float lifeTime = 2000f)
        {
            Type = type;
            Text = text;
            LifeTime = lifeTime;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Pick text color by type
            var color = Color.White;
            if(Type == MessageType.Success)
            {
                color = Color.Green;
            }
            else if (Type == MessageType.Error)
            {
                color = Color.Red;
            }
            
            spriteBatch.DrawString(FontManager.Instance.GetFont(Font.Message), Text, position, color);
        }

        internal void Update(float deltaTime)
        {
            LifeTime -= deltaTime;
        }
    }
}