using GameEngine2017.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game2017MapEditor
{
    public enum ShapeType
    {
        Circle,
        Rectangle
    }

    public class Shape
    {
        public Vector2 Position { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Radius { get; set; }

        public ShapeType Type { get; set; }
    }

    public class Handle
    {
        public Shape Shape { get; set; }

        public float Radius { get; set; }

        public Vector2 Position { get { return new Vector2(Shape.Position.X + Shape.Radius, Shape.Position.Y); } }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2017MapEditor : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _map;
        Texture2D _circle;
        float _deltaTime;
        float _cameraSpeed;
        List<Shape> Shapes;
        List<Handle> Handles;
        MouseState lastMouseState;
        MouseState currentMouseState;
        Handle draggedHandle;

        public Game2017MapEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //FontManager.Instance.Load(Content);
            //MessageHandler.Instance.Load(spriteBatch);
            Camera.Instance.Load(Window);
            InputManager.Instance.Load(spriteBatch);


            _map = Content.Load<Texture2D>("Map1");
            _circle = Content.Load<Texture2D>("circle");

            _cameraSpeed = 300f;
            Camera.Instance.Position = new Vector2(_map.Width * 0.5f, _map.Height * 0.5f);
            Shapes = new List<Shape>();
            Handles = new List<Handle>();

            currentMouseState = Mouse.GetState();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Camera.Instance.Unload();
            InputManager.Instance.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // This must be called before everything that uses input manager. Just keep it at the top.
            InputManager.Instance.AcquireNewInputStates();

            _deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            if (InputManager.Instance.Update(_deltaTime) == false)
            {
                Exit();
            }
            
            var posX = Camera.Instance.Position.X;
            var posY = Camera.Instance.Position.Y;

            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.W))
            {
                posY -= (_cameraSpeed * _deltaTime);
            }
            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.A))
            {
                posX -= (_cameraSpeed * _deltaTime);
            }
            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.S))
            {
                posY += (_cameraSpeed * _deltaTime);
            }
            if (InputManager.Instance.KeyboardState.IsKeyDown(Keys.D))
            {
                posX += (_cameraSpeed * _deltaTime);
            }
            
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            var mPos = new Vector2(currentMouseState.Position.X, currentMouseState.Y);
            
            if(draggedHandle == null && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (var handle in Handles)
                {
                    if (PointInCircle(mPos, handle.Position, handle.Radius))
                    {
                        draggedHandle = handle;
                        break;
                    }
                }
            }
            else if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                if (draggedHandle == null)
                {
                    var shape = new Shape() { Type = ShapeType.Circle, Position = mPos, Radius = 50f };
                    Shapes.Add(shape);
                    var handle = new Handle() { Shape = shape, Radius = 10f };
                    Handles.Add(handle);
                }
                else
                {
                    draggedHandle = null;
                }
            }
            
            if(draggedHandle != null)
            {
                draggedHandle.Shape.Radius += (currentMouseState.Position.X - lastMouseState.Position.X);
                if(draggedHandle.Shape.Radius < 20f)
                {
                    draggedHandle.Shape.Radius = 20f;
                }
            }

            Camera.Instance.Position = new Vector2(posX, posY);

            Camera.Instance.Update(_deltaTime);

            // This must be called after everything that uses input manager. Just keep it at the bottom.
            InputManager.Instance.UpdateOldInputStates();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: BlendState.AlphaBlend, transformMatrix: Camera.Instance.TranslationMatrix);

            spriteBatch.Draw(texture: _map, position: new Vector2(0, 0));

            foreach(var shape in Shapes)
            {
                var rect = new Rectangle();
                rect.X = (int)(shape.Position.X - shape.Radius);
                rect.Y = (int)(shape.Position.Y - shape.Radius);
                rect.Width = (int)(shape.Radius * 2);
                rect.Height = (int)(shape.Radius * 2);

                spriteBatch.Draw(texture: _circle, destinationRectangle: rect);
            }

            foreach (var handle in Handles)
            {
                var rect = new Rectangle();
                rect.X = (int)(handle.Position.X - handle.Radius);
                rect.Y = (int)(handle.Position.Y - handle.Radius);
                rect.Width = (int)(handle.Radius * 2);
                rect.Height = (int)(handle.Radius * 2);

                spriteBatch.Draw(texture: _circle, destinationRectangle: rect);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool PointInCircle(Vector2 point, Vector2 circle, float radius)
        {
            var dx = Math.Abs(point.X - circle.X);
            var dy = Math.Abs(point.Y - circle.Y);

            return ((dx + dy) <= radius);
        }
    }
}
