using GameEngine2017;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game2017
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2017 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        List<GameObject> _gameObjects;
        MainGame mainGame;
        float _deltaTime;

        public Game2017()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _gameObjects = new List<GameObject>();
            mainGame = new MainGame();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Camera.Instance.Initialize(Window);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            try
            {
                Config.Instance.Load();
                FontManager.Instance.Load(Content, Utility.GetEnumNameList<GameFont>());
                MessageHandler.Instance.Load(_spriteBatch);
                TextureManager.Instance.Load(Content);
                EventManager.Instance.Load(Utility.GetEnumNameList<GameEvent>());
                GameObjectManager.Instance.Load(_spriteBatch);
                GameObjectFactory.Instance.Load(Content);
                InputManager.Instance.Load(_spriteBatch);
                Camera.Instance.Load(Window);
                MapManager.Instance.Load(_spriteBatch);

                mainGame.Load(_spriteBatch);
            }
            catch (Exception e)
            {
                MessageHandler.Instance.AddError(e.Message);
                MessageHandler.Instance.Dump(e);
                throw e;
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            try
            {
                mainGame.Unload();

                MapManager.Instance.Unload();
                Camera.Instance.Unload();
                InputManager.Instance.Unload();
                GameObjectFactory.Instance.Unload();
                GameObjectManager.Instance.Unload();
                EventManager.Instance.Unload();
                FontManager.Instance.Unload();
                TextureManager.Instance.Unload();
                MessageHandler.Instance.Unload();
                Config.Instance.Unload();
            }
            catch (Exception e)
            {
                MessageHandler.Instance.AddError(e.Message);
                MessageHandler.Instance.Dump(e);
                throw e;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            try
            {
                base.Update(gameTime);

                // This must be called before everything that uses input manager. Just keep it at the top.
                InputManager.Instance.AcquireNewInputStates();

                _deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

                if (InputManager.Instance.Update(_deltaTime) == false)
                {
                    Exit();
                }

                MessageHandler.Instance.UpdateMessages(_deltaTime);
                GameObjectManager.Instance.Update(_deltaTime);

                mainGame.Run(_deltaTime);

                // This must be called after everything that uses input manager. Just keep it at the bottom.
                InputManager.Instance.UpdateOldInputStates();
            }
            catch (Exception e)
            {
                MessageHandler.Instance.AddError(e.Message);
                MessageHandler.Instance.Dump(e);
                throw e;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);

                base.Draw(gameTime);

                _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: BlendState.AlphaBlend, transformMatrix: Camera.Instance.TranslationMatrix);

                //var deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds;

                MapManager.Instance.Draw(Window);
                mainGame.Draw(_deltaTime, this.Window, _spriteBatch);
                GameObjectManager.Instance.Draw(_deltaTime);
                MessageHandler.Instance.Draw();
                InputManager.Instance.Draw(gameTime, this.Window, _deltaTime);

                _spriteBatch.End();

            }
            catch (Exception e)
            {
                MessageHandler.Instance.AddError(e.Message);
                MessageHandler.Instance.Dump(e);
                throw e;
            }
        }
    }
}
