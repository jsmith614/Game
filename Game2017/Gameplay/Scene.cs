﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2017
{
    public interface IScene
    {
        void Load(SpriteBatch spriteBatch);
        void Run(float deltaTime);
        void Draw(GameTime gameTime, GameWindow window, SpriteBatch spriteBatch);
        void Unload();
    }
}
