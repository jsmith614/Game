using GameEngine2017;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Game2017MapEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2017MapEditor : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _map;
        Texture2D _circle;
        Texture2D _square;
        Texture2D _handle;
        float _deltaTime;
        float _cameraSpeed;
        List<Shape> Shapes;
        List<Handle> Handles;
        MouseState lastMouseState;
        MouseState currentMouseState;
        Handle draggedHandle;
        Shape draggedShape;
        Vector2 _mPos;
        Vector2 _prevmPos;
        ShapeType currentShapeType;
        int currentID;

        float _defaultCircleRadius = 50f;
        float _defaultRectHeight = 50f;
        float _defaultRectWidth = 50f;

        const float _maxCircleRadius = 100f;
        const float _maxRectHeight = 200f;
        const float _maxRectWidth = 200f;

        const float _minCircleRadius = 1f;
        const float _minRectHeight = 1f;
        const float _minRectWidth = 1f;

        const float _handleRadius = 5f;

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

            FontManager.Instance.Load(Content, new List<string>() { "Message" });
            MessageHandler.Instance.Load(spriteBatch);
            Camera.Instance.Load(Window);
            InputManager.Instance.Load(spriteBatch);


            //_map = Content.Load<Texture2D>("Map1");
            _circle = Content.Load<Texture2D>("circle3");
            _square = Content.Load<Texture2D>("rect4");
            _handle = Content.Load<Texture2D>("circle2");

            if(_map != null)
            {
                Camera.Instance.Position = new Vector2(_map.Width * 0.5f, _map.Height * 0.5f);
            }
            _cameraSpeed = 300f;
            Shapes = new List<Shape>();
            Handles = new List<Handle>();

            Mouse.WindowHandle = Window.Handle;
            currentMouseState = Mouse.GetState();
            currentShapeType = ShapeType.Circle;
            currentID = 0;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Camera.Instance.Unload();
            InputManager.Instance.Unload();
            MessageHandler.Instance.Unload();
            FontManager.Instance.Unload();
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

            if (InputManager.Instance.CheckKeyPressed(Keys.F1))
            {
                Export();
            }
            else if (InputManager.Instance.CheckKeyPressed(Keys.F2))
            {
                Import();
            }

            if (InputManager.Instance.CheckKeyPressed(Keys.D1))
            {
                currentShapeType = ShapeType.Circle;
            }
            else if (InputManager.Instance.CheckKeyPressed(Keys.D2))
            {
                currentShapeType = ShapeType.Rectangle;
            }

            if (InputManager.Instance.CheckKeyPressed(Keys.Delete))
            {
                var deleteIds = new List<int>();
                foreach(var shape in Shapes)
                {
                    if (shape.Type == ShapeType.Circle && GameMath.PointInCircle(_mPos, shape.Position, shape.Radius))
                    {
                        deleteIds.Add(shape.ID);
                    }
                    else if(shape.Type == ShapeType.Rectangle && GameMath.PointInRect(_mPos, shape.Position, shape.Width, shape.Height))
                    {
                        deleteIds.Add(shape.ID);
                    }
                }
                Shapes.RemoveAll(s => deleteIds.Contains(s.ID));
                Handles.RemoveAll(h => deleteIds.Contains(h.Shape.ID));
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

            Camera.Instance.Position = new Vector2(posX, posY);

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // get current mouse position
            _mPos = new Vector2(currentMouseState.X, currentMouseState.Y);
            // translate mouse position relative to camera
            _mPos = Vector2.Transform(_mPos, Matrix.Invert(Camera.Instance.TranslationMatrix));
            
            _prevmPos = new Vector2(lastMouseState.X, lastMouseState.Y);
            _prevmPos = Vector2.Transform(_prevmPos, Matrix.Invert(Camera.Instance.TranslationMatrix));


            if (draggedHandle == null && draggedShape == null && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (var handle in Handles)
                {
                    if (GameMath.PointInCircle(_mPos, handle.Position, handle.Radius))
                    {
                        draggedHandle = handle;
                        break;
                    }
                }
            }
            if(draggedHandle == null && draggedShape == null && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (var shape in Shapes)
                {
                    if (shape.Type == ShapeType.Rectangle && GameMath.PointInRect(_mPos, shape.Position, shape.Width, shape.Height))
                    {
                        draggedShape = shape;
                        break;
                    }
                    else if (shape.Type == ShapeType.Circle && GameMath.PointInCircle(_mPos, shape.Position, shape.Radius))
                    {
                        draggedShape = shape;
                        break;
                    }
                }
            }
            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                if (draggedHandle == null && draggedShape == null)
                {
                    if(currentShapeType == ShapeType.Circle)
                    {
                        var shape = new Shape() { Type = ShapeType.Circle, Position = _mPos, Radius = _defaultCircleRadius, ID = currentID++ };
                        Shapes.Add(shape);
                        var handle = new Handle() { Shape = shape, Radius = _handleRadius };
                        Handles.Add(handle);
                    }
                    else if (currentShapeType == ShapeType.Rectangle)
                    {
                        var shape = new Shape() { Type = ShapeType.Rectangle, Position = _mPos, Width = _defaultRectWidth, Height = _defaultRectHeight, ID = currentID++ };
                        Shapes.Add(shape);
                        var handle = new Handle() { Shape = shape, Radius = _handleRadius };
                        Handles.Add(handle);
                    }
                }
                else
                {
                    draggedShape = null;
                    draggedHandle = null;
                }
            }
            
            if(draggedHandle != null)
            {
                if(draggedHandle.Shape.Type == ShapeType.Circle)
                {
                    draggedHandle.Shape.Radius += (_mPos.X - _prevmPos.X);
                    _defaultCircleRadius = draggedHandle.Shape.Radius;

                    if (draggedHandle.Shape.Radius < _minCircleRadius)
                    {
                        draggedHandle.Shape.Radius = _minCircleRadius;
                    }
                    else if(draggedHandle.Shape.Radius > _maxCircleRadius)
                    {
                        draggedHandle.Shape.Radius = _maxCircleRadius;
                    }
                }
                else if(draggedHandle.Shape.Type == ShapeType.Rectangle)
                {
                    draggedHandle.Shape.Width += (_mPos.X - _prevmPos.X);
                    draggedHandle.Shape.Height += (_mPos.Y - _prevmPos.Y);
                    _defaultRectHeight = draggedHandle.Shape.Height;
                    _defaultRectWidth = draggedHandle.Shape.Width;

                    if (draggedHandle.Shape.Height > _maxRectHeight)
                    {
                        draggedHandle.Shape.Height = _maxRectHeight;
                    }
                    else if (draggedHandle.Shape.Height < _minRectHeight)
                    {
                        draggedHandle.Shape.Height = _minRectHeight;
                    }
                    if (draggedHandle.Shape.Width > _maxRectWidth)
                    {
                        draggedHandle.Shape.Width = _maxRectWidth;
                    }
                    if (draggedHandle.Shape.Width < _minRectWidth)
                    {
                        draggedHandle.Shape.Width = _minRectWidth;
                    }
                }
            }
            else if(draggedShape != null)
            {
                draggedShape.Position = new Vector2(draggedShape.Position.X + (_mPos.X - _prevmPos.X),
                                                    draggedShape.Position.Y + (_mPos.Y - _prevmPos.Y));
            }

            Camera.Instance.Update(_deltaTime);

            MessageHandler.Instance.UpdateMessages(_deltaTime);

            // This must be called after everything that uses input manager. Just keep it at the bottom.
            InputManager.Instance.UpdateOldInputStates();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: BlendState.AlphaBlend, transformMatrix: Camera.Instance.TranslationMatrix);

            if (_map != null)
            {
                spriteBatch.Draw(texture: _map, position: new Vector2(0, 0));

            }

            foreach (var shape in Shapes)
            {
                if (shape.Type == ShapeType.Circle)
                {
                    var rect = new Rectangle();
                    rect.X = (int)(shape.Position.X - shape.Radius);
                    rect.Y = (int)(shape.Position.Y - shape.Radius);
                    rect.Width = (int)(shape.Radius * 2);
                    rect.Height = (int)(shape.Radius * 2);

                    spriteBatch.Draw(texture: _circle, destinationRectangle: rect);
                }
                else if (shape.Type == ShapeType.Rectangle)
                {
                    var rect = new Rectangle();
                    rect.X = (int)(shape.Position.X); // - (shape.Width * 0.5f));
                    rect.Y = (int)(shape.Position.Y); // - (shape.Height * 0.5f));
                    rect.Width = (int)(shape.Width);
                    rect.Height = (int)(shape.Height);

                    spriteBatch.Draw(texture: _square, destinationRectangle: rect);
                }

            }

            foreach (var handle in Handles)
            {
                    var rect = new Rectangle();
                    rect.X = (int)(handle.Position.X - handle.Radius);
                    rect.Y = (int)(handle.Position.Y - handle.Radius);
                    rect.Width = (int)(handle.Radius * 2);
                    rect.Height = (int)(handle.Radius * 2);

                spriteBatch.Draw(texture: _handle, destinationRectangle: rect);
            }

            //Draw coords
            //spriteBatch.DrawString(FontManager.Instance.GetFont(GameEngine2017.Constants.Font.Message), "X: " + _mPos.X + " Y: " + _mPos.Y, _mPos, Color.White);

            MessageHandler.Instance.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        const string exportDirectory = "Exports";
        const string exportFilePath = "Exports/map.xml";
        const string tempexportFilePath = "Exports/map_temp.xml";
        const string backupexportFilePath = "Exports/map_backup.xml";

        private void Export()
        {
            MessageHandler.Instance.AddMessage("Export Begin");

            var ioObject = new MapIOObject();
            //ioObject.Shapes = Shapes;
            ioObject.Handles = Handles;
            ioObject.CurrentID = currentID;
            ioObject.MapTextureName = _map.Name;


            System.IO.Directory.CreateDirectory(exportDirectory);
            if (File.Exists(exportFilePath) == false)
            {
                File.Create(exportFilePath);
            }

            var fileStream = new FileStream(tempexportFilePath, FileMode.OpenOrCreate);

            var stream = Utility.SerializeToStream(ioObject);

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);

            fileStream.Close();
            
            System.IO.File.Replace(tempexportFilePath, exportFilePath, backupexportFilePath, true);

            //System.IO.File.Delete("Exports/map.bin");
            
            MessageHandler.Instance.AddMessage("Export Complete");
        }

        private void Import()
        {
            MessageHandler.Instance.AddMessage("Import Begin");

            System.IO.Directory.CreateDirectory(exportDirectory);
            if (File.Exists(exportFilePath) == false)
            {
                File.Create(exportFilePath);
            }

            var fileStream = new FileStream(exportFilePath, FileMode.OpenOrCreate);

            var stream = new MemoryStream();

            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.CopyTo(stream);

            fileStream.Close();

            if(stream.Length > 0)
            {
                var ioObject = Utility.DeserializeFromStream(stream);
                
                Shapes = new List<Shape>();
                Handles = ioObject.Handles;
                foreach(var handle in Handles)
                {
                    Shapes.Add(handle.Shape);
                }

                currentID = ioObject.CurrentID;
                _map = Content.Load<Texture2D>(ioObject.MapTextureName);
                Camera.Instance.Position = new Vector2(_map.Width * 0.5f, _map.Height * 0.5f);

                MessageHandler.Instance.AddSuccess("Import Complete");
            }
            else
            {
                MessageHandler.Instance.AddError("Import Fail!");
                _map = Content.Load<Texture2D>("Map1");
                Camera.Instance.Position = new Vector2(_map.Width * 0.5f, _map.Height * 0.5f);
            }
        }
    }
}
