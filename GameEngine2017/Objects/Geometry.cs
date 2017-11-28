using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine2017
{
    [Serializable]
    public enum ShapeType
    {
        Circle,
        Rectangle
    }

    [Serializable]
    public class Shape
    {
        public int ID { get; set; }

        public Vector2 Position { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Radius { get; set; }

        public ShapeType Type { get; set; }

        public Rectangle GetRect()
        {
            Rectangle shapeRect;
            if (Type == ShapeType.Rectangle)
            {
                shapeRect = new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }
            else
            {
                shapeRect = new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius), (int)(Radius * 2), (int)(Radius * 2));
            }
            return shapeRect;
        }
    }

    [Serializable]
    public class Handle
    {
        public Shape Shape { get; set; }

        public float Radius { get; set; }

        public Vector2 Position
        {
            get
            {
                if (Shape.Type == ShapeType.Circle)
                {
                    return new Vector2(Shape.Position.X + Shape.Radius, Shape.Position.Y);
                }
                else // if (Shape.Type == ShapeType.Rectangle)
                {
                    return new Vector2(Shape.Position.X + (Shape.Width), Shape.Position.Y + (Shape.Height));
                }
            }
        }
    }
    
    [Serializable]
    public class MapIOObject
    {
        public int CurrentID { get; set; }

        public string MapTextureName { get; set; }

        public List<Handle> Handles { get; set; }
    }
}
