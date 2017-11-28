using GameEngine2017;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game2017
{
    public class MainGame
    {
        public MainGame()
        {

        }

        public void Load()
        {
            GameObjectFactory.Instance.CreateGameObject<Player>();
            MapManager.Instance.ChangeMap(Constants.Map1);
        }

        public void Run(float deltaTime)
        {
            Camera.Instance.Update(deltaTime);
            var cameraRect = new Rectangle((int)Camera.Instance.Position.X, (int)Camera.Instance.Position.Y, (int)Camera.Instance.Width, (int)Camera.Instance.Height);

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
                var objRect = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Width, (int)obj.Height);
                foreach(var shape in visibleMapGeometry)
                {
                    var shapeRect = shape.GetRect();
                    // Detect collision with Intersects()
                    if (objRect.Intersects(shapeRect))
                    {
                        //MessageHandler.Instance.AddMessage("Collision!");
                        // Maybe fire event and let individual objects handle it? idk.
                        // EventManager.Instance.FireEvent(shape, GameEvent.MapCollision);

                        var shapeCenter = new Vector2(shape.Position.X + (shape.Width * 0.5f), shape.Position.Y + (shape.Height * 0.5f));
                        var objCenter = new Vector2(obj.Position.X + (obj.Width * 0.5f), obj.Position.Y + (obj.Height * 0.5f));

                        var pushX = ((shape.Width * 0.5f) + (obj.Width * 0.5f)) - Math.Abs(shapeCenter.X - objCenter.X);
                        //pushX = Math.Abs(pushX);
                        var pushY = ((shape.Height * 0.5f) + (obj.Height * 0.5f)) - Math.Abs(shapeCenter.Y - objCenter.Y);
                        //pushY = Math.Abs(pushY);

                        //var actualDistance = Math.Abs((shapeCenter - objCenter).Length());
                        //var idealDistance = Math.Abs(new Vector2((shape.Width * 0.5f) + (obj.Width * 0.5f), (shape.Height * 0.5f) + (obj.Height * 0.5f)).Length());



                        // React to collision
                        if(pushX > 0)
                        {
                            if (objRect.Left < shapeRect.Right)
                            {
                                objRect.X -= (int)pushX;
                                MessageHandler.Instance.AddMessage("Reaction!");
                            }
                            else if (objRect.Right > shapeRect.Left)
                            {
                                objRect.X += (int)pushX;
                                MessageHandler.Instance.AddMessage("Reaction!");
                            }
                        }

                        if (objRect.Bottom > shapeRect.Top)
                        {
                            objRect.Y = shapeRect.Top - objRect.Height;
                            MessageHandler.Instance.AddMessage("Reaction!");
                        }
                        else if (objRect.Top < shapeRect.Bottom)
                        {
                            objRect.Y = shapeRect.Bottom;
                            MessageHandler.Instance.AddMessage("Reaction!");
                        }
                        //var shapeCenter = new Vector2(shape.Position.X + (shape.Width * 0.5f), shape.Position.Y + (shape.Height * 0.5f));
                        //var objCenter = new Vector2(obj.Position.X + (obj.Width * 0.5f), obj.Position.Y + (obj.Height * 0.5f));

                        //var actualDistance = (shapeCenter - objCenter).Length();

                        //var idealDistance = new Vector2((shape.Width * 0.5f) + (obj.Width * 0.5f), (shape.Height * 0.5f) + (obj.Height * 0.5f)).Length();

                        //var newPosition = obj.Position + new Vector2((idealDistance - actualDistance), (idealDistance - actualDistance));

                        obj.Position = new Vector2(objRect.X, objRect.Y);
                    }
                }
            }
        }

        public void Unload()
        {

        }

        internal void Draw(float deltaTime, GameWindow window, SpriteBatch spriteBatch)
        {
            // Background
            //spriteBatch.Draw()
        }
    }
}
