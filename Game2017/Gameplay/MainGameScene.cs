using GameEngine2017;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleFLib;
using System;
using System.Collections.Generic;

namespace Game2017
{
    public class MainGameScene : IScene
    {
        Texture2D _pixel;

        List<Vector2> _stuffToDraw;

        public MainGameScene()
        {

        }

        public void Load(SpriteBatch spriteBatch)
        {
            GameObjectFactory.Instance.CreateGameObject<Player>();
            MapManager.Instance.ChangeMap(Constants.Map1);

            // debug wireframe
            
            _pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Color[] colourData = new Color[1];
            colourData[0] = Color.White;
            _pixel.SetData<Color>(colourData);

            _stuffToDraw = new List<Vector2>();
        }

        public void Run(float deltaTime)
        {
            _stuffToDraw.Clear();

            Camera.Instance.Update(deltaTime);
            var cameraPosition = Camera.Instance.GetTransformPosition(0,0);
            var cameraRect = new RectangleF(cameraPosition.X, cameraPosition.Y, Camera.Instance.Width / Camera.Instance.Zoom, Camera.Instance.Height / Camera.Instance.Zoom);

            // get map geometry within view of camera
            var visibleMapGeometry = new List<Shape>();
            foreach(var shape in MapManager.Instance.MapGeometry)
            {
                if (shape.GetRect().Intersects(cameraRect))
                {
                    visibleMapGeometry.Add(shape);
                }
            }

            // check all objects against map geometry
            foreach(var obj in GameObjectManager.Instance.GameObjects)
            {
                var objRect = new RectangleF(obj.Position.X, obj.Position.Y, obj.Width, obj.Height);
                foreach(var shape in visibleMapGeometry)
                {
                    var shapeRect = shape.GetRect();

                    // Detect collision with Intersects()
                    if (objRect.Intersects(shapeRect))
                    {
                        //MessageHandler.Instance.AddMessage("Collision!");
                        // Maybe fire event and let individual objects handle it? idk.
                        // EventManager.Instance.FireEvent(shape, GameEvent.MapCollision);

                        //TODO: implement better collision reaction here:

                        // var halfShapeWidth = shape.Width * 0.5f;
                        // var halfShapeHeight = shape.Height * 0.5f;
                        // var halfObjWidth = obj.Width * 0.5f;
                        // var halfObjHeight = obj.Height * 0.5f;

                        // var shapeCenter = new Vector2(shape.Position.X + (halfShapeWidth), shape.Position.Y + (halfShapeHeight));
                        // var objCenter = new Vector2(obj.Position.X + (halfObjWidth), obj.Position.Y + (halfObjHeight));

                        // var intersect = Rectangle.Intersect(objRect, shapeRect);
                        // var idealDistance = new Vector2(intersect.Width, intersect.Height); //(halfShapeWidth) + (halfObjWidth), (halfShapeHeight) + (halfObjHeight));

                        // var reverse = objCenter - shapeCenter;
                        // reverse.Normalize();
                        // reverse = reverse * (idealDistance * deltaTime);
                        // var newPos = objCenter + reverse;

                        // newPos = new Vector2(newPos.X - (halfObjWidth), newPos.Y - (halfObjHeight));

                        //// _stuffToDraw.Add(objCenter);
                        //// _stuffToDraw.Add(newPos);

                        // obj.Position = new Vector2(newPos.X, newPos.Y);

                        
                        var velocity = (obj.Velocity * deltaTime);

                        var Ycol = new RectangleF(obj.Position.X, (obj.Position.Y - velocity.Y), obj.Width, obj.Height);
                        var Xcol = new RectangleF((obj.Position.X - velocity.X), obj.Position.Y, obj.Width, obj.Height);
                        var XYcol = new RectangleF((obj.Position.X - velocity.X), (obj.Position.Y - velocity.Y), obj.Width, obj.Height);

                        var newX = obj.Position.X;
                        var newY = obj.Position.Y;

                        if (Ycol.Intersects(shapeRect) == false)
                        {
                            newY += -velocity.Y;
                        }
                        else if (Xcol.Intersects(shapeRect) == false)
                        {
                            newX += -velocity.X;
                        }
                        else if (XYcol.Intersects(shapeRect) == false)
                        {
                            newX += -velocity.X;
                            newY += -velocity.Y;
                        }
                        
                        obj.Position = new Vector2(newX, newY);
                    }
                }
            }
        }

        public void Unload()
        {

        }

        public void Draw(GameTime gameTime, GameWindow window, SpriteBatch spriteBatch)
        {
            // Background
            
            if (InputManager.Instance.DebugMode)
            {
                // FPS
                var position = Camera.Instance.GetTransformPosition(Camera.Instance.Width, 20);
                var fps = Convert.ToInt32(1 / gameTime.ElapsedGameTime.TotalSeconds);
                MessageHandler.Instance.DrawString(spriteBatch, "FPS: " + fps, position, Color.White);

                // Zoom Level
                position = Camera.Instance.GetTransformPosition(Camera.Instance.Width, 50);
                MessageHandler.Instance.DrawString(spriteBatch, "Zoom: " + Camera.Instance.Zoom, position, Color.White);

                foreach (var shape in MapManager.Instance.MapGeometry)
                {
                    if (shape.Type == ShapeType.Rectangle)
                    {
                        DrawRectangle(spriteBatch, shape.GetRect(), Color.White);

                        var halfShapeWidth = shape.Width * 0.5f;
                        var halfShapeHeight = shape.Height * 0.5f;
                        var shapeCenter = new Vector2(shape.Position.X + (halfShapeWidth), shape.Position.Y + (halfShapeHeight));
                        DrawRectangle(spriteBatch, new Rectangle((int)shapeCenter.X, (int)shapeCenter.Y, 2, 2), Color.Red);

                        //var verts = Utility.GetVertsForBoundingBox(shape.Position, shape.Width, shape.Height);
                        //spriteBatch.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, verts, 0, verts.Count);
                    }
                }

                foreach (var obj in GameObjectManager.Instance.GameObjects)
                {
                    DrawRectangle(spriteBatch, new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Width, obj.Height), Color.White);

                    var halfObjWidth = obj.Width * 0.5f;
                    var halfObjHeight = obj.Height * 0.5f;
                    var objCenter = new Vector2(obj.Position.X + (halfObjWidth), obj.Position.Y + (halfObjHeight));
                    DrawRectangle(spriteBatch, new Rectangle((int)objCenter.X, (int)objCenter.Y, 2, 2), Color.Red);

                    //var verts = Utility.GetVertsForBoundingBox(obj.Position, obj.Width, obj.Height);
                }
                
                foreach(var thing in _stuffToDraw)
                {
                    DrawRectangle(spriteBatch, new Rectangle((int)thing.X, (int)thing.Y, 5, 5), Color.Yellow);
                }
            }
        }

        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle bounds, Color color)
        {
            spriteBatch.Draw(_pixel, bounds, color);
        }
    }
}
